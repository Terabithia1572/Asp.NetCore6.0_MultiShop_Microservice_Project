using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.DTOs.ProductDetailDTOs;
using MultiShop.Catalog.Services.ProductDetailServices;


namespace MultiShop.Catalog.Controllers
{
    //ProductDetailController, ürün detaylarını yöneten bir API denetleyicisidir.
    [Route("api/[controller]")]
    [ApiController]
    public class ProductDetailsController : ControllerBase
    {
        private readonly IProductDetailService _productDetailService; // ProductDetail Service için Dependency Injection 

        public ProductDetailsController(IProductDetailService ProductDetailService)
        {
            _productDetailService = ProductDetailService; // Constructor üzerinden ProductDetail Service'i alıyoruz
        }
        [HttpGet]
        public async Task<IActionResult> ProductDetailList()
        {
            var values = await _productDetailService.GetAllProductDetailAsync(); // ProductDetail Service üzerinden tüm Ürün Detaylarıleri alıyoruz
            if (values == null || !values.Any())
            {
                return NotFound("Ürün Detayları Bulunamadı."); // Eğer Ürün Detayları bulunamazsa 404 döndürüyoruz
            }
            return Ok(values); // Ürün Detaylarıler bulunduysa 200 OK ile birlikte Ürün Detaylarıleri döndürüyoruz
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductDetailByID(string id)
        {
            var value = await _productDetailService.GetByIDProductDetailAsync(id);
            if (value == null)
                return NotFound("Ürün Detayları Bulunamadı.");
            return Ok(value);
        }

        // GET api/ProductDetails/by-product/{productId}
        [HttpGet("by-product/{productId}")]
        public async Task<IActionResult> GetProductDetailByProductID(string productId)
        {
            var value = await _productDetailService.GetByProductIDDetailAsync(productId);
            if (value == null)
                return NotFound("Ürün Detayları Bulunamadı.");
            return Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProductDetail(CreateProductDetailDTO createProductDetailDTO)
        {
            await _productDetailService.CreateProductDetailAsync(createProductDetailDTO); // ProductDetail Service üzerinden yeni Ürün Detayları oluşturuyoruz
            return Ok("Ürün Detayları Başarıyla Oluşturuldu."); // Ürün Detayları başarıyla oluşturulduysa 200 OK ile birlikte mesaj döndürüyoruz
        }
        [HttpDelete] // Ürün Detayları silme işlemi için
        public async Task<IActionResult> DeleteProductDetail(string id)
        {
            await _productDetailService.DeleteProductDetailAsync(id); // ProductDetail Service üzerinden id ile Ürün Detayları siliyoruz
            return Ok("Ürün Detayları Başarıyla Silindi."); // Ürün Detayları başarıyla silindiyse 200 OK ile birlikte mesaj döndürüyoruz
        }
        [HttpPut] // Ürün Detayları güncelleme işlemi için
        public async Task<IActionResult> UpdateProductDetail(UpdateProductDetailDTO updateProductDetailDTO)
        {
            await _productDetailService.UpdateProductDetailAsync(updateProductDetailDTO); // ProductDetail Service üzerinden güncelleme işlemi yapıyoruz
            return Ok("Ürün Detayları Başarıyla Güncellendi."); // Ürün Detayları başarıyla güncellendiyse 200 OK ile birlikte mesaj döndürüyoruz
        }
    }
}
