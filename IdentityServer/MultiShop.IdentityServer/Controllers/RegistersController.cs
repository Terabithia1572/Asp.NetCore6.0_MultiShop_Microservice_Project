using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MultiShop.IdentityServer.DTOs;
using MultiShop.IdentityServer.Models;
using System.Threading.Tasks;
using static IdentityServer4.IdentityServerConstants;

namespace MultiShop.IdentityServer.Controllers
{
    [Authorize(LocalApi.PolicyName)] // Local API erişimi için yetkilendirme politikası kullanılır.
    [Route("api/[controller]")]
    [ApiController]
    public class RegistersController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager; // UserManager, Identity framework'ün kullanıcı yönetimi için kullanılan bir sınıfıdır.

        public RegistersController(UserManager<ApplicationUser> userManager) // UserManager, ApplicationUser türünde kullanıcıları yönetmek için kullanılır.
        {
            _userManager = userManager; // UserManager'ı enjekte ediyoruz.
        }
        [HttpPost]
        public async Task<IActionResult> UserRegister(UserRegisterDTO userRegisterDTO)
        {
            var values = new ApplicationUser // Yeni bir ApplicationUser nesnesi oluşturuyoruz.
            {
                UserName = userRegisterDTO.Username, // Kullanıcı adını DTO'dan alıyoruz.
                Email = userRegisterDTO.Email, // E-posta adresini DTO'dan alıyoruz.
                Name = userRegisterDTO.Name, // Adı DTO'dan alıyoruz.
                Surname = userRegisterDTO.Surname // Soyadı DTO'dan alıyoruz.
            };
            var result = await _userManager.CreateAsync(values, userRegisterDTO.Password); // Kullanıcıyı oluşturuyoruz ve sonucu alıyoruz.
            if (result.Succeeded) // Eğer kullanıcı oluşturma başarılı ise
            {
                return Ok("Kullanıcı Başarıyla Eklendi.."); // 200 OK döndürüyoruz.
            }
            else // Eğer kullanıcı oluşturma başarısız ise
            {
                return BadRequest("Bir Hata Meydana Geldi: "+result.Errors); // Hataları içeren bir BadRequest döndürüyoruz.
            }
        }
    
}
}
