using MultiShop.DTOLayer.IdentityDTOs.UserDTOs;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Services.UserIdentityServices
{
    public class UserIdentityService : IUserIdentityService
    {
        private readonly HttpClient _httpClient; //HttpClient nesnesi
        public UserIdentityService(HttpClient httpClient) //Dependency Injection ile HttpClient nesnesi alınır
        {
            _httpClient = httpClient; //HttpClient nesnesi atanır
        }

        public async Task<List<ResultUserDTO>> GetAllUserListAsync() //Tüm kategorileri getirir
        {

            var response = await _httpClient.GetAsync("http://localhost:5001/api/users/GetAllUserList"); //HttpClient ile GET isteği gönderilir
            var jsonData = await response.Content.ReadAsStringAsync(); // JSON verisi okunur.
                                                                       //var values = await response.Content.ReadFromJsonAsync<List<ResultAboutDTO>>(); //Gelen cevap JSON formatında okunur ve listeye dönüştürülür
            var values = JsonConvert.DeserializeObject<List<ResultUserDTO>>(jsonData); // JSON verisi DTO nesnesine dönüştürülür.
            return values; //Liste döndürülür

        }
    }
}
