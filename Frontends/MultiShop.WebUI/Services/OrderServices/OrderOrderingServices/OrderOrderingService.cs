using MultiShop.DTOLayer.CommentDTOs;
using MultiShop.DTOLayer.OrderDTOs.OrderingDTOs;
using Newtonsoft.Json;

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
            var responseMessage = await _httpClient.GetAsync($"orderings/GetOrderingsByUserID/{userID}");
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultOrderingByUserIDDTO>>(jsonData);
            return values;
        }
    }
}
