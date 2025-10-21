using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MultiShop.IdentityServer.DTOs;
using MultiShop.IdentityServer.Models;
using MultiShop.IdentityServer.Tools;
using System;
using System.Threading.Tasks;

namespace MultiShop.IdentityServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginsController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public LoginsController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> UserLogin(UserLoginDTO userLoginDto)
        {
            var result = await _signInManager.PasswordSignInAsync(userLoginDto.Username, userLoginDto.Password, false, false);
            var user = await _userManager.FindByNameAsync(userLoginDto.Username);

            if (result.Succeeded)
            {
                // 🔹 Son giriş tarihini güncelle
                user.LastLoginDate = DateTime.UtcNow; // Son giriş zamanı (UTC)
                await _userManager.UpdateAsync(user); // DB’ye kaydet

                // 🔹 Token üretimi (mevcut yapını bozmuyoruz)
                GetCheckAppUserViewModel model = new GetCheckAppUserViewModel();
                model.UserName = userLoginDto.Username;
                model.ID = user.Id;

                // 🔥 Kullanıcının profil fotoğrafını token’a dahil etmek için modele ekle
                model.ProfileImageUrl = user.ProfileImageUrl; // (AspNetUsers tablosundaki değer)

                var token = JwtTokenGenerator.GenerateToken(model);

                return Ok(token);
            }
            else
            {
                return Ok("Kullanıcı Adı veya Şifre Hatalı");
            }
        }
    }
}
