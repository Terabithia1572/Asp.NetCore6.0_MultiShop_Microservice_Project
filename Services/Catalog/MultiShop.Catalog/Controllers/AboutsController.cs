using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.DTOs.AboutDTOs;
using MultiShop.Catalog.Services.AboutServices;

namespace MultiShop.Catalog.Controllers
{
   // [AllowAnonymous] // Bu controller'a erişim için yetkilendirme gereklidir.
   [Authorize] // Bu controller'a anonim erişime izin veriyoruz, yani yetkilendirme gerekmiyor.
    [Route("api/[controller]")]
    [ApiController]

    public class AboutsController : ControllerBase
    {
        private readonly IAboutService _aboutService; // About Service için Dependency Injection 

        public AboutsController(IAboutService aboutService)
        {
            _aboutService = aboutService; // Constructor üzerinden About Service'i alıyoruz
        }
        [HttpGet]
        public async Task<IActionResult> AboutList()
        {
            var values = await _aboutService.GetAllAboutAsync(); // About Service üzerinden tüm hakkımdaleri alıyoruz
            if (values == null || !values.Any())
            {
                return NotFound("Hakkımda Bulunamadı."); // Eğer hakkımda bulunamazsa 404 döndürüyoruz
            }
            return Ok(values); // Hakkımdaler bulunduysa 200 OK ile birlikte hakkımdaleri döndürüyoruz
        }
        [HttpGet("{id}")] // Belirli bir hakkımda için id parametresi alıyoruz 
        public async Task<IActionResult> GetAboutByID(string id)
        {
            var value = await _aboutService.GetByIDAboutAsync(id); // About Service üzerinden id ile hakkımda alıyoruz
            if (value == null)
            {
                return NotFound("Hakkımda Bulunamadı."); // Eğer hakkımda bulunamazsa 404 döndürüyoruz
            }
            return Ok(value); // Hakkımda bulunduysa 200 OK ile birlikte hakkımdayi döndürüyoruz
        }
        [HttpPost]
        public async Task<IActionResult> CreateAbout(CreateAboutDTO createAboutDTO)
        {
            await _aboutService.CreateAboutAsync(createAboutDTO); // About Service üzerinden yeni hakkımda oluşturuyoruz
            return Ok("Hakkımda Başarıyla Oluşturuldu."); // Hakkımda başarıyla oluşturulduysa 200 OK ile birlikte mesaj döndürüyoruz
        }
        [HttpDelete] // Hakkımda silme işlemi için
        public async Task<IActionResult> DeleteAbout(string id)
        {
            await _aboutService.DeleteAboutAsync(id); // About Service üzerinden id ile hakkımda siliyoruz
            return Ok("Hakkımda Başarıyla Silindi."); // Hakkımda başarıyla silindiyse 200 OK ile birlikte mesaj döndürüyoruz
        }
        [HttpPut] // Hakkımda güncelleme işlemi için
        public async Task<IActionResult> UpdateAbout(UpdateAboutDTO updateAboutDTO)
        {
            await _aboutService.UpdateAboutAsync(updateAboutDTO); // About Service üzerinden güncelleme işlemi yapıyoruz
            return Ok("Hakkımda Başarıyla Güncellendi."); // Hakkımda başarıyla güncellendiyse 200 OK ile birlikte mesaj döndürüyoruz
        }
    }
}
