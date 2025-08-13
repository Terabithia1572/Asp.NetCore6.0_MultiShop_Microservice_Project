using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DTOLayer.CatalogDTOs.ProductDTOs;
using Newtonsoft.Json;
using System.Net.Http;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [AllowAnonymous]
    [Area("Admin")] // Bu Area'nın adı "Admin" olarak ayarlanır. yani URL'de /Admin/ ile başlayacak.
    [Route("Admin/[controller]/[action]")] // Bu controller için rota ayarlanır. Örneğin: /Admin/Product/Index
    public class ProductController : Controller
    {
      
        private readonly IHttpClientFactory _httpClientFactory;

        public ProductController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.v1 = "Ana Sayfa";
            ViewBag.v2 = "Ürünler";
            ViewBag.v3 = "Ürünler Listesi";
            ViewBag.v4 = "Ürün İşlemleri";
            // Bu ViewBag'ler, view içinde kullanılacak verileri taşır.

            var client = _httpClientFactory.CreateClient(); // IHttpClientFactory kullanarak HttpClient oluşturulur.
            var responseMessage = await client.GetAsync("https://localhost:1002/api/Products"); // API'den ürün verilerini almak için GET isteği yapılır.
            if (responseMessage.IsSuccessStatusCode) // Eğer istek başarılıysa
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync(); // JSON verisi okunur.
                var values = JsonConvert.DeserializeObject<List<ResultProductDTO>>(jsonData); // JSON verisi dinamik bir listeye dönüştürülür.
                return View(values); // Dönüştürülen liste view'e gönderilir.
            }
            return View(); // Eğer istek başarısızsa, boş bir view döndürülür.
        }
        [HttpGet]
        public IActionResult CreateProduct()
        {
            ViewBag.v1 = "Ana Sayfa";
            ViewBag.v2 = "Ürünler";
            ViewBag.v3 = "Yeni Ürün Ekleme";
            ViewBag.v4 = "Ürün İşlemleri";
            return View(); // Ürün ekleme sayfası için view döndürülür.
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductDTO createProductDTO)
        {
            var client = _httpClientFactory.CreateClient(); // IHttpClientFactory kullanarak HttpClient oluşturulur.
            var jsonData = JsonConvert.SerializeObject(createProductDTO); // DTO nesnesi JSON formatına dönüştürülür.
            StringContent content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json"); // JSON verisi StringContent olarak hazırlanır.
            var responseMessage = await client.PostAsync("https://localhost:1002/api/Products", content); // API'ye POST isteği yapılır.
            if (responseMessage.IsSuccessStatusCode) // Eğer istek başarılıysa
            {
                return RedirectToAction("Index", "Product", new { area = "Admin" }); // Ürün listesine yönlendirilir.
            }
            return View(); // Başarısız ise aynı view döndürülür.
        }
    }
}
