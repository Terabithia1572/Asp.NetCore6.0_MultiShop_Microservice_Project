using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Services.Interfaces;
using MultiShop.WebUI.Services.OrderServices.OrderOrderingServices;

namespace MultiShop.WebUI.Areas.User.Controllers
{
    [Area("User")] // User alanını belirtir
    public class MyOrderController : Controller
    {
        private readonly IOrderOrderingService _orderOrderingService;
        private readonly IUserService _userService;

        public MyOrderController(IOrderOrderingService orderOrderingService, IUserService userService)
        {
            _orderOrderingService = orderOrderingService;
            _userService = userService;
        }

        public async Task< IActionResult> MyOrderList()
        {
             var userID = User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value;
            // var userID = await _userService.GetUserInfo();
            //var values =await _orderOrderingService.GetOrderingByUserID(userID.ID); böylede getirir ID'yi
            var values =await _orderOrderingService.GetOrderingByUserID(userID);
            return View(values);
        }
    }
}
