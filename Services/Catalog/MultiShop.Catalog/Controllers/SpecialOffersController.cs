using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.DTOs.SpecialOfferDTOs;
using MultiShop.Catalog.Services.SpecialOfferServices;

namespace MultiShop.Catalog.Controllers
{
    [AllowAnonymous] // Bu controller'a erişim için yetkilendirme gereklidir.
    [Route("api/[controller]")] // API'nin URL yolunu tanımlar, [controller] kısmı bu controller'ın adını alır.
    [ApiController] // Bu sınıfın bir API controller olduğunu belirtir, bu sayede otomatik olarak model doğrulama ve hata işleme gibi özellikler eklenir.
    public class SpecialOffersController : ControllerBase
    {
        private readonly ISpecialOfferService _specialOfferService; // SpecialOffer Service için Dependency Injection 

        public SpecialOffersController(ISpecialOfferService specialOfferService)
        {
            _specialOfferService = specialOfferService; // Constructor üzerinden SpecialOffer Service'i alıyoruz
        }
        [HttpGet]
        public async Task<IActionResult> SpecialOfferList()
        {
            var values = await _specialOfferService.GetAllSpecialOfferAsync(); // SpecialOffer Service üzerinden tüm indirimleri alıyoruz
            if (values == null || !values.Any())
            {
                return NotFound("İndirim Bulunamadı."); // Eğer indirim bulunamazsa 404 döndürüyoruz
            }
            return Ok(values); // İndirimler bulunduysa 200 OK ile birlikte indirimleri döndürüyoruz
        }
        [HttpGet("{id}")] // Belirli bir indirim için id parametresi alıyoruz 
        public async Task<IActionResult> GetSpecialOfferByID(string id)
        {
            var value = await _specialOfferService.GetByIDSpecialOfferAsync(id); // SpecialOffer Service üzerinden id ile indirim alıyoruz
            if (value == null)
            {
                return NotFound("İndirim Bulunamadı."); // Eğer indirim bulunamazsa 404 döndürüyoruz
            }
            return Ok(value); // İndirim bulunduysa 200 OK ile birlikte indirimyi döndürüyoruz
        }
        [HttpPost]
        public async Task<IActionResult> CreateSpecialOffer(CreateSpecialOfferDTO createSpecialOfferDTO)
        {
            await _specialOfferService.CreateSpecialOfferAsync(createSpecialOfferDTO); // SpecialOffer Service üzerinden yeni indirim oluşturuyoruz
            return Ok("İndirim Başarıyla Oluşturuldu."); // İndirim başarıyla oluşturulduysa 200 OK ile birlikte mesaj döndürüyoruz
        }
        [HttpDelete] // İndirim silme işlemi için
        public async Task<IActionResult> DeleteSpecialOffer(string id)
        {
            await _specialOfferService.DeleteSpecialOfferAsync(id); // SpecialOffer Service üzerinden id ile indirim siliyoruz
            return Ok("İndirim Başarıyla Silindi."); // İndirim başarıyla silindiyse 200 OK ile birlikte mesaj döndürüyoruz
        }
        [HttpPut] // İndirim güncelleme işlemi için
        public async Task<IActionResult> UpdateSpecialOffer(UpdateSpecialOfferDTO updateSpecialOfferDTO)
        {
            await _specialOfferService.UpdateSpecialOfferAsync(updateSpecialOfferDTO); // SpecialOffer Service üzerinden güncelleme işlemi yapıyoruz
            return Ok("İndirim Başarıyla Güncellendi."); // İndirim başarıyla güncellendiyse 200 OK ile birlikte mesaj döndürüyoruz
        }
    }
}
