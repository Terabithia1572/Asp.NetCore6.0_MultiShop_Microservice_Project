using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DTOLayer.OrderDTOs.OrderAddressDTO;
using MultiShop.WebUI.Services.Interfaces;
using MultiShop.WebUI.Services.OrderServices.OrderAddressServices;
using System.Linq;
using System.Threading.Tasks;

namespace MultiShop.WebUI.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderAddressService _orderAddressService;
        private readonly IUserService _userService;

        public OrderController(IOrderAddressService orderAddressService, IUserService userService)
        {
            _orderAddressService = orderAddressService;
            _userService = userService;
        }

        // 🏠 Ana Sayfa (adres ekleme vs. değil, sadece yönlendirme)
        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.directory1 = "Ana Sayfa";
            ViewBag.directory2 = "Sipariş İşlemleri";
            ViewBag.directory3 = "Siparişler";
            return View();
        }

        // 🧩 Ödeme adımında adres seçimi partial'ı
        [HttpGet]
        public async Task<IActionResult> GetAddressSelectionPartial()
        {
            var user = await _userService.GetUserInfo();
            var allAddresses = await _orderAddressService.GetAllOrderAddressAsync();

            // sadece giriş yapan kullanıcıya ait adresleri filtrele
            var userAddresses = allAddresses
                .Where(a => a.AddressUserID == user.ID)
                .ToList();

            return PartialView("~/Views/Order/_AddressSelectionPartial.cshtml", userAddresses);
        }

        // 🆕 Yeni adres ekleme
        [HttpPost]
        public async Task<IActionResult> AddAddress(CreateOrderAddressDTO dto)
        {
            var user = await _userService.GetUserInfo();
            dto.AddressUserID = user.ID;
            dto.AddressDescription ??= "Varsayılan Adres";

            await _orderAddressService.CreateOrderAddressAsync(dto);

            var allAddresses = await _orderAddressService.GetAllOrderAddressAsync();
            var userAddresses = allAddresses
                .Where(a => a.AddressUserID == user.ID)
                .ToList();

            return PartialView("~/Views/Order/_AddressSelectionPartial.cshtml", userAddresses);
        }

        // 💳 Adres seçilip ödeme ekranına geçildiğinde (geçici veri)
        [HttpPost]
        public IActionResult GoToPayment(int selectedAddressId)
        {
            TempData["SelectedAddressId"] = selectedAddressId;
            return Json(new { ok = true });
        }

        // 📦 Yeni adres kaydı (mevcut senin Index POST)
        [HttpPost]
        public async Task<IActionResult> Index(CreateOrderAddressDTO createOrderAddressDTO)
        {
            var values = await _userService.GetUserInfo();
            createOrderAddressDTO.AddressUserID = values.ID;
            createOrderAddressDTO.AddressDescription = "Varsayılan Adres";

            await _orderAddressService.CreateOrderAddressAsync(createOrderAddressDTO);

            return RedirectToAction("Index", "Payment");
        }
    }
}
