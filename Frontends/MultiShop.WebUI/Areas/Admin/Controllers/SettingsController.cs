using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Services.UserIdentityServices;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]")]
    public class SettingController : Controller
    {
        private readonly IUserIdentityService _userIdentityService;

        public SettingController(IUserIdentityService userIdentityService)
        {
            _userIdentityService = userIdentityService;
        }

        public async Task<IActionResult> Index()
        {
            // 🔹 Giriş yapan kullanıcıyı (örneğin ID = cookie / token’dan)
            // geçici olarak ilk kullanıcıyı çekelim
            var users = await _userIdentityService.GetAllUserListAsync();
            var currentUser = users.FirstOrDefault();

            return View(currentUser);
        }
    }
}
