using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Services.CargoServices.CargoCustomerServices;
using MultiShop.WebUI.Services.UserIdentityServices;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")] // Bu Area'nın Admin olduğunu belirtir
    public class UserController : Controller
    {
        private readonly IUserIdentityService _userIdentityService; //Kullanıcı servisi
        private readonly ICargoCustomerService _cargoCustomerService; //Kargo müşteri servisi

        public UserController(IUserIdentityService userIdentityService, ICargoCustomerService cargoCustomerService)
        {
            _userIdentityService = userIdentityService;
            _cargoCustomerService = cargoCustomerService;
        }

        public async Task< IActionResult> UserList()
        {
            var values = await _userIdentityService.GetAllUserListAsync(); //Tüm kullanıcıları getirir
            return View(values);
        }
        public async Task<IActionResult> UserAddressInfo(string id)
        {
            var values = await _cargoCustomerService.GetByIDCargoCustomerInfoAsync(id);

            if (values == null)
            {
                TempData["ErrorMessage"] = "Adres bilgisi bulunamadı.";
                return RedirectToAction("Index"); // veya hata sayfası
            }

            return View(values);
        }

    }
}
