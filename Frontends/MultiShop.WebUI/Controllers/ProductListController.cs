using Microsoft.AspNetCore.Mvc;

namespace MultiShop.WebUI.Controllers
{
    public class ProductListController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ProductDetail() //Bu aksin, ürün detaylarını göstermek için kullanılacak
        {
            return View(); // Bu aksiyon, ürün detaylarını göstermek için kullanılacak
        }
    }
}
