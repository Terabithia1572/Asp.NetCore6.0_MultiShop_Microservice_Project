using MultiShop.DTOLayer.CatalogDTOs.SpecialOfferDTOs;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Services.CatalogServices.SpecialOfferServices
{
    public class SpecialOfferService: ISpecialOfferService
    {
        private readonly HttpClient _httpClient; //HttpClient nesnesi
        public SpecialOfferService(HttpClient httpClient) //Dependency Injection ile HttpClient nesnesi alınır
        {
            _httpClient = httpClient; //HttpClient nesnesi atanır
        }

        public async Task CreateSpecialOfferAsync(CreateSpecialOfferDTO createSpecialOfferDTO) //Yeni özel teklifler oluşturur
        {
            await _httpClient.PostAsJsonAsync<CreateSpecialOfferDTO>("specialoffers", createSpecialOfferDTO); //HttpClient ile POST isteği gönderilir

        }

        public async Task DeleteSpecialOfferAsync(string id) //Kategoriyi siler
        {
            await _httpClient.DeleteAsync("specialoffers?id=" + id); //HttpClient ile DELETE isteği gönderilir
        }

        public async Task<List<ResultSpecialOfferDTO>> GetAllSpecialOfferAsync() //Tüm özel tekliflerleri getirir
        {
            /*
               
            if (responseMessage.IsSuccessStatusCode) // Eğer istek başarılıysa
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync(); // JSON verisi okunur.
                var values = JsonConvert.DeserializeObject<UpdateSpecialOfferDTO>(jsonData); // JSON verisi DTO nesnesine dönüştürülür.
                return View(values); // Dönüştürülen DTO nesnesi view'e gönderilir.
            }
             */
            var response = await _httpClient.GetAsync("specialoffers"); //HttpClient ile GET isteği gönderilir
            var jsonData = await response.Content.ReadAsStringAsync(); // JSON verisi okunur.
                                                                       //var values = await response.Content.ReadFromJsonAsync<List<ResultSpecialOfferDTO>>(); //Gelen cevap JSON formatında okunur ve listeye dönüştürülür
            var values = JsonConvert.DeserializeObject<List<ResultSpecialOfferDTO>>(jsonData); // JSON verisi DTO nesnesine dönüştürülür.
            return values; //Liste döndürülür

        }

        public async Task<UpdateSpecialOfferDTO> GetByIDSpecialOfferAsync(string id) //ID ile özel teklifler getirir
        {
            var response = await _httpClient.GetAsync("specialoffers/" + id); //HttpClient ile GET isteği gönderilir
            var values = await response.Content.ReadFromJsonAsync<UpdateSpecialOfferDTO>(); //Gelen cevap JSON formatında okunur ve listeye dönüştürülür
            return values; //Liste döndürülür
        }

        public async Task UpdateSpecialOfferAsync(UpdateSpecialOfferDTO updateSpecialOfferDTO) //Kategoriyi günceller
        {
            await _httpClient.PutAsJsonAsync<UpdateSpecialOfferDTO>("specialoffers", updateSpecialOfferDTO); //HttpClient ile PUT isteği gönderilir

        }
    }
}
