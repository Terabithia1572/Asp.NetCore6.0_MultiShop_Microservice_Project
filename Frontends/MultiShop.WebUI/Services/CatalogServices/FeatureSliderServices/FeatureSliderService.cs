using MultiShop.DTOLayer.CatalogDTOs.FeatureSliderDTOs;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Services.CatalogServices.FeatureSliderServices
{
    public class FeatureSliderService: IFeatureSliderService
    {
        private readonly HttpClient _httpClient; //HttpClient nesnesi
        public FeatureSliderService(HttpClient httpClient) //Dependency Injection ile HttpClient nesnesi alınır
        {
            _httpClient = httpClient; //HttpClient nesnesi atanır
        }

        public async Task CreateFeatureSliderAsync(CreateFeatureSliderDTO createFeatureSliderDTO) //Yeni kategori oluşturur
        {
            await _httpClient.PostAsJsonAsync<CreateFeatureSliderDTO>("featuresliders", createFeatureSliderDTO); //HttpClient ile POST isteği gönderilir

        }

        public async Task DeleteFeatureSliderAsync(string id) //Kategoriyi siler
        {
            await _httpClient.DeleteAsync("featuresliders?id=" + id); //HttpClient ile DELETE isteği gönderilir
        }

        public Task FeatureSliderChangeStatusToFalse(string id)
        {
            throw new NotImplementedException();
        }

        public Task FeatureSliderChangeStatusToTrue(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ResultFeatureSliderDTO>> GetAllFeatureSliderAsync() //Tüm kategorileri getirir
        {
            /*
               
            if (responseMessage.IsSuccessStatusCode) // Eğer istek başarılıysa
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync(); // JSON verisi okunur.
                var values = JsonConvert.DeserializeObject<UpdateFeatureSliderDTO>(jsonData); // JSON verisi DTO nesnesine dönüştürülür.
                return View(values); // Dönüştürülen DTO nesnesi view'e gönderilir.
            }
             */
            var response = await _httpClient.GetAsync("featuresliders"); //HttpClient ile GET isteği gönderilir
            var jsonData = await response.Content.ReadAsStringAsync(); // JSON verisi okunur.
                                                                       //var values = await response.Content.ReadFromJsonAsync<List<ResultFeatureSliderDTO>>(); //Gelen cevap JSON formatında okunur ve listeye dönüştürülür
            var values = JsonConvert.DeserializeObject<List<ResultFeatureSliderDTO>>(jsonData); // JSON verisi DTO nesnesine dönüştürülür.
            return values; //Liste döndürülür

        }

        public async Task<UpdateFeatureSliderDTO> GetByIDFeatureSliderAsync(string id) //ID ile kategori getirir
        {
            var response = await _httpClient.GetAsync("featuresliders/" + id); //HttpClient ile GET isteği gönderilir
            var values = await response.Content.ReadFromJsonAsync<UpdateFeatureSliderDTO>(); //Gelen cevap JSON formatında okunur ve listeye dönüştürülür
            return values; //Liste döndürülür
        }

        public async Task UpdateFeatureSliderAsync(UpdateFeatureSliderDTO updateFeatureSliderDTO) //Kategoriyi günceller
        {
            await _httpClient.PutAsJsonAsync<UpdateFeatureSliderDTO>("featuresliders", updateFeatureSliderDTO); //HttpClient ile PUT isteği gönderilir

        }
    }
}
