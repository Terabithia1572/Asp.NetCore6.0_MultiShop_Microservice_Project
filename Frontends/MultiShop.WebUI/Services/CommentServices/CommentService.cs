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
            var response = await _httpClient.GetAsync("comments/GetCommentsByProductId/" + id); //HttpClient ile GET isteği gönderilir
            var jsonData = await response.Content.ReadAsStringAsync(); // JSON verisi okunur.
            var values = JsonConvert.DeserializeObject<List<ResultCommentDTO>>(jsonData); //Gelen cevap JSON formatında okunur ve listeye dönüştürülür
            return values; //Liste döndürülür
        }

        public async Task UpdateCommentAsync(UpdateCommentDTO updateCommentDTO) //Kategoriyi günceller
        {
            await _httpClient.PutAsJsonAsync<UpdateCommentDTO>("comments", updateCommentDTO); //HttpClient ile PUT isteği gönderilir

        }
    }
}
