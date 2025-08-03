using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Cargo.BusinessLayer.Abstract;
using MultiShop.Cargo.DTOLayer.DTOs.CargoOperationDTOs;
using MultiShop.Cargo.EntityLayer.Concrete;

namespace MultiShop.Cargo.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CargoOperationsController : ControllerBase
    {
        private readonly ICargoOperationService _cargoOperationService; // Deependency injection Kargo İşlemleri Servisi 
        public CargoOperationsController(ICargoOperationService cargoOperationService)
        {
            _cargoOperationService = cargoOperationService;
        }
        [HttpGet]
        public IActionResult GetAllCargoOperations()
        {
            var cargoOperations = _cargoOperationService.TGetAll(); // Tüm Kargo İşlemlerini Getir
            return Ok(cargoOperations); // 200 OK ile Dön
        }
        [HttpGet("{id}")]
        public IActionResult GetCargoOperationByID(int id)
        {
            var cargoOperation = _cargoOperationService.TGetByID(id); // Kargo İşlemini ID ile Getir
            if (cargoOperation == null) // Eğer Kargo İşlemi Bulunamazsa
            {
                return NotFound(); // 404 Not Found ile Dön
            }
            return Ok(cargoOperation); // 200 OK ile Dön
        }
        [HttpPost]
        public IActionResult CreateCargoOperation(CreateCargoOperationDTO createCargoOperationDTO)
        {
            CargoOperation cargoOperation = new CargoOperation
            {
                Barcode = createCargoOperationDTO.Barcode, // Barkod Numarası
                CargoOperationDescription = createCargoOperationDTO.CargoOperationDescription, // Kargo Hareket Açıklaması
                CargoOperationDate = createCargoOperationDTO.CargoOperationDate // Kargo Hareket Tarihi
            };
            _cargoOperationService.TInsert(cargoOperation); // Kargo İşlemini Ekle
            return Ok("Kargo İşlemi Başarıyla Eklendi.."); // 200 OK ile Başarılı Mesajı Dön
        }
        [HttpDelete]
        public IActionResult RemoveCargoOperation(int id)
        {
            var cargoOperation = _cargoOperationService.TGetByID(id); // Kargo İşlemini ID ile Getir
            if (cargoOperation == null) // Eğer Kargo İşlemi Bulunamazsa
            {
                return NotFound(); // 404 Not Found ile Dön
            }
            _cargoOperationService.TDelete(id); // Kargo İşlemini Sil
            return Ok("Kargo İşlemi Başarıyla Silindi.."); // 200 OK ile Başarılı Mesajı Dön
        }
        [HttpPut]
        public IActionResult UpdateCargoOperation(UpdateCargoOperationDTO updateCargoOperationDTO)
        {
            CargoOperation cargoOperation = new CargoOperation
            {
                CargoOperationID = updateCargoOperationDTO.CargoOperationID, // Kargo Hareket ID
                Barcode = updateCargoOperationDTO.Barcode, // Barkod Numarası
                CargoOperationDescription = updateCargoOperationDTO.CargoOperationDescription, // Kargo Hareket Açıklaması
                CargoOperationDate = updateCargoOperationDTO.CargoOperationDate // Kargo Hareket Tarihi
            };
            _cargoOperationService.TUpdate(cargoOperation); // Kargo İşlemini Güncelle
            return Ok("Kargo İşlemi Başarıyla Güncellendi.."); // 200 OK ile Başarılı Mesajı Dön
        }
    }
}
