using Microsoft.AspNetCore.Mvc;
using MultiShop.DTOLayer.CatalogDTOs.FeatureSliderDTOs;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]")] // Bu controller için rota tanımlaması yapılır.
    public class FeatureSliderController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public FeatureSliderController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }


        public async Task<IActionResult> Index()
        {
            ViewBag.v1 = "Ana Sayfa";
            ViewBag.v2 = "Önce Çıkan Özellikler";
            ViewBag.v3 = "Önce Çıkan Özellik Listesi";
            ViewBag.v4 = "Önce Çıkan Özellik İşlemleri";
            // Bu ViewBag'ler, view içinde kullanılacak verileri taşır.

            var client = _httpClientFactory.CreateClient(); // IHttpClientFactory kullanarak HttpClient oluşturulur.
            var responseMessage = await client.GetAsync("https://localhost:1002/api/FeatureSliders"); // API'den önce çıkan özellik verilerini almak için GET isteği yapılır.
            if (responseMessage.IsSuccessStatusCode) // Eğer istek başarılıysa
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync(); // JSON verisi okunur.
                var values = JsonConvert.DeserializeObject<List<ResultFeatureSliderDTO>>(jsonData); // JSON verisi dinamik bir listeye dönüştürülür.
                return View(values); // Dönüştürülen liste view'e gönderilir.
            }
            return View();
        }
        [HttpGet]
        public IActionResult CreateFeatureSlider()
        {
            ViewBag.v1 = "Ana Sayfa";
            ViewBag.v2 = "Önce Çıkan Özellikler";
            ViewBag.v3 = "Yeni Önce Çıkan Özellik Ekleme";
            ViewBag.v4 = "Önce Çıkan Özellik İşlemleri";
            return View(); // Önce Çıkan Özellik ekleme sayfası için view döndürülür.
        }
        [HttpPost]
        public async Task<IActionResult> CreateFeatureSlider(CreateFeatureSliderDTO createFeatureSliderDTO)
        {
            createFeatureSliderDTO.FeatureSliderStatus=false; // Yeni özellik eklenirken varsayılan olarak durum false olarak ayarlanır.
            var client = _httpClientFactory.CreateClient(); // IHttpClientFactory kullanarak HttpClient oluşturulur.
            var jsonData = JsonConvert.SerializeObject(createFeatureSliderDTO); // DTO nesnesi JSON formatına dönüştürülür.
            StringContent content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json"); // JSON verisi StringContent olarak hazırlanır.
            var responseMessage = await client.PostAsync("https://localhost:1002/api/FeatureSliders", content); // API'ye POST isteği yapılır.
            if (responseMessage.IsSuccessStatusCode) // Eğer istek başarılıysa
            {
                return RedirectToAction("Index", "FeatureSlider", new { area = "Admin" }); // Önce Çıkan Özellik listesine yönlendirilir.
            }
            return View(); // Başarısız ise aynı view döndürülür.
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> DeleteFeatureSlider(string id)
        {
            var client = _httpClientFactory.CreateClient(); // IHttpClientFactory kullanarak HttpClient oluşturulur.
            var responseMessage = await client.DeleteAsync($"https://localhost:1002/api/FeatureSliders?id=" + id); // API'den önce çıkan özellik silme isteği yapılır.
            if (responseMessage.IsSuccessStatusCode) // Eğer istek başarılıysa
            {
                return RedirectToAction("Index", "FeatureSlider", new { area = "Admin" }); // Önce Çıkan Özellik listesine yönlendirilir.
            }
            return View(); // Başarısız ise aynı view döndürülür.
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> UpdateFeatureSlider(string id)
        {
            ViewBag.v1 = "Ana Sayfa";
            ViewBag.v2 = "Önce Çıkan Özellikler";
            ViewBag.v3 = "Önce Çıkan Özellik Güncelleme";
            ViewBag.v4 = "Önce Çıkan Özellik İşlemleri";
            var client = _httpClientFactory.CreateClient(); // IHttpClientFactory kullanarak HttpClient oluşturulur.
            var responseMessage = await client.GetAsync($"https://localhost:1002/api/FeatureSliders/{id}"); // API'den önce çıkan özellik verisi alınır.
            if (responseMessage.IsSuccessStatusCode) // Eğer istek başarılıysa
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync(); // JSON verisi okunur.
                var values = JsonConvert.DeserializeObject<UpdateFeatureSliderDTO>(jsonData); // JSON verisi DTO nesnesine dönüştürülür.
                return View(values); // Dönüştürülen DTO nesnesi view'e gönderilir.
            }
            return View(); // Başarısız ise aynı view döndürülür.
        }
        [HttpPost("{id}")]
        public async Task<IActionResult> UpdateFeatureSlider(UpdateFeatureSliderDTO updateFeatureSliderDTO)
        {
            var client = _httpClientFactory.CreateClient(); // IHttpClientFactory kullanarak HttpClient oluşturulur.
            var jsonData = JsonConvert.SerializeObject(updateFeatureSliderDTO); // DTO nesnesi JSON formatına dönüştürülür.
            StringContent content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json"); // JSON verisi StringContent olarak hazırlanır.
            var responseMessage = await client.PutAsync("https://localhost:1002/api/FeatureSliders/", content); // API'ye PUT isteği yapılır.
            if (responseMessage.IsSuccessStatusCode) // Eğer istek başarılıysa
            {
                return RedirectToAction("Index", "FeatureSlider", new { area = "Admin" }); // Önce Çıkan Özellik listesine yönlendirilir.
            }
            return View(); // Başarısız ise aynı view döndürülür.
        }
    }
}
