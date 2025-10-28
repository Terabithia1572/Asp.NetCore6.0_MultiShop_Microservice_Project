using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DTOLayer.OrderDTOs.OrderAddressDTO;
using MultiShop.WebUI.Services.Interfaces;
using MultiShop.WebUI.Services.OrderServices.OrderAddressServices;

// 🔹 Sepet ve Sipariş servisleri
using MultiShop.WebUI.Services.BasketServices;
using MultiShop.WebUI.Services.OrderServices.OrderOrderingServices;
using MultiShop.WebUI.Services.OrderServices.OrderDetailServices;

// 🔹 DTO'lar
using MultiShop.DTOLayer.OrderDTOs.OrderingDTO;
using MultiShop.DTOLayer.OrderDTOs.OrderDetailDTO;

namespace MultiShop.WebUI.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderAddressService _orderAddressService;
        private readonly IUserService _userService;
        private readonly IBasketService _basketService;
        private readonly IOrderOrderingService _orderOrderingService;

        public OrderController(
            IOrderAddressService orderAddressService,
            IUserService userService,
            IBasketService basketService,
            IOrderOrderingService orderOrderingService
        )
        {
            _orderAddressService = orderAddressService;
            _userService = userService;
            _basketService = basketService;
            _orderOrderingService = orderOrderingService;
        }

        // 📍 Ana Sayfa
        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.directory1 = "Ana Sayfa";
            ViewBag.directory2 = "Sipariş İşlemleri";
            ViewBag.directory3 = "Siparişler";
            return View();
        }

        // 🧩 Adres seçimi partial'ı
        [HttpGet]
        public async Task<IActionResult> GetAddressSelectionPartial()
        {
            var user = await _userService.GetUserInfo();
            var allAddresses = await _orderAddressService.GetAllOrderAddressAsync();
            var userAddresses = allAddresses.Where(a => a.AddressUserID == user.ID).ToList();

            return PartialView("~/Views/Order/_AddressSelectionPartial.cshtml", userAddresses);
        }

        // ➕ Yeni adres ekleme
        [HttpPost]
        public async Task<IActionResult> AddAddress(CreateOrderAddressDTO dto)
        {
            var user = await _userService.GetUserInfo();
            dto.AddressUserID = user.ID;
            dto.AddressDescription ??= "Varsayılan Adres";

            await _orderAddressService.CreateOrderAddressAsync(dto);

            var allAddresses = await _orderAddressService.GetAllOrderAddressAsync();
            var userAddresses = allAddresses.Where(a => a.AddressUserID == user.ID).ToList();

            return PartialView("~/Views/Order/_AddressSelectionPartial.cshtml", userAddresses);
        }

        // 💳 Adres seçilip ödeme ekranına geçildiğinde
        [HttpPost]
        public async Task<IActionResult> GoToPayment(int selectedAddressId)
        {
            TempData["SelectedAddressId"] = selectedAddressId;
            var basket = await _basketService.GetBasket();

            if (basket == null || basket.BasketItems == null || !basket.BasketItems.Any())
                return Json(new { ok = false, message = "Sepetiniz boş görünüyor." });

            return PartialView("~/Views/Order/_PaymentCardPartial.cshtml", basket);
        }

        // 💰 Ödeme ekranı (AJAX yenileme)
        [HttpGet]
        public async Task<IActionResult> GetPaymentCardPartial()
        {
            var basket = await _basketService.GetBasket();
            return PartialView("~/Views/Order/_PaymentCardPartial.cshtml", basket);
        }

        // ✅ Ödemeyi Tamamlama
        [HttpPost]
        public async Task<IActionResult> CompleteOrder()
        {
            var user = await _userService.GetUserInfo();
            var basket = await _basketService.GetBasket();

            if (basket == null || basket.BasketItems == null || !basket.BasketItems.Any())
            {
                TempData["Error"] = "Sepetiniz boş görünüyor!";
                return RedirectToAction("Index", "ShoppingCart");
            }

            decimal totalPrice = basket.BasketItems.Sum(x => x.ProductPrice * x.ProductQuantity);

            var newOrder = new CreateOrderingDTO
            {
                OrderingUserID = user.ID,
                OrderingTotalPrice = totalPrice,
                OrderingDate = DateTime.Now
            };

            // 🔹 Siparişi kaydet
            await _orderOrderingService.CreateOrderingAsync(newOrder);

            // 🔹 Sepeti silmek yerine boşaltıyoruz
            await _basketService.ClearBasketAsync();

            // 🔹 Ödeme başarılı sayfasına yönlendir
            return RedirectToAction("Success");
        }


        // 🎉 Başarılı ekran
        [HttpGet]
        public IActionResult Success()
        {
            ViewBag.Title = "Ödeme Başarılı";
            return View("~/Views/Order/Success.cshtml");
        }
    }
}
