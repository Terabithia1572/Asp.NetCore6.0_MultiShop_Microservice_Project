using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MultiShop.IdentityServer.DTOs;
using MultiShop.IdentityServer.Models;
using MultiShop.IdentityServer.Tools;
using System.Threading.Tasks;

namespace MultiShop.IdentityServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginsController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> _signInManager;

        public LoginsController(SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
        }

        [HttpPost]
        public async Task<IActionResult> UserLogin(UserLoginDTO userLoginDTO)
        {
            var result = await _signInManager.PasswordSignInAsync(userLoginDTO.Username, userLoginDTO.Password, false, false);
            if (!result.Succeeded)
            {
                return BadRequest("Giriş işlemi başarısız.");
            }
            else
            {
                GetCheckAppUserViewModel getCheckAppUserViewModel = new GetCheckAppUserViewModel();
                getCheckAppUserViewModel.UserName= userLoginDTO.Username;
                getCheckAppUserViewModel.ID= "1"; //Kullanıcı ID'si burada atanmalı
                var token = JwtTokenGenerator.GenerateToken(getCheckAppUserViewModel);

                return Ok(token);

            }
        }
    }
}
