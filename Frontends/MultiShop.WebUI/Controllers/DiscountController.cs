using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Services.DiscountServices;

namespace MultiShop.WebUI.Controllers
{
    public class DiscountController : Controller
    {
        private readonly IDiscountService _discountService; // Kupon hizmeti

        public DiscountController(IDiscountService discountService)
        {
            _discountService = discountService;
        }

        [HttpPost]
        public IActionResult ConfirmDiscountCoupon(string couponCode)
        {
            couponCode = "merhaba"; // Test amaçlı sabit bir kupon kodu atandı
            var values = _discountService.GetDiscountCode(couponCode); // Kupon koduna göre kupon detaylarını aldık

            return View(values);
        }
    }
}
