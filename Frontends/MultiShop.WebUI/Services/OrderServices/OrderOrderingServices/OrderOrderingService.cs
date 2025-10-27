using MultiShop.DTOLayer.OrderDTOs.OrderingDTO;
using MultiShop.DTOLayer.OrderDTOs.OrderingDTOs;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace MultiShop.WebUI.Services.OrderServices.OrderOrderingServices
{
    public class OrderOrderingService : IOrderOrderingService
    {
        private readonly HttpClient _httpClient;

        public OrderOrderingService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ResultOrderingByUserIDDTO>> GetOrderingByUserID(string userID)
        {
            var response = await _httpClient.GetAsync($"orderings/GetOrderingsByUserID/{userID}");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<ResultOrderingByUserIDDTO>>(json);
        }

        // ✅ Yeni eklenen metot:
        public async Task<int> CreateOrderingAsync(CreateOrderingDTO createOrderingDTO)
        {
            var response = await _httpClient.PostAsJsonAsync("orderings", createOrderingDTO);
            response.EnsureSuccessStatusCode();

            // Eğer backend (Order microservice) sadece "OK" dönerse 0 döndür
            // ama ID dönüyorsa burada o değeri deserialize edebiliriz
            return 0;
        }
    }
}
