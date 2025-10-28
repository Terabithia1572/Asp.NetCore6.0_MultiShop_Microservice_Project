using Microsoft.AspNetCore.Http;
using MultiShop.DTOLayer.BasketDTOs;
using MultiShop.WebUI.Services.Interfaces;
using System.Net;
using System.Net.Http.Json;

namespace MultiShop.WebUI.Services.BasketServices
{
    public class BasketService : IBasketService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IUserService _userService;

        public BasketService(HttpClient httpClient, IHttpContextAccessor contextAccessor, IUserService userService)
        {
            _httpClient = httpClient;
            _contextAccessor = contextAccessor;
            _userService = userService;
        }

        // ✅ Kullanıcının ID’sini güvenli şekilde getir
        private async Task<string> GetUserIdAsync()
        {
            var user = await _userService.GetUserInfo();
            return user?.ID ?? string.Empty;
        }

        public async Task AddBasketItem(BasketItemDTO basketItemDto)
        {
            var values = await GetBasket();

            if (values == null)
                values = new BasketTotalDTO { BasketItems = new List<BasketItemDTO>() };

            var existingItem = values.BasketItems.FirstOrDefault(x => x.ProductID == basketItemDto.ProductID);

            if (existingItem == null)
                values.BasketItems.Add(basketItemDto);
            else
                existingItem.ProductQuantity += basketItemDto.ProductQuantity;

            // 🧠 UserID'yi garantili ata
            values.UserID = await GetUserIdAsync();

            await SaveBasket(values);
        }

        public async Task DeleteBasket(string userId)
        {
            // 💡 UserID parametresi boş gelse bile kendi ID'sini alır
            if (string.IsNullOrEmpty(userId))
                userId = await GetUserIdAsync();

            var response = await _httpClient.DeleteAsync("baskets");
            response.EnsureSuccessStatusCode();
        }

        public async Task<BasketTotalDTO> GetBasket()
        {
            var response = await _httpClient.GetAsync("baskets");

            // 🧩 Sepet bulunamadıysa veya Redis’te kayıt yoksa
            if (response.StatusCode == HttpStatusCode.NotFound || !response.IsSuccessStatusCode)
            {
                var userId = await GetUserIdAsync();

                // 🚀 Kullanıcıya özel boş sepet oluştur
                var emptyBasket = new BasketTotalDTO
                {
                    UserID = userId,
                    DiscountCode = "",
                    DiscountRate = null,
                    BasketItems = new List<BasketItemDTO>(),
                   
                };

                // 🧩 Redis'e kaydet (otomatik sepet oluştur)
                await SaveBasket(emptyBasket);

                // 🔄 Yeni boş sepeti geri döndür
                return emptyBasket;
            }

            var values = await response.Content.ReadFromJsonAsync<BasketTotalDTO>();

            // 🧠 Kullanıcı ID boşsa doldur
            if (values != null && string.IsNullOrEmpty(values.UserID))
                values.UserID = await GetUserIdAsync();

            return values;
        }


        public async Task<bool> RemoveBasketItem(string id)
        {
            var values = await GetBasket();
            var deletedItem = values.BasketItems.FirstOrDefault(x => x.ProductID == id);

            if (deletedItem == null)
                return false;

            values.BasketItems.Remove(deletedItem);
            values.UserID = await GetUserIdAsync(); // garanti altına al

            await SaveBasket(values);
            return true;
        }

        public async Task SaveBasket(BasketTotalDTO basketTotalDto)
        {
            // 🧠 UserID boşsa tekrar al
            if (string.IsNullOrEmpty(basketTotalDto.UserID))
                basketTotalDto.UserID = await GetUserIdAsync();

            await _httpClient.PostAsJsonAsync("baskets", basketTotalDto);
        }
        public async Task ClearBasketAsync()
        {
            var basket = await GetBasket();
            if (basket == null) return;

            basket.BasketItems.Clear(); // 🔥 Tüm ürünleri temizle
            await SaveBasket(basket);   // Güncel boş sepeti kaydet
        }

    }
}
