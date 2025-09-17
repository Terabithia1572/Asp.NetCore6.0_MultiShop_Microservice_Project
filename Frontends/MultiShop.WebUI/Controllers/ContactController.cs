using Microsoft.AspNetCore.Mvc;
using MultiShop.DTOLayer.CatalogDTOs.ContactDTOs;
using MultiShop.WebUI.Services.CatalogServices.ContactServices;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Controllers
{
    public class ContactController : Controller
    {
        //private readonly IHttpClientFactory _httpClientFactory;

        //public ContactController(IHttpClientFactory httpClientFactory)
        //{
        //    _httpClientFactory = httpClientFactory;
        //}
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.directory1 = "Ana Sayfa";
            ViewBag.directory2 = "İletişim";
            ViewBag.directory3 = "Mesaj Gönderme";
            return View(); // Kategori ekleme sayfası için view döndürülür.
        }
        [HttpPost]
        public async Task< IActionResult> Index(CreateContactDTO createContactDTO)
        {
            createContactDTO.ContactIsRead = false; // Yeni eklenen mesajın okunmadı olarak işaretlenmesi
            createContactDTO.ContactCreatedDate = DateTime.Now; // Mesajın oluşturulma tarihinin şu an olarak ayarlanması
            await _contactService.CreateContactAsync(createContactDTO);
            return RedirectToAction("Index", "Default");

         
            //var client = _httpClientFactory.CreateClient(); // IHttpClientFactory kullanarak HttpClient oluşturulur.
            //var jsonData = JsonConvert.SerializeObject(createContactDTO); // DTO nesnesi JSON formatına dönüştürülür.
            //StringContent content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json"); // JSON verisi StringContent olarak hazırlanır.
            //var responseMessage = await client.PostAsync("https://localhost:1002/api/Contacts", content); // API'ye POST isteği yapılır.
            //if (responseMessage.IsSuccessStatusCode) // Eğer istek başarılıysa
            //{
            //    return RedirectToAction("Index", "Default"); // Kategori listesine yönlendirilir.
            //}
            //return View(); // Başarısız ise aynı view döndürülür.
        }
    }
}
