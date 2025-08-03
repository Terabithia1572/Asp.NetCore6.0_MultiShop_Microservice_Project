using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Cargo.BusinessLayer.Abstract;
using MultiShop.Cargo.DTOLayer.DTOs.CargoCompanyDTOs;
using MultiShop.Cargo.EntityLayer.Concrete;

namespace MultiShop.Cargo.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CargoCompaniesController : ControllerBase
    {
        private readonly ICargoCompanyService _cargoCompanyService;

        public CargoCompaniesController(ICargoCompanyService cargoCompanyService)
        {
            _cargoCompanyService = cargoCompanyService;
        }
        [HttpGet]
        public IActionResult CargoCompanyList()
        {
            var values = _cargoCompanyService.TGetAll(); // Tüm kargo şirketlerini getirir
            return Ok(values); // HTTP 200 OK ile sonuçları döner
        }
        [HttpPost]
        public IActionResult CreateCargoCompany(CreateCargoCompanyDTO createCargoCompanyDTO)
        {
            CargoCompany cargoCompany = new CargoCompany // Yeni kargo şirketi nesnesi oluşturur
            {
                CargoCompanyName = createCargoCompanyDTO.CargoCompanyName // DTO'dan alınan kargo şirketi adını kullanır
            };
            _cargoCompanyService.TInsert(cargoCompany); // Yeni kargo şirketi ekler
            return Ok("Kargo Şirketi Başarıyla Eklendi.."); // Yeni kargo şirketi oluşturma işlemi için HTTP 200 OK döner
        }
        [HttpDelete]
        public IActionResult RemoveCargoCompany(int id)
        {
            _cargoCompanyService.TDelete(id); // Verilen ID'ye sahip kargo şirketini siler
            return Ok("Kargo Şirketi Başarıyla Silindi.."); // HTTP 200 OK ile silme işlemi başarılı mesajı döner
        }
        [HttpGet("{id}")]
        public IActionResult GetCargoCompanyByID(int id)
        {
            var value = _cargoCompanyService.TGetByID(id); // Verilen ID'ye sahip kargo şirketini getirir,
            return Ok(value); // HTTP 200 OK ile sonucu döner
        }
        [HttpPut]
        public IActionResult UpdateCargoCompany(UpdateCargoCompanyDTO updateCargoCompanyDTO)
        {
            CargoCompany cargoCompany = new CargoCompany // Güncelleme için yeni kargo şirketi nesnesi oluşturur
            {
                CargoCompanyID = updateCargoCompanyDTO.CargoCompanyID, // DTO'dan alınan kargo şirketi ID'sini kullanır
                CargoCompanyName = updateCargoCompanyDTO.CargoCompanyName // DTO'dan alınan kargo şirketi adını kullanır
            };
            _cargoCompanyService.TUpdate(cargoCompany); // Kargo şirketini günceller
            return Ok("Kargo Şirketi Başarıyla Güncellendi.."); // Kargo şirketi güncelleme işlemi için HTTP 200 OK döner
        }
    }
}
