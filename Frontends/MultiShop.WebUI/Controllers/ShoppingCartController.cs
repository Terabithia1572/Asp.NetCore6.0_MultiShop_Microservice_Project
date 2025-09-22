using Microsoft.AspNetCore.Mvc;
using MultiShop.DTOLayer.BasketDTOs;
using MultiShop.WebUI.Services.BasketServices;
using MultiShop.WebUI.Services.CatalogServices.ProductServices;
using MultiShop.WebUI.Services.DiscountServices;

namespace MultiShop.WebUI.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IProductService _productService; // Ürün hizmeti
        private readonly IBasketService _basketService; // Sepet hizmeti

        public ShoppingCartController(IProductService productService, IBasketService basketService) // Yapıcı metod
        {
            _productService = productService; // Ürün hizmeti atanıyor
            _basketService = basketService; // Sepet hizmeti atanıyor
          
        }

        public async Task< IActionResult> Index(string couponCode)
        {
            ViewBag.couponCode= couponCode; // Kupon kodunu ViewBag ile view'a gönderiyoruz


            ViewBag.directory1 = "Ana Sayfa";
            ViewBag.directory2 = "Ürünler";
            ViewBag.directory3 = "Sepetim";
            var values = await _basketService.GetBasket(); // Sepet bilgilerini aldık
            ViewBag.total = values.TotalPrice; // Toplam tutarı ViewBag ile view'a gönderiyoruz
            var totalPriceWithTax = values.TotalPrice+ values.TotalPrice / 100 * 10; // KDV dahil toplam tutar
            var tax=values.TotalPrice / 100 * 10; // KDV tutarı
            ViewBag.tax = tax; // KDV tutarını ViewBag ile view'a gönderiyoruz
            ViewBag.totalPriceWithTax = totalPriceWithTax; // KDV dahil toplam tutarı ViewBag ile view'a gönderiyoruz
            return View();
        }
        
        public async Task<IActionResult> AddBasketItem(string id)
        {
            var values = await _productService.GetByIDProductAsync(id); // Ürünü ID ile getir
            var items = new BasketItemDTO
            { // Sepet öğesi oluştur
                ProductID = values.ProductID,
                ProductName = values.ProductName,
                ProductPrice = values.ProductPrice,
                ProductQuantity = 1,
                ProductImageURL= values.ProductImageURL
            };
            await _basketService.AddBasketItem(items); // Sepete ürün ekle
            return RedirectToAction("Index"); // Sepet sayfasına yönlendir
        }
        public async Task<IActionResult> RemoveBasketItem(string id)
        {
            await _basketService.RemoveBasketItem(id); // Sepetten ürünü çıkar
            return RedirectToAction("Index"); // Sepet sayfasına yönlendir
        }
    }
}
