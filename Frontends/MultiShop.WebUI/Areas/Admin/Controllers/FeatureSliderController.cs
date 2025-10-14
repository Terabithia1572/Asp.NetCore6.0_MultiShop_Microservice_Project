using Microsoft.AspNetCore.Mvc;
using MultiShop.DTOLayer.CatalogDTOs.FeatureSliderDTOs;
using MultiShop.WebUI.Services.CatalogServices.FeatureSliderServices;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]")] // Bu controller için rota tanımlaması yapılır.
    public class FeatureSliderController : Controller
    {
        //private readonly IHttpClientFactory _httpClientFactory;

        //public FeatureSliderController(IHttpClientFactory httpClientFactory)
        //{
        //    _httpClientFactory = httpClientFactory;
        //}
        private readonly IFeatureSliderService _featureSliderService;

        public FeatureSliderController(IFeatureSliderService featureSliderService)
        {
            _featureSliderService = featureSliderService;
        }

        public async Task<IActionResult> Index()
        {
           
            // Bu ViewBag'ler, view içinde kullanılacak verileri taşır.

            //var client = _httpClientFactory.CreateClient(); // IHttpClientFactory kullanarak HttpClient oluşturulur.
            //var responseMessage = await client.GetAsync("https://localhost:1002/api/FeatureSliders"); // API'den önce çıkan özellik verilerini almak için GET isteği yapılır.
            //if (responseMessage.IsSuccessStatusCode) // Eğer istek başarılıysa
            //{
            //    var jsonData = await responseMessage.Content.ReadAsStringAsync(); // JSON verisi okunur.
            //    var values = JsonConvert.DeserializeObject<List<ResultFeatureSliderDTO>>(jsonData); // JSON verisi dinamik bir listeye dönüştürülür.
            //    return View(values); // Dönüştürülen liste view'e gönderilir.
            //}
            //return View();

            FeatureSliderViewBagList();
            var values = await _featureSliderService.GetAllFeatureSliderAsync(); // Tüm önce çıkan özellikleri getirir.
            return View(values); // Listeyi view'e gönderir.
        }
        [HttpGet]
        public IActionResult CreateFeatureSlider()
        {
           FeatureSliderViewBagList();
            return View(); // Önce Çıkan Özellik ekleme sayfası için view döndürülür.
        }
        [HttpPost]
        public async Task<IActionResult> CreateFeatureSlider(CreateFeatureSliderDTO createFeatureSliderDTO)
        {
            //createFeatureSliderDTO.FeatureSliderStatus=false; // Yeni özellik eklenirken varsayılan olarak durum false olarak ayarlanır.
            //var client = _httpClientFactory.CreateClient(); // IHttpClientFactory kullanarak HttpClient oluşturulur.
            //var jsonData = JsonConvert.SerializeObject(createFeatureSliderDTO); // DTO nesnesi JSON formatına dönüştürülür.
            //StringContent content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json"); // JSON verisi StringContent olarak hazırlanır.
            //var responseMessage = await client.PostAsync("https://localhost:1002/api/FeatureSliders", content); // API'ye POST isteği yapılır.
            //if (responseMessage.IsSuccessStatusCode) // Eğer istek başarılıysa
            //{
            //    return RedirectToAction("Index", "FeatureSlider", new { area = "Admin" }); // Önce Çıkan Özellik listesine yönlendirilir.
            //}
            //return View(); // Başarısız ise aynı view döndürülür.
            await _featureSliderService.CreateFeatureSliderAsync(createFeatureSliderDTO);
            return RedirectToAction("Index", "FeatureSlider", new { area = "Admin" }); // Önce Çıkan Özellik listesine yönlendirilir.
        }
        [HttpPost("{id}")]
        public async Task<IActionResult> DeleteFeatureSlider(string id)
        {
            //var client = _httpClientFactory.CreateClient(); // IHttpClientFactory kullanarak HttpClient oluşturulur.
            //var responseMessage = await client.DeleteAsync($"https://localhost:1002/api/FeatureSliders?id=" + id); // API'den önce çıkan özellik silme isteği yapılır.
            //if (responseMessage.IsSuccessStatusCode) // Eğer istek başarılıysa
            //{
            //    return RedirectToAction("Index", "FeatureSlider", new { area = "Admin" }); // Önce Çıkan Özellik listesine yönlendirilir.
            //}
            //return View(); // Başarısız ise aynı view döndürülür.
            await _featureSliderService.DeleteFeatureSliderAsync(id);
            return RedirectToAction("Index", "FeatureSlider", new { area = "Admin" }); // Önce Çıkan Özellik listesine yönlendirilir.
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> UpdateFeatureSlider(string id)
        {
            FeatureSliderViewBagList();
            //var client = _httpClientFactory.CreateClient(); // IHttpClientFactory kullanarak HttpClient oluşturulur.
            //var responseMessage = await client.GetAsync($"https://localhost:1002/api/FeatureSliders/{id}"); // API'den önce çıkan özellik verisi alınır.
            //if (responseMessage.IsSuccessStatusCode) // Eğer istek başarılıysa
            //{
            //    var jsonData = await responseMessage.Content.ReadAsStringAsync(); // JSON verisi okunur.
            //    var values = JsonConvert.DeserializeObject<UpdateFeatureSliderDTO>(jsonData); // JSON verisi DTO nesnesine dönüştürülür.
            //    return View(values); // Dönüştürülen DTO nesnesi view'e gönderilir.
            //}
            //return View(); // Başarısız ise aynı view döndürülür.
            var values = await _featureSliderService.GetByIDFeatureSliderAsync(id); // ID ile önce çıkan özellik getirir.
            return View(values); // Dönüştürülen DTO nesnesi view'e gönderilir.
        }
        [HttpPost("{id}")]
        public async Task<IActionResult> UpdateFeatureSlider(UpdateFeatureSliderDTO updateFeatureSliderDTO)
        {
            //var client = _httpClientFactory.CreateClient(); // IHttpClientFactory kullanarak HttpClient oluşturulur.
            //var jsonData = JsonConvert.SerializeObject(updateFeatureSliderDTO); // DTO nesnesi JSON formatına dönüştürülür.
            //StringContent content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json"); // JSON verisi StringContent olarak hazırlanır.
            //var responseMessage = await client.PutAsync("https://localhost:1002/api/FeatureSliders/", content); // API'ye PUT isteği yapılır.
            //if (responseMessage.IsSuccessStatusCode) // Eğer istek başarılıysa
            //{
            //    return RedirectToAction("Index", "FeatureSlider", new { area = "Admin" }); // Önce Çıkan Özellik listesine yönlendirilir.
            //}
            //return View(); // Başarısız ise aynı view döndürülür.
            await _featureSliderService.UpdateFeatureSliderAsync(updateFeatureSliderDTO);
            return RedirectToAction("Index", "FeatureSlider", new { area = "Admin" }); // Önce Çıkan Özellik listesine yönlendirilir.
        }

        void FeatureSliderViewBagList()
        {
            ViewBag.v1 = "Ana Sayfa";
            ViewBag.v2 = "Önce Çıkan Özellikler";
            ViewBag.v3 = "Önce Çıkan Özellik Listesi";
            ViewBag.v4 = "Önce Çıkan Özellik İşlemleri";
        }
    }
}
