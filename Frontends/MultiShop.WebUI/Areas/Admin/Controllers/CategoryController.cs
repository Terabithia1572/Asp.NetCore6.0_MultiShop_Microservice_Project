using Microsoft.AspNetCore.Mvc;
using MultiShop.DTOLayer.CatalogDTOs.CategoryDTOs;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")] // Bu Area'nın adı "Admin" olarak ayarlanır. yani URL'de /Admin/ ile başlayacak.
    public class CategoryController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CategoryController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task< IActionResult> Index()
        {
            ViewBag.v1="Ana Sayfa"; 
            ViewBag.v2="Kategoriler"; 
            ViewBag.v3="Kategori Listesi";
            ViewBag.v4="Kategori İşlemleri";
            // Bu ViewBag'ler, view içinde kullanılacak verileri taşır.

            var client = _httpClientFactory.CreateClient(); // IHttpClientFactory kullanarak HttpClient oluşturulur.
            var responseMessage=await client.GetAsync("https://localhost:1002/api/Categories"); // API'den kategori verilerini almak için GET isteği yapılır.
            if (responseMessage.IsSuccessStatusCode) // Eğer istek başarılıysa
            {
                var jsonData=await responseMessage.Content.ReadAsStringAsync(); // JSON verisi okunur.
                var values= JsonConvert.DeserializeObject<List<ResultCategoryDTO>>(jsonData); // JSON verisi dinamik bir listeye dönüştürülür.
                return View(values); // Dönüştürülen liste view'e gönderilir.
            }
            return View();
        }
    }
}
