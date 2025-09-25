using Microsoft.AspNetCore.Mvc;

namespace MultiShop.WebUI.Controllers
{
    public class PaymentController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.directory1 = "Ana Sayfa"; // Breadcrumb için
            ViewBag.directory2 = "Ödeme İşlemleri"; // Breadcrumb için 
            ViewBag.directory3 = "Kartla Ödeme";  // Breadcrumb için
            return View();
        }
    }
}
