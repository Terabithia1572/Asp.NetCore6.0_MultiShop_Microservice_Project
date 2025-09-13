using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DTOLayer.CatalogDTOs.FeatureDTOs;
using MultiShop.WebUI.Services.CatalogServices.FeatureServices;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{

    [Area("Admin")] // Bu Area'nın adı "Admin" olarak ayarlanır. yani URL'de /Admin/ ile başlayacak.
    [Route("Admin/[controller]/[action]")] // Bu controller için rota ayarlanır. Örneğin: /Admin/Feature/Index
    public class FeatureController : Controller
    {
        //private readonly IHttpClientFactory _httpClientFactory;

        //public FeatureController(IHttpClientFactory httpClientFactory)
        //{
        //    _httpClientFactory = httpClientFactory;
        //}
        private readonly IFeatureService _featureService; // IFeatureService türünde bir alan tanımlanır.

        public FeatureController(IFeatureService featureService)
        {
            _featureService = featureService;
        }

        public async Task<IActionResult> Index()
        {

            // Bu ViewBag'ler, view içinde kullanılacak verileri taşır.

            //var client = _httpClientFactory.CreateClient(); // IHttpClientFactory kullanarak HttpClient oluşturulur.
            //var responseMessage = await client.GetAsync("https://localhost:1002/api/Features"); // API'den kategori verilerini almak için GET isteği yapılır.
            //if (responseMessage.IsSuccessStatusCode) // Eğer istek başarılıysa
            //{
            //    var jsonData = await responseMessage.Content.ReadAsStringAsync(); // JSON verisi okunur.
            //    var values = JsonConvert.DeserializeObject<List<ResultFeatureDTO>>(jsonData); // JSON verisi dinamik bir listeye dönüştürülür.
            //    return View(values); // Dönüştürülen liste view'e gönderilir.
            //}
            //return View();
            FeatureViewBag();
            var values = await _featureService.GetAllFeatureAsync();
            return View(values);
        }
        [HttpGet]
        public IActionResult CreateFeature()
        {
          FeatureViewBag();
            return View(); // Önce Çıkan Özellik ekleme sayfası için view döndürülür.
        }
        [HttpPost]
        public async Task<IActionResult> CreateFeature(CreateFeatureDTO createFeatureDTO)
        {
            //var client = _httpClientFactory.CreateClient(); // IHttpClientFactory kullanarak HttpClient oluşturulur.
            //var jsonData = JsonConvert.SerializeObject(createFeatureDTO); // DTO nesnesi JSON formatına dönüştürülür.
            //StringContent content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json"); // JSON verisi StringContent olarak hazırlanır.
            //var responseMessage = await client.PostAsync("https://localhost:1002/api/Features", content); // API'ye POST isteği yapılır.
            //if (responseMessage.IsSuccessStatusCode) // Eğer istek başarılıysa
            //{
            //    return RedirectToAction("Index", "Feature", new { area = "Admin" }); // Önce Çıkan Özellik listesine yönlendirilir.
            //}
            //return View(); // Başarısız ise aynı view döndürülür.
            await _featureService.CreateFeatureAsync(createFeatureDTO);
            return RedirectToAction("Index", "Feature", new { area = "Admin" });
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> DeleteFeature(string id)
        {
            //var client = _httpClientFactory.CreateClient(); // IHttpClientFactory kullanarak HttpClient oluşturulur.
            //var responseMessage = await client.DeleteAsync($"https://localhost:1002/api/Features?id=" + id); // API'den kategori silme isteği yapılır.
            //if (responseMessage.IsSuccessStatusCode) // Eğer istek başarılıysa
            //{
            //    return RedirectToAction("Index", "Feature", new { area = "Admin" }); // Önce Çıkan Özellik listesine yönlendirilir.
            //}
            //return View(); // Başarısız ise aynı view döndürülür.
            await _featureService.DeleteFeatureAsync(id);
            return RedirectToAction("Index", "Feature", new { area = "Admin" });
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> UpdateFeature(string id)
        {
            //ViewBag.v1 = "Ana Sayfa";
            //ViewBag.v2 = "Önce Çıkan Özellikler";
            //ViewBag.v3 = "Önce Çıkan Özellik Güncelleme";
            //ViewBag.v4 = "Önce Çıkan Özellik İşlemleri";
            //var client = _httpClientFactory.CreateClient(); // IHttpClientFactory kullanarak HttpClient oluşturulur.
            //var responseMessage = await client.GetAsync($"https://localhost:1002/api/Features/{id}"); // API'den kategori verisi alınır.
            //if (responseMessage.IsSuccessStatusCode) // Eğer istek başarılıysa
            //{
            //    var jsonData = await responseMessage.Content.ReadAsStringAsync(); // JSON verisi okunur.
            //    var values = JsonConvert.DeserializeObject<UpdateFeatureDTO>(jsonData); // JSON verisi DTO nesnesine dönüştürülür.
            //    return View(values); // Dönüştürülen DTO nesnesi view'e gönderilir.
            //}
            //return View(); // Başarısız ise aynı view döndürülür.
            FeatureViewBag();
            var values = await _featureService.GetByIDFeatureAsync(id);
            return View(values);
        }
        [HttpPost("{id}")]
        public async Task<IActionResult> UpdateFeature(UpdateFeatureDTO updateFeatureDTO)
        {
            //var client = _httpClientFactory.CreateClient(); // IHttpClientFactory kullanarak HttpClient oluşturulur.
            //var jsonData = JsonConvert.SerializeObject(updateFeatureDTO); // DTO nesnesi JSON formatına dönüştürülür.
            //StringContent content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json"); // JSON verisi StringContent olarak hazırlanır.
            //var responseMessage = await client.PutAsync("https://localhost:1002/api/Features/", content); // API'ye PUT isteği yapılır.
            //if (responseMessage.IsSuccessStatusCode) // Eğer istek başarılıysa
            //{
            //    return RedirectToAction("Index", "Feature", new { area = "Admin" }); // Önce Çıkan Özellik listesine yönlendirilir.
            //}
            //return View(); // Başarısız ise aynı view döndürülür.
            await _featureService.UpdateFeatureAsync(updateFeatureDTO);
            return RedirectToAction("Index", "Feature", new { area = "Admin" });
        }
        void FeatureViewBag()
        {
            ViewBag.v1 = "Ana Sayfa";
            ViewBag.v2 = "Önce Çıkan Özellikler";
            ViewBag.v3 = "Önce Çıkan Özellik Güncelleme";
            ViewBag.v4 = "Önce Çıkan Özellik İşlemleri";

        }
    }
}
