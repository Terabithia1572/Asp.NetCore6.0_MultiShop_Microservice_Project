using Microsoft.AspNetCore.Mvc;

namespace MultiShop.WebUI.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            // Tüm ürünleri göstereceğimiz sayfa
            ViewBag.id = "Tüm Ürünler"; // kategori sınırlaması olmadan
            return View();
        }
    }
}
