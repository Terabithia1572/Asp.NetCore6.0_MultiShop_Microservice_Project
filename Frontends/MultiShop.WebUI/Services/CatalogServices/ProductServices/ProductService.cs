using MultiShop.DTOLayer.CatalogDTOs.ProductDTOs;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Services.CatalogServices.ProductServices
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient; //HttpClient nesnesi
        public ProductService(HttpClient httpClient) //Dependency Injection ile HttpClient nesnesi alınır
        {
            _httpClient = httpClient; //HttpClient nesnesi atanır
        }

        public async Task CreateProductAsync(CreateProductDTO createProductDTO)
        {
            await _httpClient.PostAsJsonAsync<CreateProductDTO>("products", createProductDTO); //HttpClient ile POST isteği gönderilir
        }

        public async Task DeleteProductAsync(string id)
        {
            await _httpClient.DeleteAsync("products?id=" + id); //HttpClient ile DELETE isteği gönderilir
        }

        public async Task<List<ResultProductDTO>> GetAllProductAsync()
        {
            var response = await _httpClient.GetAsync("products"); //HttpClient ile GET isteği gönderilir
            var jsonData = await response.Content.ReadAsStringAsync(); // JSON verisi okunur.
                                                                       //var values = await response.Content.ReadFromJsonAsync<List<ResultProductDTO>>(); //Gelen cevap JSON formatında okunur ve listeye dönüştürülür
            var values = JsonConvert.DeserializeObject<List<ResultProductDTO>>(jsonData); // JSON verisi DTO nesnesine dönüştürülür.
            return values; //Liste döndürülür
        }

        public async Task<UpdateProductDTO> GetByIDProductAsync(string id)
        {
            var response = await _httpClient.GetAsync("products/" + id); //HttpClient ile GET isteği gönderilir
            var values = await response.Content.ReadFromJsonAsync<UpdateProductDTO>(); //Gelen cevap JSON formatında okunur ve listeye dönüştürülür
            return values; //Liste döndürülür
        }

        public async Task<List<ResultProductWithCategoryDTO>> GetProductsWithByCategoryByCategoryIDAsync(string categoryID)
        {
            var response = await _httpClient.GetAsync("products/ProductListWithCategoryByCategoryID/"+categoryID); //HttpClient ile GET isteği gönderilir
            var jsonData = await response.Content.ReadAsStringAsync(); // JSON verisi okunur.
                                                                       //var values = await response.Content.ReadFromJsonAsync<List<ResultProductDTO>>(); //Gelen cevap JSON formatında okunur ve listeye dönüştürülür
            var values = JsonConvert.DeserializeObject<List<ResultProductWithCategoryDTO>>(jsonData); // JSON verisi DTO nesnesine dönüştürülür.
            return values; //Liste döndürülür
        }

        public async Task<List<ResultProductWithCategoryDTO>> GetProductsWithCategoryAsync()
        {
            var responseMessage=await _httpClient.GetAsync("products/ProductListWithCategory"); //HttpClient ile GET isteği gönderilir
            var jsonData = await responseMessage.Content.ReadAsStringAsync(); // JSON verisi okunur. 
            var values=JsonConvert.DeserializeObject<List<ResultProductWithCategoryDTO>>(jsonData); // JSON verisi DTO nesnesine dönüştürülür.
            return values; //Liste döndürülür
        }

        public async Task UpdateProductAsync(UpdateProductDTO updateProductDTO)
        {
            await _httpClient.PutAsJsonAsync<UpdateProductDTO>("products", updateProductDTO); //HttpClient ile PUT isteği gönderilir

        }
    }
}
