using MultiShop.DTOLayer.IdentityDTOs.UserDTOs;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace MultiShop.WebUI.Services.UserIdentityServices
{
    public class UserIdentityService : IUserIdentityService
    {
        private readonly HttpClient _httpClient;

        public UserIdentityService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // 🔹 Tüm kullanıcıları getir
        public async Task<List<ResultUserDTO>> GetAllUserListAsync()
        {
            var response = await _httpClient.GetAsync("http://localhost:5001/api/users/GetAllUserList");
            var jsonData = await response.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultUserDTO>>(jsonData);
            return values;
        }

        // 🔹 ID’ye göre kullanıcı getir
        public async Task<ResultUserDTO> GetUserByIdAsync(string id)
        {
            var response = await _httpClient.GetAsync($"http://localhost:5001/api/users/GetUserById?id={id}");
            if (!response.IsSuccessStatusCode)
                return null;

            var jsonData = await response.Content.ReadAsStringAsync();
            var value = JsonConvert.DeserializeObject<ResultUserDTO>(jsonData);
            return value;
        }


        // 🔹 Kullanıcı bilgilerini güncelle
        // 🔹 Kullanıcı güncelleme (FormData ile PUT)
        public async Task<bool> UpdateUserAsync(UpdateUserDTO dto)
        {
            var json = JsonConvert.SerializeObject(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var resp = await _httpClient.PutAsync("http://localhost:5001/api/users/UpdateUser", content);
            return resp.IsSuccessStatusCode;
        }

    }

}

