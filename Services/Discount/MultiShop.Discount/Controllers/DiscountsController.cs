using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Discount.DTOs.DiscountCouponDTOs;
using MultiShop.Discount.Services.DiscountService;

namespace MultiShop.Discount.Controllers
{
    [Authorize] // Bu controller'a erişim için yetkilendirme gereklidir.
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountsController : ControllerBase
    {
        private readonly IDiscountService _discountService; // DiscountService arayüzünü uygulayan servis nesnesi, indirim işlemlerini yönetir.

        public DiscountsController(IDiscountService discountService)
        {
            _discountService = discountService;
        }
        [HttpGet]
        public async Task<IActionResult> DiscountCouponList()
        {
            var result = await _discountService.GetAllDiscountCouponAsync();
            // Tüm kuponları listelemek için DiscountService üzerinden asenkron çağrı yapar.
            return Ok(result);
            // Sonucu HTTP 200 OK ile döner.
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDiscountCouponByID(int id)
        {
            var result = await _discountService.GetByIDDiscountCouponAsync(id);
            // Belirtilen ID'ye sahip kuponu almak için DiscountService üzerinden asenkron çağrı yapar.
            return Ok(result);
            // Sonucu HTTP 200 OK ile döner.
        }
        [HttpPost]
        public async Task<IActionResult> CreateDiscountCoupon(CreateDiscountCouponDTO createCouponDTO)
        {
            await _discountService.CreateDiscountCouponAsync(createCouponDTO);
            // Yeni bir kupon oluşturmak için DiscountService üzerinden asenkron çağrı yapar.
            return Ok("Kupon Başarıyla Oluşturuldu..");
            // İşlem başarılı ise HTTP 200 OK ile döner.
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteDiscountCoupon(int couponID)
        {
            await _discountService.DeleteDiscountCouponAsync(couponID);
            // Belirtilen ID'ye sahip kuponu silmek için DiscountService üzerinden asenkron çağrı yapar.
            return Ok("Kupon Başarıyla Silindi..");
            // İşlem başarılı ise HTTP 200 OK ile döner.
        }
        [HttpPut]
        public async Task<IActionResult> UpdateDiscountCoupon(UpdateDiscountCouponDTO updateCouponDTO)
        {
            await _discountService.UpdateDiscountCouponAsync(updateCouponDTO);
            // Mevcut bir kuponu güncellemek için DiscountService üzerinden asenkron çağrı yapar.
            return Ok("Kupon Başarıyla Güncellendi..");
            // İşlem başarılı ise HTTP 200 OK ile döner.
        }

    }
}
