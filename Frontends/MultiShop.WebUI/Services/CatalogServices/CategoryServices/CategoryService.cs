using MultiShop.DTOLayer.CatalogDTOs.CategoryDTOs;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Services.CatalogServices.CategoryServices
{
    public class CategoryService : ICategoryService //ICategoryService arayüzünü implemente eden CategoryService sınıfı
    {
        private readonly HttpClient _httpClient; //HttpClient nesnesi
        public CategoryService(HttpClient httpClient) //Dependency Injection ile HttpClient nesnesi alınır
        {
            _httpClient = httpClient; //HttpClient nesnesi atanır
        }

        public async Task CreateCategoryAsync(CreateCategoryDTO createCategoryDTO) //Yeni kategori oluşturur
        {
            var response =await _httpClient.PostAsJsonAsync<CreateCategoryDTO>("categories", createCategoryDTO); //HttpClient ile POST isteği gönderilir

        }

        public async Task DeleteCategoryAsync(string id) //Kategoriyi siler
        {
           await _httpClient.DeleteAsync("categories?id="+id); //HttpClient ile DELETE isteği gönderilir
        }

        public async Task<List<ResultCategoryDTO>> GetAllCategoryAsync() //Tüm kategorileri getirir
        {
            /*
               
            if (responseMessage.IsSuccessStatusCode) // Eğer istek başarılıysa
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync(); // JSON verisi okunur.
                var values = JsonConvert.DeserializeObject<UpdateCategoryDTO>(jsonData); // JSON verisi DTO nesnesine dönüştürülür.
                return View(values); // Dönüştürülen DTO nesnesi view'e gönderilir.
            }
             */
            var response = await _httpClient.GetAsync("categories"); //HttpClient ile GET isteği gönderilir
            var jsonData = await response.Content.ReadAsStringAsync(); // JSON verisi okunur.
                                                                       //var values = await response.Content.ReadFromJsonAsync<List<ResultCategoryDTO>>(); //Gelen cevap JSON formatında okunur ve listeye dönüştürülür
            var values = JsonConvert.DeserializeObject<List<ResultCategoryDTO>>(jsonData); // JSON verisi DTO nesnesine dönüştürülür.
            return values; //Liste döndürülür

        }

        public async Task<GetByIDCategoryDTO> GetByIDCategoryAsync(string id) //ID ile kategori getirir
        {
            var response = await _httpClient.GetAsync("categories/" + id); //HttpClient ile GET isteği gönderilir
            var values = await response.Content.ReadFromJsonAsync<GetByIDCategoryDTO>(); //Gelen cevap JSON formatında okunur ve listeye dönüştürülür
            return values; //Liste döndürülür
        }

        public async Task UpdateCategoryAsync(UpdateCategoryDTO updateCategoryDTO) //Kategoriyi günceller
        {
           await _httpClient.PutAsJsonAsync<UpdateCategoryDTO>("categories", updateCategoryDTO); //HttpClient ile PUT isteği gönderilir

        }
    }
}
