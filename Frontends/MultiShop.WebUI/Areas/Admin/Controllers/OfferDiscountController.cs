using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DTOLayer.CatalogDTOs.OfferDiscountDTOs;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")] //Burası Admin Areada yer alıyor alamına gelir.
    [Route("Admin/[controller]/[action]")] // Burası Controller için Route tanımlaması yapıldı.

    public class OfferDiscountController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public OfferDiscountController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }


        public async Task<IActionResult> Index()
        {
            ViewBag.v1 = "Ana Sayfa";
            ViewBag.v2 = "İndirim Teklifler";
            ViewBag.v3 = "İndirim Teklif Listesi";
            ViewBag.v4 = "İndirim Teklif İşlemleri";
            // Bu ViewBag'ler, view içinde kullanılacak verileri taşır.

            var client = _httpClientFactory.CreateClient(); // IHttpClientFactory kullanarak HttpClient oluşturulur.
            var responseMessage = await client.GetAsync("https://localhost:1002/api/OfferDiscounts"); // API'den İndirim Teklifler verilerini almak için GET isteği yapılır.
            if (responseMessage.IsSuccessStatusCode) // Eğer istek başarılıysa
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync(); // JSON verisi okunur.
                var values = JsonConvert.DeserializeObject<List<ResultOfferDiscountDTO>>(jsonData); // JSON verisi dinamik bir listeye dönüştürülür.
                return View(values); // Dönüştürülen liste view'e gönderilir.
            }
            return View();
        }
        [HttpGet]
        public IActionResult CreateOfferDiscount()
        {
            ViewBag.v1 = "Ana Sayfa";
            ViewBag.v2 = "İndirim Teklifler";
            ViewBag.v3 = "Yeni İndirim Teklif Ekleme";
            ViewBag.v4 = "İndirim Teklif İşlemleri";
            return View(); // İndirim Teklif ekleme sayfası için view döndürülür.
        }
        [HttpPost]
        public async Task<IActionResult> CreateOfferDiscount(CreateOfferDiscountDTO createOfferDiscountDTO)
        {
            var client = _httpClientFactory.CreateClient(); // IHttpClientFactory kullanarak HttpClient oluşturulur.
            var jsonData = JsonConvert.SerializeObject(createOfferDiscountDTO); // DTO nesnesi JSON formatına dönüştürülür.
            StringContent content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json"); // JSON verisi StringContent olarak hazırlanır.
            var responseMessage = await client.PostAsync("https://localhost:1002/api/OfferDiscounts", content); // API'ye POST isteği yapılır.
            if (responseMessage.IsSuccessStatusCode) // Eğer istek başarılıysa
            {
                return RedirectToAction("Index", "OfferDiscount", new { area = "Admin" }); // İndirim Teklif listesine yönlendirilir.
            }
            return View(); // Başarısız ise aynı view döndürülür.
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> DeleteOfferDiscount(string id)
        {
            var client = _httpClientFactory.CreateClient(); // IHttpClientFactory kullanarak HttpClient oluşturulur.
            var responseMessage = await client.DeleteAsync($"https://localhost:1002/api/OfferDiscounts?id=" + id); // API'den İndirim Teklifler silme isteği yapılır.
            if (responseMessage.IsSuccessStatusCode) // Eğer istek başarılıysa
            {
                return RedirectToAction("Index", "OfferDiscount", new { area = "Admin" }); // İndirim Teklif listesine yönlendirilir.
            }
            return View(); // Başarısız ise aynı view döndürülür.
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> UpdateOfferDiscount(string id)
        {
            ViewBag.v1 = "Ana Sayfa";
            ViewBag.v2 = "İndirim Teklifler";
            ViewBag.v3 = "İndirim Teklif Güncelleme";
            ViewBag.v4 = "İndirim Teklif İşlemleri";
            var client = _httpClientFactory.CreateClient(); // IHttpClientFactory kullanarak HttpClient oluşturulur.
            var responseMessage = await client.GetAsync($"https://localhost:1002/api/OfferDiscounts/{id}"); // API'den İndirim Teklifler verisi alınır.
            if (responseMessage.IsSuccessStatusCode) // Eğer istek başarılıysa
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync(); // JSON verisi okunur.
                var values = JsonConvert.DeserializeObject<UpdateOfferDiscountDTO>(jsonData); // JSON verisi DTO nesnesine dönüştürülür.
                return View(values); // Dönüştürülen DTO nesnesi view'e gönderilir.
            }
            return View(); // Başarısız ise aynı view döndürülür.
        }
        [HttpPost("{id}")]
        public async Task<IActionResult> UpdateOfferDiscount(UpdateOfferDiscountDTO updateOfferDiscountDTO)
        {
            var client = _httpClientFactory.CreateClient(); // IHttpClientFactory kullanarak HttpClient oluşturulur.
            var jsonData = JsonConvert.SerializeObject(updateOfferDiscountDTO); // DTO nesnesi JSON formatına dönüştürülür.
            StringContent content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json"); // JSON verisi StringContent olarak hazırlanır.
            var responseMessage = await client.PutAsync("https://localhost:1002/api/OfferDiscounts/", content); // API'ye PUT isteği yapılır.
            if (responseMessage.IsSuccessStatusCode) // Eğer istek başarılıysa
            {
                return RedirectToAction("Index", "OfferDiscount", new { area = "Admin" }); // İndirim Teklif listesine yönlendirilir.
            }
            return View(); // Başarısız ise aynı view döndürülür.
        }
    }
}
