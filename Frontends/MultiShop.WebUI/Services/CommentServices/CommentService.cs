using MultiShop.DTOLayer.CommentDTOs;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Services.CommentServices
{
    public class CommentService:ICommentService
    {
        private readonly HttpClient _httpClient; //HttpClient nesnesi
        public CommentService(HttpClient httpClient) //Dependency Injection ile HttpClient nesnesi alınır
        {
            _httpClient = httpClient; //HttpClient nesnesi atanır
        }

        public async Task CreateCommentAsync(CreateCommentDTO createCommentDTO) //Yeni kategori oluşturur
        {
            await _httpClient.PostAsJsonAsync<CreateCommentDTO>("comments", createCommentDTO); //HttpClient ile POST isteği gönderilir

        }

        public async Task DeleteCommentAsync(string id) //Kategoriyi siler
        {
            await _httpClient.DeleteAsync("comments?id=" + id); //HttpClient ile DELETE isteği gönderilir
        }

       

        public async Task<List<ResultCommentDTO>> GetAllCommentAsync() //Tüm kategorileri getirir
        {

            var response = await _httpClient.GetAsync("comments"); //HttpClient ile GET isteği gönderilir
            var jsonData = await response.Content.ReadAsStringAsync(); // JSON verisi okunur.
                                                                       //var values = await response.Content.ReadFromJsonAsync<List<ResultCommentDTO>>(); //Gelen cevap JSON formatında okunur ve listeye dönüştürülür
            var values = JsonConvert.DeserializeObject<List<ResultCommentDTO>>(jsonData); // JSON verisi DTO nesnesine dönüştürülür.
            return values; //Liste döndürülür

        }

        public async Task<UpdateCommentDTO> GetByIDCommentAsync(string id) //ID ile kategori getirir
        {
            var response = await _httpClient.GetAsync("comments/" + id); //HttpClient ile GET isteği gönderilir
            var values = await response.Content.ReadFromJsonAsync<UpdateCommentDTO>(); //Gelen cevap JSON formatında okunur ve listeye dönüştürülür
            return values; //Liste döndürülür
        }

        public async Task<List<ResultCommentDTO>> GetCommentsByProductId(string id)
        {
            var responseMessage = await _httpClient.GetAsync($"comments/GetCommentsByProductId/{id}");
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultCommentDTO>>(jsonData);
            return values;
        }

        public async Task<int> GetTotalCommentCount()
        {
            var response = await _httpClient.GetAsync("comments/GetTotalCommentCount");
            var content = await response.Content.ReadAsStringAsync();
            return int.TryParse(content, out var value) ? value : 0;
        }

        public async Task<int> GetActiveCommentCount()
        {
            var response = await _httpClient.GetAsync("comments/GetActiveCommentCount");
            var content = await response.Content.ReadAsStringAsync();
            return int.TryParse(content, out var value) ? value : 0;
        }

        public async Task<int> GetPassiveCommentCount()
        {
            var response = await _httpClient.GetAsync("comments/GetPassiveCommentCount");
            var content = await response.Content.ReadAsStringAsync();
            return int.TryParse(content, out var value) ? value : 0;
        }


        public async Task UpdateCommentAsync(UpdateCommentDTO updateCommentDTO) //Kategoriyi günceller
        {
            await _httpClient.PutAsJsonAsync<UpdateCommentDTO>("comments", updateCommentDTO); //HttpClient ile PUT isteği gönderilir

        }

        public async Task<List<ResultCommentDTO>> GetCommentsByUserIdAsync(string userId)
        {
            var response = await _httpClient.GetAsync($"comments/GetCommentsByUserId/{userId}");
            if (!response.IsSuccessStatusCode)
                return new List<ResultCommentDTO>();

            var jsonData = await response.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultCommentDTO>>(jsonData);
            return values ?? new List<ResultCommentDTO>();
        }

        public async Task<ResultCommentDTO> GetCommentByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"comments/{id}");
            if (!response.IsSuccessStatusCode)
                return null;

            var jsonData = await response.Content.ReadAsStringAsync();
            var value = JsonConvert.DeserializeObject<ResultCommentDTO>(jsonData);
            return value;
        }

    }
}
