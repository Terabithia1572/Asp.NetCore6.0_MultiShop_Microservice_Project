using MultiShop.DTOLayer.CatalogDTOs.ProductDetailDTOs;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Services.CatalogServices.ProductDetailServices
{
    public class ProductDetailService : IProductDetailService
    {
        private readonly HttpClient _httpClient; //HttpClient nesnesi
        public ProductDetailService(HttpClient httpClient) //Dependency Injection ile HttpClient nesnesi alınır
        {
            _httpClient = httpClient; //HttpClient nesnesi atanır
        }

        public async Task CreateProductDetailAsync(CreateProductDetailDTO createProductDetailDTO) //Yeni kategori oluşturur
        {
            await _httpClient.PostAsJsonAsync<CreateProductDetailDTO>("productdetails", createProductDetailDTO); //HttpClient ile POST isteği gönderilir

        }

        public async Task DeleteProductDetailAsync(string id) //Kategoriyi siler
        {
            await _httpClient.DeleteAsync("productdetails?id=" + id); //HttpClient ile DELETE isteği gönderilir
        }



        public async Task<List<ResultProductDetailDTO>> GetAllProductDetailAsync() //Tüm kategorileri getirir
        {

            var response = await _httpClient.GetAsync("productdetails"); //HttpClient ile GET isteği gönderilir
            var jsonData = await response.Content.ReadAsStringAsync(); // JSON verisi okunur.
                                                                       //var values = await response.Content.ReadFromJsonAsync<List<ResultProductDetailDTO>>(); //Gelen cevap JSON formatında okunur ve listeye dönüştürülür
            var values = JsonConvert.DeserializeObject<List<ResultProductDetailDTO>>(jsonData); // JSON verisi DTO nesnesine dönüştürülür.
            return values; //Liste döndürülür

        }

        public async Task<UpdateProductDetailDTO> GetByIDProductDetailAsync(string id) //ID ile kategori getirir
        {
            var response = await _httpClient.GetAsync("productdetails/" + id); //HttpClient ile GET isteği gönderilir
            var values = await response.Content.ReadFromJsonAsync<UpdateProductDetailDTO>(); //Gelen cevap JSON formatında okunur ve listeye dönüştürülür
            return values; //Liste döndürülür
        }

        public async Task<UpdateProductDetailDTO> GetByProductIDDetailAsync(string id)
        {
            var response = await _httpClient.GetAsync("productdetails/GetProductDetailByProductID/" + id); //HttpClient ile GET isteği gönderilir
            var values = await response.Content.ReadFromJsonAsync<UpdateProductDetailDTO>(); //Gelen cevap JSON formatında okunur ve listeye dönüştürülür
            return values; //Liste döndürülür
        }

        public async Task UpdateProductDetailAsync(UpdateProductDetailDTO updateProductDetailDTO) //Kategoriyi günceller
        {
            await _httpClient.PutAsJsonAsync<UpdateProductDetailDTO>("productdetails", updateProductDetailDTO); //HttpClient ile PUT isteği gönderilir

        }
    }
}
