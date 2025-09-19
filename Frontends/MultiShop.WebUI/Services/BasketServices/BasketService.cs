using MultiShop.DTOLayer.BasketDTOs;

namespace MultiShop.WebUI.Services.BasketServices
{
    public class BasketService : IBasketService
    {
        private readonly HttpClient _httpClient;

        public BasketService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task AddBasketItem(BasketItemDTO basketItemDTO)
        {
            throw new NotImplementedException();
        }

        public Task DeleteBasket(string UserID)
        {
            throw new NotImplementedException();
        }

        public async Task<BasketTotalDTO> GetBasket(string UserID)
        {
            var responseMessage = await _httpClient.GetAsync("baskets"); // "baskets" endpoint'ine GET isteği gönder
            var values=await responseMessage.Content.ReadFromJsonAsync<BasketTotalDTO>(); // gelen cevabı BasketTotalDTO türüne dönüştür
            return values; // dönüştürülen veriyi döndür
        }

        public Task<bool> RemoveBasketItem(string ProductID)
        {
            throw new NotImplementedException();
        }

        public async Task SaveBasket(BasketTotalDTO basketTotalDTO)
        {
            await _httpClient.PostAsJsonAsync<BasketTotalDTO>("baskets", basketTotalDTO); // "baskets" endpoint'ine POST isteği gönder ve basketTotalDTO'yu JSON olarak ekle
        }
    }
}
