using Microsoft.AspNetCore.Mvc;
using MultiShop.DTOLayer.IdentityDTOs.RegisterDTOs;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public RegisterController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }


        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(CreateRegisterDTO createRegisterDTO)
        {
            if(createRegisterDTO.Password==createRegisterDTO.ConfirmPassword) // Şifre ve onay şifresi eşleşmiyorsa
            {
            var client = _httpClientFactory.CreateClient(); // IHttpClientFactory kullanarak HttpClient oluşturulur.
            var jsonData = JsonConvert.SerializeObject(createRegisterDTO); // DTO nesnesi JSON formatına dönüştürülür.
            StringContent content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json"); // JSON verisi StringContent olarak hazırlanır.
            var responseMessage = await client.PostAsync("http://localhost:5001/api/Registers", content); // API'ye POST isteği yapılır.
            if (responseMessage.IsSuccessStatusCode) // Eğer istek başarılıysa
            {
                return RedirectToAction("Index", "Login"); // Giriş kısmına yönlendirilir.
            }
            }
            return View(); // Başarısız ise aynı view döndürülür.
        }
    }
}
