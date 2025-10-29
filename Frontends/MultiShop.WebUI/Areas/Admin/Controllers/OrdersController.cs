using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DTOLayer.OrderDTOs.OrderDetailDTO;
using MultiShop.DTOLayer.OrderDTOs.OrderingDTOs;
using MultiShop.WebUI.Areas.Admin.Models;
using MultiShop.WebUI.Services.CatalogServices.ProductServices;
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
        private readonly IProductService _productService;          // 🔥 EKLEDİK

        public OrdersController(
            IOrderOrderingService orderOrderingService,
            IOrderDetailService orderDetailService,
            IUserService userService,
            IProductService productService)
        {
            _orderOrderingService = orderOrderingService;
            _orderDetailService = orderDetailService;
            _userService = userService;
            _productService = productService;
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
            if (user == null) return RedirectToAction("Index", "Login");

            // sipariş sahipliği
            var myOrders = await _orderOrderingService.GetOrderingByUserID(user.ID);
            if (myOrders == null || !myOrders.Any(x => x.OrderingID == id))
                return Unauthorized();

            // mevcut servisini kullan: CreateOrderDetailDTO listesi geliyor (artık ProductAmount da var)
            var details = await _orderDetailService.GetOrderDetailsByOrderingIdAsync(id);
            var list = new List<OrderDetailWithImageVM>();

            foreach (var d in details ?? Enumerable.Empty<CreateOrderDetailDTO>())
            {
                var prod = await _productService.GetProductByIdAsync(d.ProductID); // ProductID → ürün
                list.Add(new OrderDetailWithImageVM
                {
                    ProductID = d.ProductID,
                    ProductName = d.ProductName,
                    ProductPrice = d.ProductPrice,
                    Quantity = d.ProductQuantity != 0 ? d.ProductQuantity : d.ProductAmount, // 🔥 normalize
                    ProductTotalPrice = d.ProductTotalPrice,
                    ProductImageUrl = prod?.ProductImageURL
                });
            }

            ViewBag.OrderingID = id;
            return View(list);
        }

    }
}
