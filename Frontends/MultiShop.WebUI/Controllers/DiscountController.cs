using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Services.BasketServices;
using MultiShop.WebUI.Services.DiscountServices;

namespace MultiShop.WebUI.Controllers
{
    public class DiscountController : Controller
    {
        private readonly IDiscountService _discountService; // Kupon hizmeti
        private readonly IBasketService _basketService; // Sepet hizmeti

        public DiscountController(IDiscountService discountService, IBasketService basketService)
        {
            _discountService = discountService;
            _basketService = basketService;
        }

        [HttpGet]
        public PartialViewResult ConfirmDiscountCoupon()
        {
         
            return PartialView();
        }
        [HttpPost]
        public IActionResult ConfirmDiscountCoupon(string couponCode)
        {
            //couponCode = "merhaba"; // Test amaçlı sabit bir kupon kodu atandı
            var values = _discountService.GetDiscountCode(couponCode); // Kupon koduna göre kupon detaylarını aldık

            return View(values);
        }
    }
}
