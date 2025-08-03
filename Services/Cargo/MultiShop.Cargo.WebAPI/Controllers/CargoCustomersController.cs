using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Cargo.BusinessLayer.Abstract;
using MultiShop.Cargo.DTOLayer.DTOs.CargoCustomerDTOs;
using MultiShop.Cargo.EntityLayer.Concrete;

namespace MultiShop.Cargo.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CargoCustomersController : ControllerBase
    {
        private readonly ICargoCustomerService _cargoCustomerService;

        public CargoCustomersController(ICargoCustomerService cargoCustomerService)
        {
            _cargoCustomerService = cargoCustomerService;
        }
        [HttpGet]
        public IActionResult CargoCustomerList()
        {
            var values = _cargoCustomerService.TGetAll(); // Tüm kargo müşterilerini getirir
            return Ok(values); // HTTP 200 OK ile sonuçları döner
        }
        [HttpGet("{id}")]
        public IActionResult GetCargoCustomerByID(int id)
        {
            var value = _cargoCustomerService.TGetByID(id); // Verilen ID'ye sahip kargo müşterisini getirir
            return Ok(value); // HTTP 200 OK ile sonucu döner
        }

        [HttpPost]
        public IActionResult CreateCargoCustomer(CreateCargoCustomerDTO createCargoCustomerDTO)
        {
            CargoCustomer cargoCustomer = new CargoCustomer // Yeni kargo müşteri nesnesi oluşturur
            {
                CargoCustomerName = createCargoCustomerDTO.CargoCustomerName, // DTO'dan alınan kargo müşteri adını kullanır
                CargoCustomerPhone = createCargoCustomerDTO.CargoCustomerPhone, // DTO'dan alınan kargo müşteri telefonunu kullanır
                CargoCustomerSurname = createCargoCustomerDTO.CargoCustomerSurname, // DTO'dan alınan kargo müşteri soyadını kullanır
                CargoCustomerEmail = createCargoCustomerDTO.CargoCustomerEmail, // DTO'dan alınan kargo müşteri e-posta adresini kullanır
                CargoCustomerCity = createCargoCustomerDTO.CargoCustomerCity, // DTO'dan alınan kargo müşteri şehrini kullanır
                CargoCustomerDistrict = createCargoCustomerDTO.CargoCustomerDistrict, // DTO'dan alınan kargo müşteri ilçesini kullanır
                CargoCustomerAddress = createCargoCustomerDTO.CargoCustomerAddress // DTO'dan alınan kargo müşteri adresini kullanır
            };
            _cargoCustomerService.TInsert(cargoCustomer); // Yeni kargo müşterisi ekler
            return Ok("Kargo Müşterisi Başarıyla Eklendi.."); // Yeni kargo müşteri oluşturma işlemi için HTTP 200 OK döner
        }
        [HttpDelete]
        public IActionResult RemoveCargoCustomer(int id)
        {
            _cargoCustomerService.TDelete(id); // Verilen ID'ye sahip kargo müşterisini siler
            return Ok("Kargo Müşterisi Başarıyla Silindi.."); // HTTP 200 OK ile silme işlemi başarılı mesajı döner
        }

        [HttpPut]
        public IActionResult UpdateCargoCustomer(UpdateCargoCustomerDTO updateCargoCustomerDTO)
        {
            CargoCustomer cargoCustomer = new CargoCustomer // Güncelleme için yeni kargo müşteri nesnesi oluşturur
            {
                CargoCustomerID = updateCargoCustomerDTO.CargoCustomerID, // DTO'dan alınan kargo müşteri ID'sini kullanır
                CargoCustomerName = updateCargoCustomerDTO.CargoCustomerName, // DTO'dan alınan kargo müşteri adını kullanır
                CargoCustomerPhone = updateCargoCustomerDTO.CargoCustomerPhone, // DTO'dan alınan kargo müşteri telefonunu kullanır
                CargoCustomerSurname = updateCargoCustomerDTO.CargoCustomerSurname, // DTO'dan alınan kargo müşteri soyadını kullanır
                CargoCustomerEmail = updateCargoCustomerDTO.CargoCustomerEmail, // DTO'dan alınan kargo müşteri e-posta adresini kullanır
                CargoCustomerCity = updateCargoCustomerDTO.CargoCustomerCity, // DTO'dan alınan kargo müşteri şehrini kullanır
                CargoCustomerDistrict = updateCargoCustomerDTO.CargoCustomerDistrict, // DTO'dan alınan kargo müşteri ilçesini kullanır
                CargoCustomerAddress = updateCargoCustomerDTO.CargoCustomerAddress // DTO'dan alınan kargo müşteri adresini kullanır
            };
            _cargoCustomerService.TUpdate(cargoCustomer); // Kargo müşterisini günceller
            return Ok("Kargo Müşterisi Başarıyla Güncellendi.."); // Kargo müşteri güncelleme işlemi için HTTP 200 OK döner
        }
    }
}
