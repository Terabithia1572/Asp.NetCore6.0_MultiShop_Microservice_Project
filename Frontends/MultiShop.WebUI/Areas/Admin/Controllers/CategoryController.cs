using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DTOLayer.CatalogDTOs.CategoryDTOs;
using MultiShop.WebUI.Services.CatalogServices.CategoryServices;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")] // Bu Area'nın adı "Admin" olarak ayarlanır. yani URL'de /Admin/ ile başlayacak.
    [Route("Admin/[controller]/[action]")] // Bu controller için rota ayarlanır. Örneğin: /Admin/Category/Index
    public class CategoryController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ICategoryService _categoryService;

        public CategoryController(IHttpClientFactory httpClientFactory, ICategoryService categoryService)
        {
            _httpClientFactory = httpClientFactory;
            _categoryService = categoryService;
        }



        public async Task<IActionResult> Index()
        {

            // Bu ViewBag'ler, view içinde kullanılacak verileri taşır.

            //var client = _httpClientFactory.CreateClient(); // IHttpClientFactory kullanarak HttpClient oluşturulur.
            //var responseMessage = await client.GetAsync("http://localhost:1002/api/Categories"); // API'den kategori verilerini almak için GET isteği yapılır.
            //if (responseMessage.IsSuccessStatusCode) // Eğer istek başarılıysa
            //{
            //    var jsonData = await responseMessage.Content.ReadAsStringAsync(); // JSON verisi okunur.
            //    var values = JsonConvert.DeserializeObject<List<ResultCategoryDTO>>(jsonData); // JSON verisi dinamik bir listeye dönüştürülür.
            //    return View(values); // Dönüştürülen liste view'e gönderilir.
            //}
            CategoryViewBagList();

            var values = await _categoryService.GetAllCategoryAsync();
            return View(values);
        }
        [HttpGet]
        public IActionResult CreateCategory()
        {
            CategoryViewBagList();
            return View(); // Kategori ekleme sayfası için view döndürülür.
        }
        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryDTO createCategoryDTO)
        {
            //var client = _httpClientFactory.CreateClient(); // IHttpClientFactory kullanarak HttpClient oluşturulur.
            //var jsonData = JsonConvert.SerializeObject(createCategoryDTO); // DTO nesnesi JSON formatına dönüştürülür.
            //StringContent content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json"); // JSON verisi StringContent olarak hazırlanır.
            //var responseMessage = await client.PostAsync("https://localhost:1002/api/Categories", content); // API'ye POST isteği yapılır.
            await _categoryService.CreateCategoryAsync(createCategoryDTO);
            return RedirectToAction("Index", "Category", new { area = "Admin" }); // Kategori listesine yönlendirilir.
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> DeleteCategory(string id)
        {
            await _categoryService.DeleteCategoryAsync(id);
            //var client = _httpClientFactory.CreateClient(); // IHttpClientFactory kullanarak HttpClient oluşturulur.
            //var responseMessage = await client.DeleteAsync($"https://localhost:1002/api/Categories?id=" + id); // API'den kategori silme isteği yapılır.
            //if (responseMessage.IsSuccessStatusCode) // Eğer istek başarılıysa
            //{
                return RedirectToAction("Index", "Category", new { area = "Admin" }); // Kategori listesine yönlendirilir.
            
            //return View(); // Başarısız ise aynı view döndürülür.
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> UpdateCategory(string id)
        {
          CategoryViewBagList();
            var values = await _categoryService.GetByIDCategoryAsync(id);

            //var client = _httpClientFactory.CreateClient(); // IHttpClientFactory kullanarak HttpClient oluşturulur.
            //var responseMessage = await client.GetAsync($"https://localhost:1002/api/Categories/{id}"); // API'den kategori verisi alınır.
            //if (responseMessage.IsSuccessStatusCode) // Eğer istek başarılıysa
            //{
            //    var jsonData = await responseMessage.Content.ReadAsStringAsync(); // JSON verisi okunur.
            //    var values = JsonConvert.DeserializeObject<UpdateCategoryDTO>(jsonData); // JSON verisi DTO nesnesine dönüştürülür.
            //    return View(values); // Dönüştürülen DTO nesnesi view'e gönderilir.
            //}
            return View(values); // Başarısız ise aynı view döndürülür.
        }
        [HttpPost("{id}")]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryDTO updateCategoryDTO)
        {
            await _categoryService.UpdateCategoryAsync(updateCategoryDTO);

            //var client = _httpClientFactory.CreateClient(); // IHttpClientFactory kullanarak HttpClient oluşturulur.
            //var jsonData = JsonConvert.SerializeObject(updateCategoryDTO); // DTO nesnesi JSON formatına dönüştürülür.
            //StringContent content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json"); // JSON verisi StringContent olarak hazırlanır.
            //var responseMessage = await client.PutAsync("https://localhost:1002/api/Categories/", content); // API'ye PUT isteği yapılır.
            //if (responseMessage.IsSuccessStatusCode) // Eğer istek başarılıysa
            //{
            return RedirectToAction("Index", "Category", new { area = "Admin" }); // Kategori listesine yönlendirilir.
            
            //return View(); // Başarısız ise aynı view döndürülür.
        }
        void CategoryViewBagList()
        {
            ViewBag.v1 = "Ana Sayfa";
            ViewBag.v2 = "Kategoriler";
            ViewBag.v3 = "Kategori Listesi";
            ViewBag.v4 = "Kategori İşlemleri";
        }

    }
}
