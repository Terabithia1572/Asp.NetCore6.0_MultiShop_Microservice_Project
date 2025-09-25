using Microsoft.AspNetCore.Mvc;
using MultiShop.DTOLayer.OrderDTOs.OrderAddressDTO;
using MultiShop.WebUI.Services.Interfaces;
using MultiShop.WebUI.Services.OrderServices.OrderAddressServices;
using System.Threading.Tasks;

namespace MultiShop.WebUI.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderAddressService _orderAddressService; //OrderAddressService nesnesi
        private readonly IUserService _userService; //UserService nesnesi

        public OrderController(IOrderAddressService orderAddressService, IUserService userService)
        {
            _orderAddressService = orderAddressService;
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.directory1 = "Ana Sayfa"; // Breadcrumb için
            ViewBag.directory2 = "Sipariş İşlemleri"; // Breadcrumb için 
            ViewBag.directory3 = "Siparişler";  // Breadcrumb için
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(CreateOrderAddressDTO createOrderAddressDTO)
        {
          
            var values=await _userService.GetUserInfo(); // Kullanıcı bilgilerini al
            
            createOrderAddressDTO.AddressUserID= values.ID; // Kullanıcı ID'sini DTO'ya ata
            createOrderAddressDTO.AddressDescription="Varsayılan Adres"; // Adres açıklamasını ata

            await _orderAddressService.CreateOrderAddressAsync(createOrderAddressDTO); // Yeni adres oluşturma işlemi

            return RedirectToAction("Index","Payment"); // Sipariş sayfası yerine ödeme sayfasına yönlendir
        }
    }
}
