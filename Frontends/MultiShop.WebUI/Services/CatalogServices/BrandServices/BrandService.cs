using MultiShop.DTOLayer.CatalogDTOs.BrandDTOs;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Services.CatalogServices.BrandServices
{
    public class BrandService:IBrandService
    {
        private readonly HttpClient _httpClient; //HttpClient nesnesi
        public BrandService(HttpClient httpClient) //Dependency Injection ile HttpClient nesnesi alınır
        {
            _httpClient = httpClient; //HttpClient nesnesi atanır
        }

        public async Task CreateBrandAsync(CreateBrandDTO createBrandDTO) //Yeni kategori oluşturur
        {
            await _httpClient.PostAsJsonAsync<CreateBrandDTO>("brands", createBrandDTO); //HttpClient ile POST isteği gönderilir

        }

        public async Task DeleteBrandAsync(string id) //Kategoriyi siler
        {
            await _httpClient.DeleteAsync("brands?id=" + id); //HttpClient ile DELETE isteği gönderilir
        }



        public async Task<List<ResultBrandDTO>> GetAllBrandAsync() //Tüm kategorileri getirir
        {

            var response = await _httpClient.GetAsync("brands"); //HttpClient ile GET isteği gönderilir
            var jsonData = await response.Content.ReadAsStringAsync(); // JSON verisi okunur.
                                                                       //var values = await response.Content.ReadFromJsonAsync<List<ResultBrandDTO>>(); //Gelen cevap JSON formatında okunur ve listeye dönüştürülür
            var values = JsonConvert.DeserializeObject<List<ResultBrandDTO>>(jsonData); // JSON verisi DTO nesnesine dönüştürülür.
            return values; //Liste döndürülür

        }

        public async Task<UpdateBrandDTO> GetByIDBrandAsync(string id) //ID ile kategori getirir
        {
            var response = await _httpClient.GetAsync("brands/" + id); //HttpClient ile GET isteği gönderilir
            var values = await response.Content.ReadFromJsonAsync<UpdateBrandDTO>(); //Gelen cevap JSON formatında okunur ve listeye dönüştürülür
            return values; //Liste döndürülür
        }

        public async Task UpdateBrandAsync(UpdateBrandDTO updateBrandDTO) //Kategoriyi günceller
        {
            await _httpClient.PutAsJsonAsync<UpdateBrandDTO>("brands", updateBrandDTO); //HttpClient ile PUT isteği gönderilir

        }
    }
}
