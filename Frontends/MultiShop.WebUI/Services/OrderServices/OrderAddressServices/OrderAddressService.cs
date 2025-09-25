using MultiShop.DTOLayer.OrderDTOs.OrderAddressDTO;

namespace MultiShop.WebUI.Services.OrderServices.OrderAddressServices
{
    public class OrderAddressService : IOrderAddressService
    {
        private readonly HttpClient _httpClient; //HttpClient nesnesi
        public OrderAddressService(HttpClient httpClient) //Dependency Injection ile HttpClient nesnesi alınır
        {
            _httpClient = httpClient; //HttpClient nesnesi atanır
        }

        public async Task CreateOrderAddressAsync(CreateOrderAddressDTO createOrderAddressDTO) //Yeni kategori oluşturur
        {
            await _httpClient.PostAsJsonAsync<CreateOrderAddressDTO>("addresses", createOrderAddressDTO); //HttpClient ile POST isteği gönderilir

        }

    }
}
