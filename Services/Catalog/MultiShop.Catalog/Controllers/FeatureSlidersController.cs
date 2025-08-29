using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.DTOs.FeatureSliderDTOs;
using MultiShop.Catalog.Services.FeatureSliderServices;

namespace MultiShop.Catalog.Controllers
{
    [Authorize] // Bu controller'a anonim erişime izin veriyoruz, yani yetkilendirme gerekmiyor.
    // [AllowAnonymous] // Bu controller'a erişim için yetkilendirme gereklidir.
    [Route("api/[controller]")]
    [ApiController]
    public class FeatureSlidersController : ControllerBase
    {
        private readonly IFeatureSliderService _featureSliderService; // FeatureSlider Service için Dependency Injection 

        public FeatureSlidersController(IFeatureSliderService featureSliderService)
        {
            _featureSliderService = featureSliderService; // Constructor üzerinden FeatureSlider Service'i alıyoruz
        }
        [HttpGet]
        public async Task<IActionResult> FeatureSliderList()
        {
            var values = await _featureSliderService.GetAllFeatureSliderAsync(); // FeatureSlider Service üzerinden tüm kategorileri alıyoruz
            if (values == null || !values.Any())
            {
                return NotFound("Önce Çıkan Görsel Bulunamadı."); // Eğer kategori bulunamazsa 404 döndürüyoruz
            }
            return Ok(values); // Önce Çıkan Görseller bulunduysa 200 OK ile birlikte kategorileri döndürüyoruz
        }
        [HttpGet("{id}")] // Belirli bir kategori için id parametresi alıyoruz 
        public async Task<IActionResult> GetFeatureSliderByID(string id)
        {
            var value = await _featureSliderService.GetByIDFeatureSliderAsync(id); // FeatureSlider Service üzerinden id ile kategori alıyoruz
            if (value == null)
            {
                return NotFound("Önce Çıkan Görsel Bulunamadı."); // Eğer kategori bulunamazsa 404 döndürüyoruz
            }
            return Ok(value); // Önce Çıkan Görsel bulunduysa 200 OK ile birlikte kategoriyi döndürüyoruz
        }
        [HttpPost]
        public async Task<IActionResult> CreateFeatureSlider(CreateFeatureSliderDTO createFeatureSliderDTO)
        {
            await _featureSliderService.CreateFeatureSliderAsync(createFeatureSliderDTO); // FeatureSlider Service üzerinden yeni kategori oluşturuyoruz
            return Ok("Önce Çıkan Görsel Başarıyla Oluşturuldu."); // Önce Çıkan Görsel başarıyla oluşturulduysa 200 OK ile birlikte mesaj döndürüyoruz
        }
        [HttpDelete] // Önce Çıkan Görsel silme işlemi için
        public async Task<IActionResult> DeleteFeatureSlider(string id)
        {
            await _featureSliderService.DeleteFeatureSliderAsync(id); // FeatureSlider Service üzerinden id ile kategori siliyoruz
            return Ok("Önce Çıkan Görsel Başarıyla Silindi."); // Önce Çıkan Görsel başarıyla silindiyse 200 OK ile birlikte mesaj döndürüyoruz
        }
        [HttpPut] // Önce Çıkan Görsel güncelleme işlemi için
        public async Task<IActionResult> UpdateFeatureSlider(UpdateFeatureSliderDTO updateFeatureSliderDTO)
        {
            await _featureSliderService.UpdateFeatureSliderAsync(updateFeatureSliderDTO); // FeatureSlider Service üzerinden güncelleme işlemi yapıyoruz
            return Ok("Önce Çıkan Görsel Başarıyla Güncellendi."); // Önce Çıkan Görsel başarıyla güncellendiyse 200 OK ile birlikte mesaj döndürüyoruz
        }
    }
}
