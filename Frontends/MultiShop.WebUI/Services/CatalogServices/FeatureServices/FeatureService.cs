using MultiShop.DTOLayer.CatalogDTOs.FeatureDTOs;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Services.CatalogServices.FeatureServices
{
    public class FeatureService:IFeatureService
    {
        private readonly HttpClient _httpClient; //HttpClient nesnesi
        public FeatureService(HttpClient httpClient) //Dependency Injection ile HttpClient nesnesi alınır
        {
            _httpClient = httpClient; //HttpClient nesnesi atanır
        }

        public async Task CreateFeatureAsync(CreateFeatureDTO createFeatureDTO) //Yeni kategori oluşturur
        {
            await _httpClient.PostAsJsonAsync<CreateFeatureDTO>("features", createFeatureDTO); //HttpClient ile POST isteği gönderilir

        }

        public async Task DeleteFeatureAsync(string id) //Kategoriyi siler
        {
            await _httpClient.DeleteAsync("features?id=" + id); //HttpClient ile DELETE isteği gönderilir
        }

       

        public async Task<List<ResultFeatureDTO>> GetAllFeatureAsync() //Tüm kategorileri getirir
        {

            var response = await _httpClient.GetAsync("features"); //HttpClient ile GET isteği gönderilir
            var jsonData = await response.Content.ReadAsStringAsync(); // JSON verisi okunur.
                                                                       //var values = await response.Content.ReadFromJsonAsync<List<ResultFeatureDTO>>(); //Gelen cevap JSON formatında okunur ve listeye dönüştürülür
            var values = JsonConvert.DeserializeObject<List<ResultFeatureDTO>>(jsonData); // JSON verisi DTO nesnesine dönüştürülür.
            return values; //Liste döndürülür

        }

        public async Task<UpdateFeatureDTO> GetByIDFeatureAsync(string id) //ID ile kategori getirir
        {
            var response = await _httpClient.GetAsync("features/" + id); //HttpClient ile GET isteği gönderilir
            var values = await response.Content.ReadFromJsonAsync<UpdateFeatureDTO>(); //Gelen cevap JSON formatında okunur ve listeye dönüştürülür
            return values; //Liste döndürülür
        }

        public async Task UpdateFeatureAsync(UpdateFeatureDTO updateFeatureDTO) //Kategoriyi günceller
        {
            await _httpClient.PutAsJsonAsync<UpdateFeatureDTO>("features", updateFeatureDTO); //HttpClient ile PUT isteği gönderilir

        }
    }
}
