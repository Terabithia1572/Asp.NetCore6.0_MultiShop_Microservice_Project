using MultiShop.Catalog.DTOs.ProductDiscountDTOs;
using System.Net.Http.Json;

namespace MultiShop.WebUI.Services.CatalogServices
{
    public interface IProductDiscountService
    {
        Task<List<ResultProductDiscountDTO>> GetAllAsync(); // Tüm indirimleri getir
        Task<ResultProductDiscountDTO> GetByIdAsync(string id); // ID'ye göre getir
        Task CreateAsync(CreateProductDiscountDTO dto); // Yeni indirim oluştur
        Task UpdateAsync(UpdateProductDiscountDTO dto); // Güncelle
        Task DeleteAsync(string id); // Sil
    }

    public class ProductDiscountService : IProductDiscountService
    {
        private readonly HttpClient _httpClient;

        public ProductDiscountService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // 🔥 Ürün isimleriyle birlikte tüm indirimleri getir
        public async Task<List<ResultProductDiscountDTO>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync("productdiscounts/withnames");

            if (!response.IsSuccessStatusCode)
                return new List<ResultProductDiscountDTO>();

            var result = await response.Content.ReadFromJsonAsync<List<ResultProductDiscountDTO>>();
            return result ?? new List<ResultProductDiscountDTO>();
        }

        public async Task<ResultProductDiscountDTO> GetByIdAsync(string id)
        {
            return await _httpClient.GetFromJsonAsync<ResultProductDiscountDTO>($"productdiscounts/{id}");
        }

        public async Task CreateAsync(CreateProductDiscountDTO dto)
        {
            await _httpClient.PostAsJsonAsync("productdiscounts", dto);
        }

        public async Task UpdateAsync(UpdateProductDiscountDTO dto)
        {
            await _httpClient.PutAsJsonAsync("productdiscounts", dto);
        }

        public async Task DeleteAsync(string id)
        {
            await _httpClient.DeleteAsync($"productdiscounts/{id}");
        }
    }
}
