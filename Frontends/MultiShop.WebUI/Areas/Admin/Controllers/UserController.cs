using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Services.CargoServices.CargoCustomerServices;
using MultiShop.WebUI.Services.UserIdentityServices;
using MultiShop.DTOLayer.IdentityDTOs.UserDTOs;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly IUserIdentityService _userIdentityService;
        private readonly ICargoCustomerService _cargoCustomerService;

        public UserController(IUserIdentityService userIdentityService, ICargoCustomerService cargoCustomerService)
        {
            _userIdentityService = userIdentityService;
            _cargoCustomerService = cargoCustomerService;
        }

        public async Task<IActionResult> UserList()
        {
            var values = await _userIdentityService.GetAllUserListAsync();
            ViewBag.v1 = "Ana Sayfa";
            ViewBag.v2 = "Kullanıcılar";
            ViewBag.v3 = "Kullanıcı Listesi";
            ViewBag.v4 = "Yönetim Paneli";
            return View(values);
        }

        [HttpGet]
        public async Task<IActionResult> GetUserInfo(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest(new { message = "Geçersiz kullanıcı ID." });

            var user = await _userIdentityService.GetUserByIdAsync(id);
            if (user == null)
                return NotFound(new { message = "Kullanıcı bulunamadı." });

            return Ok(user);
        }

        [HttpGet]
        public async Task<IActionResult> GetUserAddress(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest(new { message = "Geçersiz kullanıcı ID." });

            var address = await _cargoCustomerService.GetByIDCargoCustomerInfoAsync(id);
            if (address == null)
                return NotFound(new { message = "Adres bilgisi bulunamadı." });

            return Ok(address);
        }

        // 🔄 UI’dan gelen FormData’yı service’e iletir
        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> UpdateUserInfo()
        {
            var form = await Request.ReadFormAsync();

            string id = form["Id"];
            string name = form["Name"];
            string surname = form["Surname"];
            string email = form["Email"];
            string phone = form["PhoneNumber"];
            string city = form["City"];
            string gender = form["Gender"];
            string about = form["About"];
            string newPassword = form["NewPassword"];

            IFormFile? file = form.Files.FirstOrDefault();

            var (ok, message) = await _userIdentityService.UpdateUserAsyncMultipart(
                id, name, surname, email, phone, city, gender, about,
                string.IsNullOrWhiteSpace(newPassword) ? null : newPassword,
                file
            );

            if (!ok)
                return BadRequest(message); // JS tarafında bu mesajı aynen göstereceğiz

            return Ok(new { success = true });
        }

    }
}
