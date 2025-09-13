using MultiShop.DTOLayer.CatalogDTOs.AboutDTOs;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Services.CatalogServices.AboutServices
{
    public class AboutService:IAboutService
    {
        private readonly HttpClient _httpClient; //HttpClient nesnesi
        public AboutService(HttpClient httpClient) //Dependency Injection ile HttpClient nesnesi alınır
        {
            _httpClient = httpClient; //HttpClient nesnesi atanır
        }

        public async Task CreateAboutAsync(CreateAboutDTO createAboutDTO) //Yeni kategori oluşturur
        {
            await _httpClient.PostAsJsonAsync<CreateAboutDTO>("abouts", createAboutDTO); //HttpClient ile POST isteği gönderilir

        }

        public async Task DeleteAboutAsync(string id) //Kategoriyi siler
        {
            await _httpClient.DeleteAsync("abouts?id=" + id); //HttpClient ile DELETE isteği gönderilir
        }



        public async Task<List<ResultAboutDTO>> GetAllAboutAsync() //Tüm kategorileri getirir
        {

            var response = await _httpClient.GetAsync("abouts"); //HttpClient ile GET isteği gönderilir
            var jsonData = await response.Content.ReadAsStringAsync(); // JSON verisi okunur.
                                                                       //var values = await response.Content.ReadFromJsonAsync<List<ResultAboutDTO>>(); //Gelen cevap JSON formatında okunur ve listeye dönüştürülür
            var values = JsonConvert.DeserializeObject<List<ResultAboutDTO>>(jsonData); // JSON verisi DTO nesnesine dönüştürülür.
            return values; //Liste döndürülür

        }

        public async Task<UpdateAboutDTO> GetByIDAboutAsync(string id) //ID ile kategori getirir
        {
            var response = await _httpClient.GetAsync("abouts/" + id); //HttpClient ile GET isteği gönderilir
            var values = await response.Content.ReadFromJsonAsync<UpdateAboutDTO>(); //Gelen cevap JSON formatında okunur ve listeye dönüştürülür
            return values; //Liste döndürülür
        }

        public async Task UpdateAboutAsync(UpdateAboutDTO updateAboutDTO) //Kategoriyi günceller
        {
            await _httpClient.PutAsJsonAsync<UpdateAboutDTO>("abouts", updateAboutDTO); //HttpClient ile PUT isteği gönderilir

        }
    }
}
