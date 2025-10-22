using Microsoft.AspNetCore.Mvc;
using MultiShop.DTOLayer.BasketDTOs;
using MultiShop.WebUI.Services.BasketServices;
using MultiShop.WebUI.Services.CatalogServices.ProductDiscountServices;
using MultiShop.WebUI.Services.CatalogServices.ProductServices;
using MultiShop.WebUI.Services.DiscountServices;

namespace MultiShop.WebUI.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IProductService _productService; // Ürün hizmeti
        private readonly IBasketService _basketService; // Sepet hizmeti
        private readonly IProductDiscountService _productDiscountService; // Ürün indirim hizmeti


        public ShoppingCartController(IProductService productService, IBasketService basketService, IProductDiscountService productDiscountService) // Yapıcı metod
        {
            _productService = productService; // Ürün hizmeti atanıyor
            _basketService = basketService; // Sepet hizmeti atanıyor                                            
            _productDiscountService = productDiscountService;//_productDiscountService = productDiscountService; // Ürün indirim hizmeti atanıyor

        }

        public async Task<IActionResult> Index(string couponCode, int discountRate, decimal totalAfterDiscount)
        {
            ViewBag.couponCode = couponCode; // Kupon kodunu ViewBag ile view'a gönderiyoruz
            ViewBag.discountRate = discountRate; // İndirim oranını ViewBag ile view'a gönderiyoruz
            ViewBag.totalAfterDiscount = totalAfterDiscount; // İndirim sonrası toplam tutarı ViewBag ile view'a gönderiyoruz
            ViewBag.directory1 = "Ana Sayfa";
            ViewBag.directory2 = "Ürünler";
            ViewBag.directory3 = "Sepetim";
            var values = await _basketService.GetBasket(); // Sepet bilgilerini aldık
            ViewBag.total = values.TotalPrice; // Toplam tutarı ViewBag ile view'a gönderiyoruz
            var totalPriceWithTax = values.TotalPrice + values.TotalPrice / 100 * 10; // KDV dahil toplam tutar
            var tax = values.TotalPrice / 100 * 10; // KDV tutarı
            ViewBag.tax = tax; // KDV tutarını ViewBag ile view'a gönderiyoruz
            ViewBag.totalPriceWithTax = totalPriceWithTax; // KDV dahil toplam tutarı ViewBag ile view'a gönderiyoruz
            return View();
        }

        public async Task<IActionResult> AddBasketItem(string id)
        {
            var product = await _productService.GetByIDProductAsync(id);

            var discounts = await _productDiscountService.GetAllProductDiscountAsync();
            var activeDiscount = discounts.FirstOrDefault(d =>
                d.ProductID == product.ProductID &&
                d.IsActive &&
                d.StartDate <= DateTime.Now &&
                d.EndDate >= DateTime.Now);

            decimal finalPrice = product.ProductPrice;
            if (activeDiscount != null)
            {
                finalPrice = product.ProductPrice - (product.ProductPrice * activeDiscount.DiscountRate / 100);
            }

            var items = new BasketItemDTO
            {
                ProductID = product.ProductID,
                ProductName = product.ProductName,
                ProductPrice = Math.Round(finalPrice, 2),
                ProductQuantity = 1,
                ProductImageURL = product.ProductImageURL
            };

            await _basketService.AddBasketItem(items);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> RemoveBasketItem(string id)
        {
            await _basketService.RemoveBasketItem(id); // Sepetten ürünü çıkar
            return RedirectToAction("Index"); // Sepet sayfasına yönlendir
        }
        // ⚡ Yeni AJAX versiyonu:
        [HttpPost]
        public async Task<IActionResult> AddBasketItemAjax(string id)
        {
            try
            {
                var product = await _productService.GetByIDProductAsync(id);

                // 🔍 Ürüne ait indirim var mı kontrol et
                var discounts = await _productDiscountService.GetAllProductDiscountAsync();
                var activeDiscount = discounts.FirstOrDefault(d =>
                    d.ProductID == product.ProductID &&
                    d.IsActive &&
                    d.StartDate <= DateTime.Now &&
                    d.EndDate >= DateTime.Now);

                // 💰 Fiyat hesapla
                decimal finalPrice = product.ProductPrice;
                if (activeDiscount != null)
                {
                    finalPrice = product.ProductPrice - (product.ProductPrice * activeDiscount.DiscountRate / 100);
                }

                var item = new BasketItemDTO
                {
                    ProductID = product.ProductID,
                    ProductName = product.ProductName,
                    ProductPrice = Math.Round(finalPrice, 2),
                    ProductQuantity = 1,
                    ProductImageURL = product.ProductImageURL
                };

                await _basketService.AddBasketItem(item);

                var miniCart = await _basketService.GetBasket();
                return PartialView("~/Views/Shared/Components/_MiniCartPartialView/_MiniCartPartialView.cshtml", miniCart);
            }
            catch
            {
                return BadRequest();
            }
        }
        // 📦 Mini Cart verisini getirmek için (sayfa açılışında)
        [HttpGet]
        public async Task<IActionResult> GetMiniCart()
        {
            var basket = await _basketService.GetBasket();
            return PartialView("~/Views/Shared/Components/_MiniCartPartialView/_MiniCartPartialView.cshtml", basket);
        }

        // 🔢 Sepetteki toplam ürün sayısını getirmek için
        [HttpGet]
        public async Task<IActionResult> GetCartCount()
        {
            var basket = await _basketService.GetBasket();
            var count = basket?.BasketItems?.Count ?? 0;
            return Json(count);
        }
        [HttpPost]
        public async Task<IActionResult> RemoveBasketItemAjax(string id)
        {
            await _basketService.RemoveBasketItem(id);
            var basket = await _basketService.GetBasket();
            return PartialView("~/Views/Shared/Components/_MiniCartPartialView/_MiniCartPartialView.cshtml", basket);
        }
        [HttpPost]
        public async Task<IActionResult> RemoveBasketItemFromCartAjax(string id)
        {
            await _basketService.RemoveBasketItem(id);

            // Güncel sepeti al
            var basket = await _basketService.GetBasket();

            // 🔹 ViewComponent çağırıyoruz — sadece sepet tablosu yenilenecek
            return ViewComponent("_ShoppingCartProductListComponentPartial", basket.BasketItems);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateQuantityAjax(string id, int quantity)
        {
            var basket = await _basketService.GetBasket();
            var item = basket.BasketItems.FirstOrDefault(x => x.ProductID == id);
            if (item != null)
            {
                item.ProductQuantity = quantity;
                await _basketService.SaveBasket(basket);
            }
            return Ok();
        }


    }
}
