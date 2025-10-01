using Microsoft.AspNetCore.Mvc;
using MultiShop.DTOLayer.CargoDTOs.CargoCompanyDTOs;
using MultiShop.WebUI.Services.CargoServices.CargoCompanyServices;
using System.Threading.Tasks;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")] // Bu Area'nın "Admin Area" olduğunu belirtir
    [Route("Admin/[controller]/[action]")] // Route şablonu

    public class CargoController : Controller
    {
        private readonly ICargoCompanyService _cargoCompanyService; // ICargoCompanyService arayüzü için bir alan

        public CargoController(ICargoCompanyService cargoCompanyService)
        {
            _cargoCompanyService = cargoCompanyService; // Arayüzü yapıcıya enjekte et
        }

        public async Task<IActionResult> CargoCompanyList()
        {
            var values=await _cargoCompanyService.GetAllCargoCompanyAsync(); // Tüm kargo şirketlerini asenkron olarak al
            return View(values); // Değerleri görünüme gönder
        }
        [HttpGet] // Sadece GET istekleri için
        public IActionResult CreateCargoCompany()
        {
            return View(); // Değerleri görünüme gönder
        }
        [HttpPost] // Sadece POST istekleri için
        public async Task<IActionResult> CreateCargoCompany(CreateCargoCompanyDTO createCargoCompanyDTO)
        {
            await _cargoCompanyService.CreateCargoCompanyAsync(createCargoCompanyDTO); // Yeni kargo şirketi oluştur
            return RedirectToAction("CargoCompanyList","Cargo",new {Area="Admin"}); // Kargo şirketi listesine yönlendir
        }
        [HttpGet("{id}")] // Sadece GET istekleri için
        public async Task<IActionResult> UpdateCargoCompany(int id)
        {
            var values=await _cargoCompanyService.GetByIDCargoCompanyAsync(id); // ID ile kargo şirketini al
            return View(values); // Değerleri görünüme gönder
        }
        [HttpPost("{id}")] // Sadece POST istekleri için
        public async Task<IActionResult> UpdateCargoCompany(UpdateCargoCompanyDTO updateCargoCompanyDTO)
        {
            await _cargoCompanyService.UpdateCargoCompanyAsync(updateCargoCompanyDTO); // Kargo şirketini güncelle
            return RedirectToAction("CargoCompanyList","Cargo",new {Area="Admin"}); // Kargo şirketi listesine yönlendir
        }
        [HttpGet("{id}")] // Sadece GET istekleri için
        public async Task<IActionResult> DeleteCargoCompany(int id)
        {
            await _cargoCompanyService.DeleteCargoCompanyAsync(id); // Kargo şirketini sil
            return RedirectToAction("CargoCompanyList","Cargo",new {Area="Admin"}); // Kargo şirketi listesine yönlendir
        }

    }
}
