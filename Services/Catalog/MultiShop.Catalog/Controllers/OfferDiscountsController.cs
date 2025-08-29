using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.DTOs.OfferDiscountDTOs;
using MultiShop.Catalog.Services.OfferDiscountServices;

namespace MultiShop.Catalog.Controllers
{
    [Authorize] // Bu controller'a anonim erişime izin veriyoruz, yani yetkilendirme gerekmiyor.
                //  [AllowAnonymous] // Bu controller'a erişim için yetkilendirme gereklidir.
    [Route("api/[controller]")]
    [ApiController]
    public class OfferDiscountsController : ControllerBase
    {
        private readonly IOfferDiscountService _categoryService; // OfferDiscount Service için Dependency Injection 

        public OfferDiscountsController(IOfferDiscountService categoryService)
        {
            _categoryService = categoryService; // Constructor üzerinden OfferDiscount Service'i alıyoruz
        }
        [HttpGet]
        public async Task<IActionResult> OfferDiscountList()
        {
            var values = await _categoryService.GetAllOfferDiscountAsync(); // OfferDiscount Service üzerinden tüm İndirim Teklifileri alıyoruz
            if (values == null || !values.Any())
            {
                return NotFound("İndirim Teklifi Bulunamadı."); // Eğer İndirim Teklifi bulunamazsa 404 döndürüyoruz
            }
            return Ok(values); // İndirim Teklifiler bulunduysa 200 OK ile birlikte İndirim Teklifileri döndürüyoruz
        }
        [HttpGet("{id}")] // Belirli bir İndirim Teklifi için id parametresi alıyoruz 
        public async Task<IActionResult> GetOfferDiscountByID(string id)
        {
            var value = await _categoryService.GetByIDOfferDiscountAsync(id); // OfferDiscount Service üzerinden id ile İndirim Teklifi alıyoruz
            if (value == null)
            {
                return NotFound("İndirim Teklifi Bulunamadı."); // Eğer İndirim Teklifi bulunamazsa 404 döndürüyoruz
            }
            return Ok(value); // İndirim Teklifi bulunduysa 200 OK ile birlikte İndirim Teklifiyi döndürüyoruz
        }
        [HttpPost]
        public async Task<IActionResult> CreateOfferDiscount(CreateOfferDiscountDTO createOfferDiscountDTO)
        {
            await _categoryService.CreateOfferDiscountAsync(createOfferDiscountDTO); // OfferDiscount Service üzerinden yeni İndirim Teklifi oluşturuyoruz
            return Ok("İndirim Teklifi Başarıyla Oluşturuldu."); // İndirim Teklifi başarıyla oluşturulduysa 200 OK ile birlikte mesaj döndürüyoruz
        }
        [HttpDelete] // İndirim Teklifi silme işlemi için
        public async Task<IActionResult> DeleteOfferDiscount(string id)
        {
            await _categoryService.DeleteOfferDiscountAsync(id); // OfferDiscount Service üzerinden id ile İndirim Teklifi siliyoruz
            return Ok("İndirim Teklifi Başarıyla Silindi."); // İndirim Teklifi başarıyla silindiyse 200 OK ile birlikte mesaj döndürüyoruz
        }
        [HttpPut] // İndirim Teklifi güncelleme işlemi için
        public async Task<IActionResult> UpdateOfferDiscount(UpdateOfferDiscountDTO updateOfferDiscountDTO)
        {
            await _categoryService.UpdateOfferDiscountAsync(updateOfferDiscountDTO); // OfferDiscount Service üzerinden güncelleme işlemi yapıyoruz
            return Ok("İndirim Teklifi Başarıyla Güncellendi."); // İndirim Teklifi başarıyla güncellendiyse 200 OK ile birlikte mesaj döndürüyoruz
        }
    }
}
