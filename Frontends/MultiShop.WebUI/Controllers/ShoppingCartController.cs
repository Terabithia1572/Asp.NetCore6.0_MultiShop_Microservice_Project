using Microsoft.AspNetCore.Mvc;
using MultiShop.DTOLayer.BasketDTOs;
using MultiShop.WebUI.Services.BasketServices;
using MultiShop.WebUI.Services.CatalogServices.ProductServices;

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

        public async Task<IActionResult> Index()
        {
            var values = await _basketService.GetBasket(); // Sepeti getir
            return View(values); // Sepet görünümünü döndür
        }
        public async Task<IActionResult> AddBasketItem(string productID)
        {
            var values = await _productService.GetByIDProductAsync(productID); // Ürünü ID ile getir
            var items = new BasketItemDTO
            { // Sepet öğesi oluştur
                ProductID = values.ProductID,
                ProductName = values.ProductName,
                ProductPrice = values.ProductPrice,
                ProductQuantity = 1
            };
            await _basketService.AddBasketItem(items); // Sepete ürün ekle
            return RedirectToAction("Index"); // Sepet sayfasına yönlendir
        }
        public async Task<IActionResult> RemoveBasketItem(string productID)
        {
            await _basketService.RemoveBasketItem(productID); // Sepetten ürünü çıkar
            return RedirectToAction("Index"); // Sepet sayfasına yönlendir
        }
    }
}
