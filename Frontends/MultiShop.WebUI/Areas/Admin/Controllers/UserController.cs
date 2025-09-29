using Microsoft.AspNetCore.Mvc;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")] // Bu Area'nın Admin olduğunu belirtir
    public class UserController : Controller
    {
        public async Task< IActionResult> UserList()
        {
            return View();
        }
    }
}
