using MultiShop.DTOLayer.OrderDTOs.OrderAddressDTO;

namespace MultiShop.WebUI.Services.OrderServices.OrderAddressServices
{
    public interface IOrderAddressService
    {
       // Task<List<ResultOrderAddressDTO>> GetAllOrderAddressAsync(); // Tüm kategorileri getirir
        Task CreateOrderAddressAsync(CreateOrderAddressDTO createOrderAddressDTO); // Yeni kategori oluşturur
      //  Task UpdateOrderAddressAsync(UpdateOrderAddressDTO updateOrderAddressDTO); // Kategoriyi günceller
      //  Task DeleteOrderAddressAsync(string id); // Kategoriyi siler
      //  Task<UpdateOrderAddressDTO> GetByIDOrderAddressAsync(string id); // ID ile kategori getirir
    }
}
