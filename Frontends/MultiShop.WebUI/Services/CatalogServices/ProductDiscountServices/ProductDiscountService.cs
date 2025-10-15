using MultiShop.DTOLayer.CatalogDTOs.ProductDiscountDTOs;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Services.CatalogServices.ProductDiscountServices
{
    public class ProductDiscountService : IProductDiscountService
    {
        private readonly HttpClient _httpClient; // HttpClient nesnesi

        public ProductDiscountService(HttpClient httpClient) // Dependency Injection
        {
            _httpClient = httpClient;
        }

        // 🔹 Tüm ürün indirimlerini getir
        public async Task<List<ResultProductDiscountDTO>> GetAllProductDiscountAsync()
        {
            // API endpoint ismi Controller adının küçük haliyle aynı olmalı (productdiscounts)
            var response = await _httpClient.GetAsync("productdiscounts/withnames"); // GET isteği gönderilir
            var jsonData = await response.Content.ReadAsStringAsync(); // JSON verisi okunur

            Console.WriteLine("🎯 DEBUG ProductDiscount JSON => " + jsonData);

            // JSON verisini DTO listesine dönüştür
            var values = JsonConvert.DeserializeObject<List<ResultProductDiscountDTO>>(jsonData);
            return values ?? new List<ResultProductDiscountDTO>(); // Null koruması
        }

        // 🔹 ID'ye göre getir
        public async Task<GetByIDProductDiscountDTO> GetByIdProductDiscountAsync(string id)
        {
            var response = await _httpClient.GetAsync("productdiscounts/" + id);
            var jsonData = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<GetByIDProductDiscountDTO>(jsonData);
        }

        // 🔹 Yeni indirim ekle
        public async Task CreateProductDiscountAsync(CreateProductDiscountDTO dto)
        {
            await _httpClient.PostAsJsonAsync("productdiscounts", dto);
        }

        // 🔹 Güncelle
        public async Task UpdateProductDiscountAsync(UpdateProductDiscountDTO dto)
        {
            await _httpClient.PutAsJsonAsync("productdiscounts", dto);
        }

        // 🔹 Sil
        public async Task DeleteProductDiscountAsync(string id)
        {
            await _httpClient.DeleteAsync("productdiscounts/" + id);
        }
    }
}
