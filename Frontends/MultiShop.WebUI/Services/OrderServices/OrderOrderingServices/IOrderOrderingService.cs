using MultiShop.DTOLayer.OrderDTOs.OrderingDTO;
using MultiShop.DTOLayer.OrderDTOs.OrderingDTOs;

namespace MultiShop.WebUI.Services.OrderServices.OrderOrderingServices
{
    public interface IOrderOrderingService
    {
        Task<List<ResultOrderingByUserIDDTO>> GetOrderingByUserID(string userID); // Kullanıcı ID'sine göre siparişleri getirir
                                                                                  // ✅ Yeni eklenen satır:
        Task<int> CreateOrderingAsync(CreateOrderingDTO createOrderingDTO); // Yeni sipariş oluşturur
        Task<List<ResultOrderingByUserIDDTO>> GetAllOrdersAsync();

    }
}
