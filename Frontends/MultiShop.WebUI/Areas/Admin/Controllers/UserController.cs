using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Services.UserIdentityServices;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")] // Bu Area'nın Admin olduğunu belirtir
    public class UserController : Controller
    {
        private readonly IUserIdentityService _userIdentityService; //Kullanıcı servisi

        public UserController(IUserIdentityService userIdentityService)
        {
            _userIdentityService = userIdentityService;
        }

        public async Task< IActionResult> UserList()
        {
            var values = await _userIdentityService.GetAllUserListAsync(); //Tüm kullanıcıları getirir
            return View(values);
        }
    }
}
