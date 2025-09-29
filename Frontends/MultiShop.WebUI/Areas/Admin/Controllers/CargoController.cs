using Microsoft.AspNetCore.Mvc;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")] // Bu Area'nın "Admin Area" olduğunu belirtir
    public class CargoController : Controller
    {
        public IActionResult CargoList()
        {
            return View();
        }
    }
}
