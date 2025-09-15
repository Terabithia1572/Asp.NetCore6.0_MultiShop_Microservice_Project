using MultiShop.DTOLayer.CatalogDTOs.ProductImageDTOs;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Services.CatalogServices.ProductImageServices
{
    public class ProductImageService:IProductImageService
    {
        private readonly HttpClient _httpClient; //HttpClient nesnesi
        public ProductImageService(HttpClient httpClient) //Dependency Injection ile HttpClient nesnesi alınır
        {
            _httpClient = httpClient; //HttpClient nesnesi atanır
        }

        public async Task CreateProductImageAsync(CreateProductImageDTO createProductImageDTO) //Yeni kategori oluşturur
        {
            await _httpClient.PostAsJsonAsync<CreateProductImageDTO>("productimages", createProductImageDTO); //HttpClient ile POST isteği gönderilir

        }

        public async Task DeleteProductImageAsync(string id) //Kategoriyi siler
        {
            await _httpClient.DeleteAsync("productimages?id=" + id); //HttpClient ile DELETE isteği gönderilir
        }



        public async Task<List<ResultProductImageDTO>> GetAllProductImageAsync() //Tüm kategorileri getirir
        {

            var response = await _httpClient.GetAsync("productimages"); //HttpClient ile GET isteği gönderilir
            var jsonData = await response.Content.ReadAsStringAsync(); // JSON verisi okunur.
                                                                       //var values = await response.Content.ReadFromJsonAsync<List<ResultProductImageDTO>>(); //Gelen cevap JSON formatında okunur ve listeye dönüştürülür
            var values = JsonConvert.DeserializeObject<List<ResultProductImageDTO>>(jsonData); // JSON verisi DTO nesnesine dönüştürülür.
            return values; //Liste döndürülür

        }

        public async Task<GetByIDProductImageDTO> GetByIDProductImageAsync(string id) //ID ile kategori getirir
        {
            var response = await _httpClient.GetAsync("productimages/" + id); //HttpClient ile GET isteği gönderilir
            var values = await response.Content.ReadFromJsonAsync<GetByIDProductImageDTO>(); //Gelen cevap JSON formatında okunur ve listeye dönüştürülür
            return values; //Liste döndürülür
        }

        public async Task<GetByIDProductImageDTO> GetByProductIDProductImageAsync(string productId)
        {
            var response = await _httpClient.GetAsync("productimages/ProductImagesByProductID/" + productId); //HttpClient ile GET isteği gönderilir
            var values = await response.Content.ReadFromJsonAsync<GetByIDProductImageDTO>(); //Gelen cevap JSON formatında okunur ve listeye dönüştürülür
            return values; //Liste döndürülür
        }

        public async Task UpdateProductImageAsync(UpdateProductImageDTO updateProductImageDTO) //Kategoriyi günceller
        {
            await _httpClient.PutAsJsonAsync<UpdateProductImageDTO>("productimages", updateProductImageDTO); //HttpClient ile PUT isteği gönderilir

        }

    }
}
