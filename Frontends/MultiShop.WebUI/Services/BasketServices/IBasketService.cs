using MultiShop.DTOLayer.BasketDTOs;

namespace MultiShop.WebUI.Services.BasketServices
{
    public interface IBasketService
    {

        Task<BasketTotalDTO> GetBasket();
        Task SaveBasket(BasketTotalDTO basketTotalDTO);
        Task DeleteBasket(string userId);
        Task AddBasketItem(BasketItemDTO basketItemDTO);
        Task<bool> RemoveBasketItem(string productID);

        // 🔹 Yeni eklenen metod — sepeti silmeden temizler
        Task ClearBasketAsync();
    }
}