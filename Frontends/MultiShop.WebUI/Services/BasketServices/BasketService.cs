using MultiShop.DTOLayer.BasketDTOs;
using System.Net.Http.Json;

namespace MultiShop.WebUI.Services.BasketServices
{
    public class BasketService : IBasketService
    {
        private readonly HttpClient _httpClient;

        public BasketService(HttpClient httpClient)
        {
            _httpClient = httpClient;
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

            await SaveBasket(values);
        }

        public async Task DeleteBasket(string userId)
        {
            var response = await _httpClient.DeleteAsync("baskets");
            response.EnsureSuccessStatusCode();
        }

        public async Task<BasketTotalDTO> GetBasket()
        {
            var response = await _httpClient.GetAsync("baskets");
            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadFromJsonAsync<BasketTotalDTO>();
        }

        public async Task<bool> RemoveBasketItem(string id)
        {
            var values = await GetBasket();
            var deletedItem = values.BasketItems.FirstOrDefault(x => x.ProductID == id);

            if (deletedItem == null)
                return false;

            values.BasketItems.Remove(deletedItem);
            await SaveBasket(values);
            return true;
        }

        public async Task SaveBasket(BasketTotalDTO basketTotalDto)
        {
            await _httpClient.PostAsJsonAsync("baskets", basketTotalDto);
        }
    }
}
