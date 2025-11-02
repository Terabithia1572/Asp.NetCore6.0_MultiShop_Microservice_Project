using Microsoft.AspNetCore.Mvc;
using MultiShop.DTOLayer.CargoDTOs.CargoCompanyDTOs;
using MultiShop.WebUI.Services.CargoServices.CargoCompanyServices;
using System.Threading.Tasks;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]")]
    public class CargoController : Controller
    {
        private readonly ICargoCompanyService _cargoCompanyService;

        public CargoController(ICargoCompanyService cargoCompanyService)
        {
            _cargoCompanyService = cargoCompanyService;
        }

        // LIST
        [HttpGet]
        public async Task<IActionResult> CargoCompanyList()
        {
            var values = await _cargoCompanyService.GetAllCargoCompanyAsync();
            return View(values);
        }

        // CREATE (GET)
        [HttpGet]
        public IActionResult CreateCargoCompany()
        {
            return View();
        }

        // CREATE (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCargoCompany(CreateCargoCompanyDTO dto)
        {
            if (!ModelState.IsValid)
            {
                TempData["err"] = "Lütfen gerekli alanları doldurun.";
                return View(dto);
            }

            await _cargoCompanyService.CreateCargoCompanyAsync(dto);
            TempData["ok"] = "Kargo firması başarıyla eklendi.";
            return RedirectToAction(nameof(CargoCompanyList));
        }

        // UPDATE (GET)
        [HttpGet("{id:int}")]
        public async Task<IActionResult> UpdateCargoCompany(int id)
        {
            var value = await _cargoCompanyService.GetByIDCargoCompanyAsync(id);
            if (value == null)
            {
                TempData["err"] = "Kargo firması bulunamadı.";
                return RedirectToAction(nameof(CargoCompanyList));
            }
            return View(value);
        }

        // UPDATE (POST)
        [HttpPost("{id:int}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateCargoCompany(UpdateCargoCompanyDTO dto)
        {
            if (!ModelState.IsValid)
            {
                TempData["err"] = "Lütfen gerekli alanları doldurun.";
                return View(dto);
            }

            await _cargoCompanyService.UpdateCargoCompanyAsync(dto);
            TempData["ok"] = "Kargo firması başarıyla güncellendi.";
            return RedirectToAction(nameof(CargoCompanyList));
        }

        // DELETE
        [HttpGet("{id:int}")]
        public async Task<IActionResult> DeleteCargoCompany(int id)
        {
            await _cargoCompanyService.DeleteCargoCompanyAsync(id);
            TempData["ok"] = "Kargo firması silindi.";
            return RedirectToAction(nameof(CargoCompanyList));
        }
    }
}
