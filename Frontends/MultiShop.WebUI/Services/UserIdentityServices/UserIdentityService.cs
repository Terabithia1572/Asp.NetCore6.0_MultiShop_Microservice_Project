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
        public async Task<(bool ok, string message)> UpdateUserAsyncMultipart(
     string id, string name, string surname, string email, string phoneNumber,
     string city, string gender, string about, string? newPassword,
     IFormFile? profileImage)
        {
            using var form = new MultipartFormDataContent();
            form.Add(new StringContent(id ?? ""), "Id");
            form.Add(new StringContent(name ?? ""), "Name");
            form.Add(new StringContent(surname ?? ""), "Surname");
            form.Add(new StringContent(email ?? ""), "Email");
            form.Add(new StringContent(phoneNumber ?? ""), "PhoneNumber");
            form.Add(new StringContent(city ?? ""), "City");
            form.Add(new StringContent(gender ?? ""), "Gender");
            form.Add(new StringContent(about ?? ""), "About");
            if (!string.IsNullOrWhiteSpace(newPassword))
                form.Add(new StringContent(newPassword), "NewPassword");

            if (profileImage is not null && profileImage.Length > 0)
            {
                var stream = profileImage.OpenReadStream();
                var fileContent = new StreamContent(stream);
                fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(profileImage.ContentType ?? "application/octet-stream");
                form.Add(fileContent, "ProfileImage", profileImage.FileName);
            }

            var resp = await _httpClient.PutAsync("http://localhost:5001/api/users/UpdateUser", form);
            var body = await resp.Content.ReadAsStringAsync();

            return resp.IsSuccessStatusCode
                ? (true, body)
                : (false, body);
        }

    }

}

