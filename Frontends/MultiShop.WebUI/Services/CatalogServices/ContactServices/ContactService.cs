using MultiShop.DTOLayer.CatalogDTOs.ContactDTOs;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Services.CatalogServices.ContactServices
{
    public class ContactService:IContactService
    {
        private readonly HttpClient _httpClient; //HttpClient nesnesi
        public ContactService(HttpClient httpClient) //Dependency Injection ile HttpClient nesnesi alınır
        {
            _httpClient = httpClient; //HttpClient nesnesi atanır
        }

        public async Task CreateContactAsync(CreateContactDTO createContactDTO) //Yeni kategori oluşturur
        {
            await _httpClient.PostAsJsonAsync<CreateContactDTO>("contacts", createContactDTO); //HttpClient ile POST isteği gönderilir

        }

        public async Task DeleteContactAsync(string id) //Kategoriyi siler
        {
            await _httpClient.DeleteAsync("contacts?id=" + id); //HttpClient ile DELETE isteği gönderilir
        }



        public async Task<List<ResultContactDTO>> GetAllContactAsync() //Tüm kategorileri getirir
        {

            var response = await _httpClient.GetAsync("contacts"); //HttpClient ile GET isteği gönderilir
            var jsonData = await response.Content.ReadAsStringAsync(); // JSON verisi okunur.
                                                                       //var values = await response.Content.ReadFromJsonAsync<List<ResultContactDTO>>(); //Gelen cevap JSON formatında okunur ve listeye dönüştürülür
            var values = JsonConvert.DeserializeObject<List<ResultContactDTO>>(jsonData); // JSON verisi DTO nesnesine dönüştürülür.
            return values; //Liste döndürülür

        }

        public async Task<GetByIDContactDTO> GetByIDContactAsync(string id) //ID ile kategori getirir
        {
            var response = await _httpClient.GetAsync("contacts/" + id); //HttpClient ile GET isteği gönderilir
            var values = await response.Content.ReadFromJsonAsync<GetByIDContactDTO>(); //Gelen cevap JSON formatında okunur ve listeye dönüştürülür
            return values; //Liste döndürülür
        }

        public async Task UpdateContactAsync(UpdateContactDTO updateContactDTO) //Kategoriyi günceller
        {
            await _httpClient.PutAsJsonAsync<UpdateContactDTO>("contacts", updateContactDTO); //HttpClient ile PUT isteği gönderilir

        }
    }
}
