using MultiShop.DTOLayer.OrderDTOs.OrderDetailDTO;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MultiShop.WebUI.Services.OrderServices.OrderDetailServices
{
    public class OrderDetailService : IOrderDetailService
    {
        private readonly HttpClient _httpClient;

        public OrderDetailService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateOrderDetailAsync(CreateOrderDetailDTO dto)
        {
            await _httpClient.PostAsJsonAsync("orderdetails", dto);
        }
    }
}
