using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DTOLayer.CatalogDTOs.AboutDTOs;
using MultiShop.WebUI.Services.CatalogServices.AboutServices;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
   // [AllowAnonymous] // Bu controller'a anonim erişime izin verilir.
    [Area("Admin")] // Bu Area'nın adı "Admin" olarak ayarlanır. yani URL'de /Admin/ ile başlayacak.
    [Route("Admin/[controller]/[action]")] // Bu controller için rota ayarlanır. Örneğin: /Admin/About/Index
   
    public class AboutController : Controller
    {
        //private readonly IHttpClientFactory _httpClientFactory;

        //public AboutController(IHttpClientFactory httpClientFactory)
        //{
        //    _httpClientFactory = httpClientFactory;
        //}
        private readonly IAboutService _aboutService;

        public AboutController(IAboutService aboutService)
        {
            _aboutService = aboutService;
        }

        public async Task<IActionResult> Index()
        {
            //ViewBag.v1 = "Ana Sayfa";
            //ViewBag.v2 = "Hakkımdaler";
            //ViewBag.v3 = "Hakkımda Listesi";
            //ViewBag.v4 = "Hakkımda İşlemleri";
            //// Bu ViewBag'ler, view içinde kullanılacak verileri taşır.

            //var client = _httpClientFactory.CreateClient(); // IHttpClientFactory kullanarak HttpClient oluşturulur.
            //var responseMessage = await client.GetAsync("https://localhost:1002/api/Abouts"); // API'den kategori verilerini almak için GET isteği yapılır.
            //if (responseMessage.IsSuccessStatusCode) // Eğer istek başarılıysa
            //{
            //    var jsonData = await responseMessage.Content.ReadAsStringAsync(); // JSON verisi okunur.
            //    var values = JsonConvert.DeserializeObject<List<ResultAboutDTO>>(jsonData); // JSON verisi dinamik bir listeye dönüştürülür.
            //    return View(values); // Dönüştürülen liste view'e gönderilir.
            //}
            //return View();
            BrandViewBagList(); // ViewBag verilerini ayarlayan metot çağrılır.
            var values = await _aboutService.GetAllAboutAsync(); // Tüm markaları getiren servis metodu çağrılır.
            return View(values); // Markalar view'e gönderilir.
        }
        [HttpGet]
        public IActionResult CreateAbout()
        {
            //ViewBag.v1 = "Ana Sayfa";
            //ViewBag.v2 = "Hakkımdaler";
            //ViewBag.v3 = "Yeni Hakkımda Ekleme";
            //ViewBag.v4 = "Hakkımda İşlemleri";
            BrandViewBagList();
            return View(); // Hakkımda ekleme sayfası için view döndürülür.
        }
        [HttpPost]
        public async Task<IActionResult> CreateAbout(CreateAboutDTO createAboutDTO)
        {
            //var client = _httpClientFactory.CreateClient(); // IHttpClientFactory kullanarak HttpClient oluşturulur.
            //var jsonData = JsonConvert.SerializeObject(createAboutDTO); // DTO nesnesi JSON formatına dönüştürülür.
            //StringContent content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json"); // JSON verisi StringContent olarak hazırlanır.
            //var responseMessage = await client.PostAsync("https://localhost:1002/api/Abouts", content); // API'ye POST isteği yapılır.
            //if (responseMessage.IsSuccessStatusCode) // Eğer istek başarılıysa
            //{
            //    return RedirectToAction("Index", "About", new { area = "Admin" }); // Hakkımda listesine yönlendirilir.
            //}
            //return View(); // Başarısız ise aynı view döndürülür.
            await _aboutService.CreateAboutAsync(createAboutDTO); // Yeni hakkımda ekleyen servis metodu çağrılır.
            return RedirectToAction("Index", "About", new { area = "Admin" }); // Hakkımda listesine yönlendirilir.
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> DeleteAbout(string id)
        {
            //var client = _httpClientFactory.CreateClient(); // IHttpClientFactory kullanarak HttpClient oluşturulur.
            //var responseMessage = await client.DeleteAsync($"https://localhost:1002/api/Abouts?id=" + id); // API'den kategori silme isteği yapılır.
            //if (responseMessage.IsSuccessStatusCode) // Eğer istek başarılıysa
            //{
            //    return RedirectToAction("Index", "About", new { area = "Admin" }); // Hakkımda listesine yönlendirilir.
            //}
            //return View(); // Başarısız ise aynı view döndürülür.
            await _aboutService.DeleteAboutAsync(id); // Hakkımda silme servis metodu çağrılır.
            return RedirectToAction("Index", "About", new { area = "Admin" }); // Hakkımda listesine yönlendirilir.
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> UpdateAbout(string id)
        {
            //ViewBag.v1 = "Ana Sayfa";
            //ViewBag.v2 = "Hakkımdaler";
            //ViewBag.v3 = "Hakkımda Güncelleme";
            //ViewBag.v4 = "Hakkımda İşlemleri";
            //var client = _httpClientFactory.CreateClient(); // IHttpClientFactory kullanarak HttpClient oluşturulur.
            //var responseMessage = await client.GetAsync($"https://localhost:1002/api/Abouts/{id}"); // API'den kategori verisi alınır.
            //if (responseMessage.IsSuccessStatusCode) // Eğer istek başarılıysa
            //{
            //    var jsonData = await responseMessage.Content.ReadAsStringAsync(); // JSON verisi okunur.
            //    var values = JsonConvert.DeserializeObject<UpdateAboutDTO>(jsonData); // JSON verisi DTO nesnesine dönüştürülür.
            //    return View(values); // Dönüştürülen DTO nesnesi view'e gönderilir.
            //}
            //return View(); // Başarısız ise aynı view döndürülür.
            BrandViewBagList();
            var values = await _aboutService.GetByIDAboutAsync(id); // ID'ye göre hakkımda getiren servis metodu çağrılır.
            return View(values); // Hakkımda bilgileri view'e gönderilir.
        }
        [HttpPost("{id}")]
        public async Task<IActionResult> UpdateAbout(UpdateAboutDTO updateAboutDTO)
        {
            //var client = _httpClientFactory.CreateClient(); // IHttpClientFactory kullanarak HttpClient oluşturulur.
            //var jsonData = JsonConvert.SerializeObject(updateAboutDTO); // DTO nesnesi JSON formatına dönüştürülür.
            //StringContent content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json"); // JSON verisi StringContent olarak hazırlanır.
            //var responseMessage = await client.PutAsync("https://localhost:1002/api/Abouts/", content); // API'ye PUT isteği yapılır.
            //if (responseMessage.IsSuccessStatusCode) // Eğer istek başarılıysa
            //{
            //    return RedirectToAction("Index", "About", new { area = "Admin" }); // Hakkımda listesine yönlendirilir.
            //}
            //return View(); // Başarısız ise aynı view döndürülür.
            await _aboutService.UpdateAboutAsync(updateAboutDTO); // Hakkımda güncelleyen servis metodu çağrılır.
            return RedirectToAction("Index", "About", new { area = "Admin" }); // Hakkımda listesine yönlendirilir.
        }
        void BrandViewBagList()
        {
            ViewBag.v1 = "Ana Sayfa";
            ViewBag.v2 = "Hakkımdaler";
            ViewBag.v3 = "Hakkımda Listesi";
            ViewBag.v4 = "Hakkımda İşlemleri";
        }
    }
}
