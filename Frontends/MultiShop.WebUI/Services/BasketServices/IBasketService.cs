using MultiShop.DTOLayer.BasketDTOs;

namespace MultiShop.WebUI.Services.BasketServices
{
    public interface IBasketService
    {
        Task<BasketTotalDTO> GetBasket(string UserID); // belirli bir kullanıcının sepetini getirir
        Task SaveBasket(BasketTotalDTO basketTotalDTO); // Sepeti kaydeder veya günceller
        Task DeleteBasket(string UserID); // Belirli bir kullanıcının sepetini siler
    }
}
