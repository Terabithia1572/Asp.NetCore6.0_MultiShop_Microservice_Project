using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Cargo.BusinessLayer.Abstract;
using MultiShop.Cargo.DTOLayer.DTOs.CargoDetailDTOs;
using MultiShop.Cargo.EntityLayer.Concrete;

namespace MultiShop.Cargo.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CargoDetailsController : ControllerBase
    {
        private readonly ICargoDetailService _cargoDetailService; // Deependency injection Kargo Detay Servisi 

        public CargoDetailsController(ICargoDetailService cargoDetailService)
        {
            _cargoDetailService = cargoDetailService;
        }
        [HttpGet]
        public IActionResult GetAllCargoDetails()
        {
            var cargoDetails = _cargoDetailService.TGetAll(); // Tüm Kargo Detaylarını Getir
            return Ok(cargoDetails); // 200 OK ile Dön
        }
        [HttpGet("{id}")]
        public IActionResult GetCargoDetailByID(int id)
        {
            var cargoDetail = _cargoDetailService.TGetByID(id); // Kargo Detayını ID ile Getir
            if (cargoDetail == null) // Eğer Kargo Detayı Bulunamazsa
            {
                return NotFound(); // 404 Not Found ile Dön
            }
            return Ok(cargoDetail); // 200 OK ile Dön
        }
        [HttpPost]
        public IActionResult CreateCargoDetail(CreateCargoDetailDTO createCargoDetailDTO)
        {
            CargoDetail cargoDetail = new CargoDetail
            {
                SenderCustomer = createCargoDetailDTO.SenderCustomer, // Gönderen Müşteri
                ReceiverCustomer = createCargoDetailDTO.ReceiverCustomer, // Alıcı Müşteri
                Barcode = createCargoDetailDTO.Barcode, // Barkod Numarası
                CargoCompanyID = createCargoDetailDTO.CargoCompanyID // Kargo Şirket ID
            };
            _cargoDetailService.TInsert(cargoDetail); // Kargo Detayını Ekle
            return Ok("Kargo Detayı Başarıyla Eklendi.."); // 200 OK ile Başarılı Mesajı Dön
        }
        [HttpDelete]
        public IActionResult RemoveCargoDetail(int id)
        {
            var cargoDetail = _cargoDetailService.TGetByID(id); // Kargo Detayını ID ile Getir
            if (cargoDetail == null) // Eğer Kargo Detayı Bulunamazsa
            {
                return NotFound(); // 404 Not Found ile Dön
            }
            _cargoDetailService.TDelete(id); // Kargo Detayını Sil
            return Ok("Kargo Detayı Başarıyla Silindi.."); // 200 OK ile Başarılı Mesajı Dön
        }
        [HttpPut]
        public IActionResult UpdateCargoDetail(UpdateCargoDetailDTO updateCargoDetailDTO)
        {
            CargoDetail cargoDetail = new CargoDetail
            {
                CargoDetailID = updateCargoDetailDTO.CargoDetailID, // Kargo Detay ID
                SenderCustomer = updateCargoDetailDTO.SenderCustomer, // Gönderen Müşteri
                ReceiverCustomer = updateCargoDetailDTO.ReceiverCustomer, // Alıcı Müşteri
                Barcode = updateCargoDetailDTO.Barcode, // Barkod Numarası
                CargoCompanyID = updateCargoDetailDTO.CargoCompanyID // Kargo Şirket ID
            };
            _cargoDetailService.TUpdate(cargoDetail); // Kargo Detayını Güncelle
            return Ok("Kargo Detayı Başarıyla Güncellendi.."); // 200 OK ile Başarılı Mesajı Dön
        }

    }
}
