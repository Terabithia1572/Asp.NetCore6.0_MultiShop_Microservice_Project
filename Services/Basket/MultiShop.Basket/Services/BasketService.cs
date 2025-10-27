using MultiShop.Basket.DTOs.BasketTotalDTOs;
using MultiShop.Basket.Settings;
using System.Text.Json;

namespace MultiShop.Basket.Services
{
    public class BasketService : IBasketService
    {
        private readonly RedisService _redisService;

        public BasketService(RedisService redisService)
        {
            _redisService = redisService;
        }

        public async Task DeleteBasket(string UserID)
        {
            await _redisService.GetDB().KeyDeleteAsync(UserID); // Belirli bir kullanıcının sepetini siler
        }

        public async Task<BasketTotalDTO> GetBasket(string UserID)
        {
            var existBasket = await _redisService.GetDB().StringGetAsync(UserID); // belirli bir kullanıcının sepetini getirir
            return JsonSerializer.Deserialize<BasketTotalDTO>(existBasket); // Sepeti JSON'dan nesneye dönüştürür

        }

        public async Task SaveBasket(BasketTotalDTO basketTotalDTO)
        {
            await _redisService.GetDB().StringSetAsync(basketTotalDTO.UserID, JsonSerializer.Serialize(basketTotalDTO)); // Sepeti kaydeder veya günceller
        }
    }
}