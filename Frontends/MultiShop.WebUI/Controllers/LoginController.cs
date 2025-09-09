using Microsoft.AspNetCore.Mvc;
using MultiShop.DTOLayer.IdentityDTOs.LoginDTOs;
using MultiShop.WebUI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Text.Json;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication;
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
            return View();
        }
        public async Task<IActionResult> SignIn(SignInDTO signInDTO)
        {
            signInDTO.UserName = "terabithia1572";
            signInDTO.Password = "Yunus6565*";
            await _identityService.SignIn(signInDTO); //IdentityService üzerinden SignIn metodunu çağırıyoruz

            return RedirectToAction("Index","Test"); //Başarısız ise tekrar kayıt sayfasını gösteriyoruz
        }
    }
}
