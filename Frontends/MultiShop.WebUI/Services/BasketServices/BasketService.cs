
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
        public async Task AddBasketItem(BasketItemDTO basketItemDto)
        {
            var values = await GetBasket();

            if (values == null)
            {
                values = new BasketTotalDTO
                {
                    BasketItems = new List<BasketItemDTO>()
                };
            }

            var existingItem = values.BasketItems.FirstOrDefault(x => x.ProductID == basketItemDto.ProductID);

            if (existingItem == null)
            {
                values.BasketItems.Add(basketItemDto);
            }
            else
            {
                existingItem.ProductQuantity += basketItemDto.ProductQuantity; // miktarı artır
            }

            await SaveBasket(values);
        }


        public Task DeleteBasket(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<BasketTotalDTO> GetBasket()
        {
            var responseMessage = await _httpClient.GetAsync("baskets");
            var values = await responseMessage.Content.ReadFromJsonAsync<BasketTotalDTO>();
            return values;
        }

        public async Task<bool> RemoveBasketItem(string productId)
        {
            var values = await GetBasket();
            var deletedItem = values.BasketItems.FirstOrDefault(x => x.ProductID == productId);
            var result = values.BasketItems.Remove(deletedItem);
            await SaveBasket(values);
            return true;
        }

        public async Task SaveBasket(BasketTotalDTO basketTotalDto)
        {
            await _httpClient.PostAsJsonAsync<BasketTotalDTO>("baskets", basketTotalDto);
        }
    }
}