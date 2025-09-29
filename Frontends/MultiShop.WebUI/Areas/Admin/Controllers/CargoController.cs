using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Services.CargoServices.CargoCompanyServices;
using System.Threading.Tasks;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")] // Bu Area'nın "Admin Area" olduğunu belirtir
    public class CargoController : Controller
    {
        private readonly ICargoCompanyService _cargoCompanyService; // ICargoCompanyService arayüzü için bir alan

        public CargoController(ICargoCompanyService cargoCompanyService)
        {
            _cargoCompanyService = cargoCompanyService;
        }

        public async Task<IActionResult> CargoCompanyList()
        {
            var values=await _cargoCompanyService.GetAllCargoCompanyAsync();
            return View(values);
        }
    }
}
