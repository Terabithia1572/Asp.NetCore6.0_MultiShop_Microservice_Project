using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Services.Interfaces; // userService interface'ini kullanabilmek için

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]")]
    public class LiveChatController : Controller
    {
        private readonly IUserService _userService;

        public LiveChatController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userService.GetUserInfo();
            ViewBag.CurrentUser = user?.Name + " " + user?.Surname ?? "Ziyaretçi";
            return View();
        }
    }
}
