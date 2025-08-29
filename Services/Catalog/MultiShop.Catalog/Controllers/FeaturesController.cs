using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.DTOs.FeatureDTOs;
using MultiShop.Catalog.Services.FeatureServices;

namespace MultiShop.Catalog.Controllers
{
    [Authorize] // Bu controller'a anonim erişime izin veriyoruz, yani yetkilendirme gerekmiyor.
                //  [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class FeaturesController : ControllerBase
    {
        private readonly IFeatureService _featureService; // Feature Service için Dependency Injection 

        public FeaturesController(IFeatureService featureService)
        {
            _featureService = featureService; // Constructor üzerinden Feature Service'i alıyoruz
        }
        [HttpGet]
        public async Task<IActionResult> FeatureList()
        {
            var values = await _featureService.GetAllFeatureAsync(); // Feature Service üzerinden tüm Önce Çıkan Alanleri alıyoruz
            if (values == null || !values.Any())
            {
                return NotFound("Önce Çıkan Alan Bulunamadı."); // Eğer Önce Çıkan Alan bulunamazsa 404 döndürüyoruz
            }
            return Ok(values); // Önce Çıkan Alanler bulunduysa 200 OK ile birlikte Önce Çıkan Alanleri döndürüyoruz
        }
        [HttpGet("{id}")] // Belirli bir Önce Çıkan Alan için id parametresi alıyoruz 
        public async Task<IActionResult> GetFeatureByID(string id)
        {
            var value = await _featureService.GetByIDFeatureAsync(id); // Feature Service üzerinden id ile Önce Çıkan Alan alıyoruz
            if (value == null)
            {
                return NotFound("Önce Çıkan Alan Bulunamadı."); // Eğer Önce Çıkan Alan bulunamazsa 404 döndürüyoruz
            }
            return Ok(value); // Önce Çıkan Alan bulunduysa 200 OK ile birlikte Önce Çıkan Alanyi döndürüyoruz
        }
        [HttpPost]
        public async Task<IActionResult> CreateFeature(CreateFeatureDTO createFeatureDTO)
        {
            await _featureService.CreateFeatureAsync(createFeatureDTO); // Feature Service üzerinden yeni Önce Çıkan Alan oluşturuyoruz
            return Ok("Önce Çıkan Alan Başarıyla Oluşturuldu."); // Önce Çıkan Alan başarıyla oluşturulduysa 200 OK ile birlikte mesaj döndürüyoruz
        }
        [HttpDelete] // Önce Çıkan Alan silme işlemi için
        public async Task<IActionResult> DeleteFeature(string id)
        {
            await _featureService.DeleteFeatureAsync(id); // Feature Service üzerinden id ile Önce Çıkan Alan siliyoruz
            return Ok("Önce Çıkan Alan Başarıyla Silindi."); // Önce Çıkan Alan başarıyla silindiyse 200 OK ile birlikte mesaj döndürüyoruz
        }
        [HttpPut] // Önce Çıkan Alan güncelleme işlemi için
        public async Task<IActionResult> UpdateFeature(UpdateFeatureDTO updateFeatureDTO)
        {
            await _featureService.UpdateFeatureAsync(updateFeatureDTO); // Feature Service üzerinden güncelleme işlemi yapıyoruz
            return Ok("Önce Çıkan Alan Başarıyla Güncellendi."); // Önce Çıkan Alan başarıyla güncellendiyse 200 OK ile birlikte mesaj döndürüyoruz
        }
    }
}
