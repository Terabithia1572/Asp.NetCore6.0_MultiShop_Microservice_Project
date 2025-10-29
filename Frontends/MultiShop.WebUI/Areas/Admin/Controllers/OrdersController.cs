using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DTOLayer.OrderDTOs.OrderDetailDTO;
using MultiShop.DTOLayer.OrderDTOs.OrderingDTOs;
using MultiShop.WebUI.Services.Interfaces;
using MultiShop.WebUI.Services.OrderServices.OrderDetailServices;
using MultiShop.WebUI.Services.OrderServices.OrderOrderingServices;
using System.Threading.Tasks;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]")]
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IOrderOrderingService _orderOrderingService;
        private readonly IOrderDetailService _orderDetailService;
        private readonly IUserService _userService; // 🧩 eklendi

        public OrdersController(
            IOrderOrderingService orderOrderingService,
            IOrderDetailService orderDetailService,
            IUserService userService)
        {
            _orderOrderingService = orderOrderingService;
            _orderDetailService = orderDetailService;
            _userService = userService;
        }

        // 🔸 Giriş yapan kullanıcının kendi siparişlerini listele
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userService.GetUserInfo();
            if (user == null || string.IsNullOrEmpty(user.ID))
            {
                // login kontrolü
                return RedirectToAction("Index", "Login");
            }

            var orders = await _orderOrderingService.GetOrderingByUserID(user.ID);
            return View(orders);
        }

        // 🔹 Seçilen siparişin detayları
        [HttpGet("{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var user = await _userService.GetUserInfo();
            if (user == null)
            {
                return RedirectToAction("Index", "Login");
            }

            var details = await _orderDetailService.GetOrderDetailsByOrderingIdAsync(id);

            // 🧠 Güvenlik kontrolü: sipariş gerçekten bu kullanıcıya mı ait?
            // (opsiyonel ama iyi bir pratik)
            var orderList = await _orderOrderingService.GetOrderingByUserID(user.ID);
            var ownsOrder = orderList.Any(x => x.OrderingID == id);

            if (!ownsOrder)
            {
                return Unauthorized(); // başkasının siparişini görmeye çalışırsa
            }

            ViewBag.OrderingID = id;
            return View(details);
        }
    }
}
