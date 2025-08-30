using Microsoft.AspNetCore.Mvc;
using MultiShop.DTOLayer.IdentityDTOs.LoginDTOs;
using MultiShop.WebUI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Text.Json;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication;
using MultiShop.WebUI.Services;
using MultiShop.WebUI.Services.Interfaces;

namespace MultiShop.WebUI.Controllers
{
    public class LoginController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IloginService _loginService; //ILoginService'i ekliyoruz
        private readonly IIdentityService _identityService;

        public LoginController(IHttpClientFactory httpClientFactory, IloginService loginService, IIdentityService identityService)
        {
            _httpClientFactory = httpClientFactory;
            _loginService = loginService;
            _identityService = identityService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(CreateLoginDTO createLoginDTO)
        {

            var client = _httpClientFactory.CreateClient(); //HttpClient'ı HttpClientFactory üzerinden alıyoruz
            var content = new StringContent(JsonSerializer.Serialize(createLoginDTO), Encoding.UTF8, "application/json"); //DTO'yu JSON formatına çeviriyoruz
            var response = await client.PostAsync("http://localhost:5001/api/Logins", content); //API'ye POST isteği gönderiyoruz
            if (response.IsSuccessStatusCode) //İstek başarılı ise
            {
                var jsonData = await response.Content.ReadAsStringAsync(); //Cevap içeriğini okuyoruz
                var tokenModel = JsonSerializer.Deserialize<JwtResponseModel>(jsonData, new JsonSerializerOptions //JSON'u JwtResponseModel'e çeviriyoruz
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase, //CamelCase kullanıyoruz
                });
                if (tokenModel != null) //TokenModel null değilse
                {
                    JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler(); //JWT token'ı işlemek için handler oluşturuyoruz
                    var token = handler.ReadJwtToken(tokenModel.Token); //Token'ı okuyoruz
                    var claims = token.Claims.ToList(); //Token içindeki claim'leri listeye çeviriyoruz

                    if (tokenModel.Token != null) //Token null değilse
                    {
                        claims.Add(new Claim("multishoptoken", tokenModel.Token)); //Token'ı claim olarak ekliyoruz
                        var claimsIdentity = new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme); //ClaimsIdentity oluşturuyoruz
                        var authProperties = new AuthenticationProperties //Kimlik doğrulama özelliklerini ayarlıyoruz
                        {
                            ExpiresUtc = tokenModel.ExpireDate, //Token'ın geçerlilik süresi
                            IsPersistent = true, //Oturumun kalıcı olup olmadığı
                        };
                        await HttpContext.SignInAsync(JwtBearerDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties); //Kullanıcıyı oturum açtırıyoruz
                        var id = _loginService.GetUserID; //Kullanıcı ID'sini alıyoruz  
                        return RedirectToAction("Index", "Default"); //Ana sayfaya yönlendiriyoruz
                    }
                }
            }
            return View(); //Başarısız ise tekrar giriş sayfasını gösteriyoruz
        }

        [HttpGet]
        public async Task<IActionResult> SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpDTO signUpDTO)
        {
            signUpDTO.UserName = "terabithia1572";
            signUpDTO.Password = "Yunus6565*";
            await _identityService.SignIn(signUpDTO); //IdentityService üzerinden SignIn metodunu çağırıyoruz

            return RedirectToAction("Index","Test"); //Başarısız ise tekrar kayıt sayfasını gösteriyoruz
        }
    }
}
