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
        private readonly IWebHostEnvironment _env; // ⬅️ eklendi

        public UserController(IUserIdentityService userIdentityService, ICargoCustomerService cargoCustomerService, IWebHostEnvironment env)
        {
            _userIdentityService = userIdentityService;
            _cargoCustomerService = cargoCustomerService;
            _env = env;
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
            var form = Request.Form;

            // 1) Dosyayı UI/wwwroot/profile-images içine kaydet
            string? profileUrl = null;
            if (Request.Form.Files != null && Request.Form.Files.Count > 0)
            {
                var file = Request.Form.Files["ProfileImage"];
                if (file != null && file.Length > 0)
                {
                    var imagesRoot = Path.Combine(_env.WebRootPath, "profile-images");
                    if (!Directory.Exists(imagesRoot)) Directory.CreateDirectory(imagesRoot);

                    var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                    var savePath = Path.Combine(imagesRoot, fileName);
                    using (var fs = new FileStream(savePath, FileMode.Create))
                        await file.CopyToAsync(fs);

                    // UI tarafında kullanılacak relative url
                    profileUrl = $"/profile-images/{fileName}";
                }
            }

            // 2) IdentityServer’a JSON gönder
            var dto = new UpdateUserDTO
            {
                Id = form["Id"],
                Name = form["Name"],
                Surname = form["Surname"],
                Email = form["Email"],
                PhoneNumber = form["PhoneNumber"],
                City = form["City"],
                Gender = form["Gender"],
                About = form["About"],
                NewPassword = string.IsNullOrWhiteSpace(form["NewPassword"]) ? null : form["NewPassword"].ToString(),
                ProfileImageUrl = profileUrl // null olabilir → API dokunmaz
            };

            var ok = await _userIdentityService.UpdateUserAsync(dto);
            if (!ok)
                return BadRequest(new { message = "Güncelleme başarısız." });

            return Ok(new { success = true });
        }

    }
}
