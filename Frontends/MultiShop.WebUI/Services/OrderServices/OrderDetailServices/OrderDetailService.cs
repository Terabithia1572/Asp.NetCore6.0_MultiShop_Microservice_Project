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
            // 🔹 JSON’da ProductQuantity yerine ProductAmount olarak göndereceğiz
            var payload = new
            {
                dto.ProductID,
                dto.ProductName,
                dto.ProductPrice,
                ProductAmount = dto.ProductQuantity, // ✅ manuel map
                dto.ProductTotalPrice,
                dto.OrderingID
            };

            await _httpClient.PostAsJsonAsync("orderdetails", payload);
        }
        public async Task<List<CreateOrderDetailDTO>> GetOrderDetailsByOrderingIdAsync(int orderingId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"OrderDetails/GetOrderDetailsByOrderingId/{orderingId}");

                if (!response.IsSuccessStatusCode)
                    return new List<CreateOrderDetailDTO>();

                var json = await response.Content.ReadAsStringAsync();

                // Eğer API null döndürürse deserialize sırasında null almamak için kontrol
                if (string.IsNullOrWhiteSpace(json))
                    return new List<CreateOrderDetailDTO>();

                var values = System.Text.Json.JsonSerializer.Deserialize<List<CreateOrderDetailDTO>>(json,
                    new System.Text.Json.JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                return values ?? new List<CreateOrderDetailDTO>();
            }
            catch
            {
                // Herhangi bir hata durumunda boş liste dön
                return new List<CreateOrderDetailDTO>();
            }
        }



    }
}
