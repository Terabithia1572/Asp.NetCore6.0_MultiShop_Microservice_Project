using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DTOLayer.CatalogDTOs.BrandDTOs;
using MultiShop.WebUI.Services.CatalogServices.BrandServices;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")] // Bu controller'ın admin alanında olduğunu belirtir.
    [Route("Admin/[controller]/[action]")] // Bu controller için rota ayarlanır. Örneğin: /Admin/Brand/Index
    //[AllowAnonymous] // Bu controller'a anonim erişime izin verir.
    public class BrandController : Controller
    {
        //private readonly IHttpClientFactory _httpClientFactory;

        //public BrandController(IHttpClientFactory httpClientFactory)
        //{
        //    _httpClientFactory = httpClientFactory;
        //}
        private readonly IBrandService _brandService;

        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        public async Task<IActionResult> Index()
        {
            //ViewBag.v1 = "Ana Sayfa";
            //ViewBag.v2 = "Markaler";
            //ViewBag.v3 = "Marka Listesi";
            //ViewBag.v4 = "Marka İşlemleri";
            //// Bu ViewBag'ler, view içinde kullanılacak verileri taşır.

            //var client = _httpClientFactory.CreateClient(); // IHttpClientFactory kullanarak HttpClient oluşturulur.
            //var responseMessage = await client.GetAsync("https://localhost:1002/api/Brands"); // API'den kategori verilerini almak için GET isteği yapılır.
            //if (responseMessage.IsSuccessStatusCode) // Eğer istek başarılıysa
            //{
            //    var jsonData = await responseMessage.Content.ReadAsStringAsync(); // JSON verisi okunur.
            //    var values = JsonConvert.DeserializeObject<List<ResultBrandDTO>>(jsonData); // JSON verisi dinamik bir listeye dönüştürülür.
            //    return View(values); // Dönüştürülen liste view'e gönderilir.
            //}
            //return View();
            BrandViewBagList(); // ViewBag verilerini ayarlayan metot çağrılır.
            var values = await _brandService.GetAllBrandAsync(); // Tüm markaları getiren servis metodu çağrılır.
            return View(values); // Markalar view'e gönderilir.
        }
        [HttpGet]
        public IActionResult CreateBrand()
        {
            //ViewBag.v1 = "Ana Sayfa";
            //ViewBag.v2 = "Markaler";
            //ViewBag.v3 = "Yeni Marka Ekleme";
            //ViewBag.v4 = "Marka İşlemleri";
            BrandViewBagList();
            return View(); // Marka ekleme sayfası için view döndürülür.
        }
        [HttpPost]
        public async Task<IActionResult> CreateBrand(CreateBrandDTO createBrandDTO)
        {
            //var client = _httpClientFactory.CreateClient(); // IHttpClientFactory kullanarak HttpClient oluşturulur.
            //var jsonData = JsonConvert.SerializeObject(createBrandDTO); // DTO nesnesi JSON formatına dönüştürülür.
            //StringContent content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json"); // JSON verisi StringContent olarak hazırlanır.
            //var responseMessage = await client.PostAsync("https://localhost:1002/api/Brands", content); // API'ye POST isteği yapılır.
            //if (responseMessage.IsSuccessStatusCode) // Eğer istek başarılıysa
            //{
            //    return RedirectToAction("Index", "Brand", new { area = "Admin" }); // Marka listesine yönlendirilir.
            //}
            //return View(); // Başarısız ise aynı view döndürülür.
            await _brandService.CreateBrandAsync(createBrandDTO); // Yeni marka oluşturma servisi çağrılır.
            return RedirectToAction("Index", "Brand", new { area = "Admin" }); // Marka listesine yönlendirilir.
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> DeleteBrand(string id)
        {
            //var client = _httpClientFactory.CreateClient(); // IHttpClientFactory kullanarak HttpClient oluşturulur.
            //var responseMessage = await client.DeleteAsync($"https://localhost:1002/api/Brands?id=" + id); // API'den kategori silme isteği yapılır.
            //if (responseMessage.IsSuccessStatusCode) // Eğer istek başarılıysa
            //{
            //    return RedirectToAction("Index", "Brand", new { area = "Admin" }); // Marka listesine yönlendirilir.
            //}
            //return View(); // Başarısız ise aynı view döndürülür.
            await _brandService.DeleteBrandAsync(id); // Marka silme servisi çağrılır.
            return RedirectToAction("Index", "Brand", new { area = "Admin" }); // Marka listesine yönlendirilir.
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> UpdateBrand(string id)
        {
            //ViewBag.v1 = "Ana Sayfa";
            //ViewBag.v2 = "Markaler";
            //ViewBag.v3 = "Marka Güncelleme";
            //ViewBag.v4 = "Marka İşlemleri";
            //var client = _httpClientFactory.CreateClient(); // IHttpClientFactory kullanarak HttpClient oluşturulur.
            //var responseMessage = await client.GetAsync($"https://localhost:1002/api/Brands/{id}"); // API'den kategori verisi alınır.
            //if (responseMessage.IsSuccessStatusCode) // Eğer istek başarılıysa
            //{
            //    var jsonData = await responseMessage.Content.ReadAsStringAsync(); // JSON verisi okunur.
            //    var values = JsonConvert.DeserializeObject<UpdateBrandDTO>(jsonData); // JSON verisi DTO nesnesine dönüştürülür.
            //    return View(values); // Dönüştürülen DTO nesnesi view'e gönderilir.
            //}
            //return View(); // Başarısız ise aynı view döndürülür.
            BrandViewBagList();
            var values = await _brandService.GetByIDBrandAsync(id); // ID'ye göre marka getirme servisi çağrılır.
            return View(values); // DTO nesnesi view'e gönderilir.
        }
        [HttpPost("{id}")]
        public async Task<IActionResult> UpdateBrand(UpdateBrandDTO updateBrandDTO)
        {
            //var client = _httpClientFactory.CreateClient(); // IHttpClientFactory kullanarak HttpClient oluşturulur.
            //var jsonData = JsonConvert.SerializeObject(updateBrandDTO); // DTO nesnesi JSON formatına dönüştürülür.
            //StringContent content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json"); // JSON verisi StringContent olarak hazırlanır.
            //var responseMessage = await client.PutAsync("https://localhost:1002/api/Brands/", content); // API'ye PUT isteği yapılır.
            //if (responseMessage.IsSuccessStatusCode) // Eğer istek başarılıysa
            //{
            //    return RedirectToAction("Index", "Brand", new { area = "Admin" }); // Marka listesine yönlendirilir.
            //}
            //return View(); // Başarısız ise aynı view döndürülür.
            await _brandService.UpdateBrandAsync(updateBrandDTO); // Marka güncelleme servisi çağrılır.
            return RedirectToAction("Index", "Brand", new { area = "Admin" }); // Marka listesine yönlendirilir.
        }
        void BrandViewBagList()
        {
            ViewBag.v1 = "Ana Sayfa";
            ViewBag.v2 = "Markaler";
            ViewBag.v3 = "Marka Listesi";
            ViewBag.v4 = "Marka İşlemleri";
        }
    }
}
