using Microsoft.AspNetCore.Mvc;

namespace MultiShop.WebUI.Controllers
{
    public class ProductListController : Controller
    {
        public IActionResult Index(string id)
        {
            ViewBag.id = id; // Bu aksiyon, ürün listesini göstermek için kullanılacak
            return View();
        }
        public IActionResult ProductDetail(string id) //Bu aksin, ürün detaylarını göstermek için kullanılacak
        {
            ViewBag.productID = id; // Ürün ID'sini ViewBag'e atıyoruz

            return View(); // Bu aksiyon, ürün detaylarını göstermek için kullanılacak
        }
    }
}
