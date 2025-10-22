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
        public async Task< IActionResult> ConfirmDiscountCoupon(string couponCode)
        {
            ////couponCode = "merhaba"; // Test amaçlı sabit bir kupon kodu atandı
            //var values = _discountService.GetDiscountCode(couponCode); // Kupon koduna göre kupon detaylarını aldık

            //var basketValues =await _basketService.GetBasket(); // Sepet bilgilerini aldık
            //var totalPriceWithTax = basketValues.TotalPrice + basketValues.TotalPrice / 100 * 10; // KDV dahil toplam tutar
            //var tax = basketValues.TotalPrice / 100 * 10; // KDV tutarı
            //var totalAfterDiscount = totalPriceWithTax - (totalPriceWithTax / 100 * values.Result.CouponRate); // İndirim sonrası toplam tutar
            //ViewBag.totalAfterDiscount = totalAfterDiscount; // İndirim sonrası toplam tutarı ViewBag ile view'a gönderiyoruz
            ///*
            //   var values = await _basketService.GetBasket(); // Sepet bilgilerini aldık
            //ViewBag.total = values.TotalPrice; // Toplam tutarı ViewBag ile view'a gönderiyoruz
            //var totalPriceWithTax = values.TotalPrice+ values.TotalPrice / 100 * 10; // KDV dahil toplam tutar
            //var tax=values.TotalPrice / 100 * 10; // KDV tutarı
            //ViewBag.tax = tax; // KDV tutarını ViewBag ile view'a gönderiyoruz
            //ViewBag.totalPriceWithTax = totalPriceWithTax; // KDV dahil toplam tutarı ViewBag ile view'a gönderiyoruz
            // */

            var values=await _discountService.GetDiscountCouponCountRate(couponCode); // Kupon koduna göre kupon detaylarını aldık
            var basketValues = await _basketService.GetBasket(); // Sepet bilgilerini aldık
            var totalPriceWithTax = basketValues.TotalPrice + basketValues.TotalPrice / 100 * 10; // KDV dahil toplam tutar
            var totalNewPriceWithDiscount= totalPriceWithTax - (totalPriceWithTax / 100 * values); // İndirim sonrası toplam tutar
         //   ViewBag.totalAfterDiscount = totalNewPriceWithDiscount; // İndirim sonrası toplam tutarı ViewBag ile view'a gönderiyoruz

            return RedirectToAction("Index", "ShoppingCart",new {couponCode=couponCode,discountRate=values, totalAfterDiscount = totalNewPriceWithDiscount }); // Sepet sayfasına yönlendir ve kupon kodunu gönder

           
        }
        [HttpGet]
        public async Task<IActionResult> ConfirmDiscountCouponPartial()
        {
            var basketValues = await _basketService.GetBasket();
            decimal discountRate = 0;

            // Eğer kupon uygulandıysa tempData veya ViewBag’den oran çekebilirsin
            if (TempData["discountRate"] != null)
                discountRate = Convert.ToDecimal(TempData["discountRate"]);

            var totalPriceWithTax = basketValues.TotalPrice + basketValues.TotalPrice * 0.18m;
            var tax = basketValues.TotalPrice * 0.18m;
            var totalAfterDiscount = totalPriceWithTax - (totalPriceWithTax * discountRate / 100);

            ViewBag.total = basketValues.TotalPrice.ToString("N2");
            ViewBag.tax = tax.ToString("N2");
            ViewBag.discountRate = discountRate.ToString("N2");
            ViewBag.totalPriceWithTax = totalPriceWithTax.ToString("N2");
            ViewBag.totalAfterDiscount = totalAfterDiscount.ToString("N2");

            return PartialView("~/Views/Discount/ConfirmDiscountCoupon.cshtml");
        }

    }
}
