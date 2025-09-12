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
        private readonly IIdentityService _identityService;

        public LoginController(IHttpClientFactory httpClientFactory,IIdentityService identityService)
        {
            _httpClientFactory = httpClientFactory;
            _identityService = identityService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(SignInDTO signInDTO)
        {
            await _identityService.SignIn(signInDTO); //IdentityService üzerinden SignIn metodunu çağırıyoruz

            return RedirectToAction("Index", "User"); //Başarısız ise tekrar kayıt sayfasını gösteriyoruz
        }
      
    }
}
