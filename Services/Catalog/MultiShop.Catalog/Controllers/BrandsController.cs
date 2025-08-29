using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.DTOs.BrandDTOs;
using MultiShop.Catalog.Services.BrandServices;

namespace MultiShop.Catalog.Controllers
{
    [Authorize] // Bu controller'a anonim erişime izin veriyoruz, yani yetkilendirme gerekmiyor.
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly IBrandService _brandService; // Brand Service için Dependency Injection 

        public BrandsController(IBrandService brandService)
        {
            _brandService = brandService; // Constructor üzerinden Brand Service'i alıyoruz
        }
        [HttpGet]
        public async Task<IActionResult> BrandList()
        {
            var values = await _brandService.GetAllBrandAsync(); // Brand Service üzerinden tüm markaleri alıyoruz
            if (values == null || !values.Any())
            {
                return NotFound("Marka Bulunamadı."); // Eğer marka bulunamazsa 404 döndürüyoruz
            }
            return Ok(values); // Markaler bulunduysa 200 OK ile birlikte markaleri döndürüyoruz
        }
        [HttpGet("{id}")] // Belirli bir marka için id parametresi alıyoruz 
        public async Task<IActionResult> GetBrandByID(string id)
        {
            var value = await _brandService.GetByIDBrandAsync(id); // Brand Service üzerinden id ile marka alıyoruz
            if (value == null)
            {
                return NotFound("Marka Bulunamadı."); // Eğer marka bulunamazsa 404 döndürüyoruz
            }
            return Ok(value); // Marka bulunduysa 200 OK ile birlikte markayi döndürüyoruz
        }
        [HttpPost]
        public async Task<IActionResult> CreateBrand(CreateBrandDTO createBrandDTO)
        {
            await _brandService.CreateBrandAsync(createBrandDTO); // Brand Service üzerinden yeni marka oluşturuyoruz
            return Ok("Marka Başarıyla Oluşturuldu."); // Marka başarıyla oluşturulduysa 200 OK ile birlikte mesaj döndürüyoruz
        }
        [HttpDelete] // Marka silme işlemi için
        public async Task<IActionResult> DeleteBrand(string id)
        {
            await _brandService.DeleteBrandAsync(id); // Brand Service üzerinden id ile marka siliyoruz
            return Ok("Marka Başarıyla Silindi."); // Marka başarıyla silindiyse 200 OK ile birlikte mesaj döndürüyoruz
        }
        [HttpPut] // Marka güncelleme işlemi için
        public async Task<IActionResult> UpdateBrand(UpdateBrandDTO updateBrandDTO)
        {
            await _brandService.UpdateBrandAsync(updateBrandDTO); // Brand Service üzerinden güncelleme işlemi yapıyoruz
            return Ok("Marka Başarıyla Güncellendi."); // Marka başarıyla güncellendiyse 200 OK ile birlikte mesaj döndürüyoruz
        }
    }
}
