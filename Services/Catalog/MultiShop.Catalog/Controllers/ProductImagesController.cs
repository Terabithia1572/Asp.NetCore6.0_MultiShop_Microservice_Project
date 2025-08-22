using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.DTOs.ProductImageDTOs;
using MultiShop.Catalog.Services.ProductImageServices;

namespace MultiShop.Catalog.Controllers
{
    // ProductImagesController, ürün resimlerini yöneten bir API denetleyicisidir.
    [Route("api/[controller]")]
    [ApiController]
    public class ProductImagesController : ControllerBase
    {
        private readonly IProductImageService _productImageService; // ProductImage Service için Dependency Injection 

        public ProductImagesController(IProductImageService ProductImageService)
        {
            _productImageService = ProductImageService; // Constructor üzerinden ProductImage Service'i alıyoruz
        }
        [HttpGet]
        public async Task<IActionResult> ProductImageList()
        {
            var values = await _productImageService.GetAllProductImageAsync(); // ProductImage Service üzerinden tüm Ürün Resmileri alıyoruz
            if (values == null || !values.Any())
            {
                return NotFound("Ürün Resmi Bulunamadı."); // Eğer Ürün Resmi bulunamazsa 404 döndürüyoruz
            }
            return Ok(values); // Ürün Resmiler bulunduysa 200 OK ile birlikte Ürün Resmileri döndürüyoruz
        }
        [HttpGet("{id}")] // Belirli bir Ürün Resmi için id parametresi alıyoruz 
        public async Task<IActionResult> GetProductImageByID(string id)
        {
            var value = await _productImageService.GetByIDProductImageAsync(id); // ProductImage Service üzerinden id ile Ürün Resmi alıyoruz
            if (value == null)
            {
                return NotFound("Ürün Resmi Bulunamadı."); // Eğer Ürün Resmi bulunamazsa 404 döndürüyoruz
            }
            return Ok(value); // Ürün Resmi bulunduysa 200 OK ile birlikte Ürün Resmiyi döndürüyoruz
        }
        [HttpPost]
        public async Task<IActionResult> CreateProductImage(CreateProductImageDTO createProductImageDTO)
        {
            await _productImageService.CreateProductImageAsync(createProductImageDTO); // ProductImage Service üzerinden yeni Ürün Resmi oluşturuyoruz
            return Ok("Ürün Resmi Başarıyla Oluşturuldu."); // Ürün Resmi başarıyla oluşturulduysa 200 OK ile birlikte mesaj döndürüyoruz
        }
        [HttpDelete] // Ürün Resmi silme işlemi için
        public async Task<IActionResult> DeleteProductImage(string id)
        {
            await _productImageService.DeleteProductImageAsync(id); // ProductImage Service üzerinden id ile Ürün Resmi siliyoruz
            return Ok("Ürün Resmi Başarıyla Silindi."); // Ürün Resmi başarıyla silindiyse 200 OK ile birlikte mesaj döndürüyoruz
        }
        [HttpPut] // Ürün Resmi güncelleme işlemi için
        public async Task<IActionResult> UpdateProductImage(UpdateProductImageDTO updateProductImageDTO)
        {
            await _productImageService.UpdateProductImageAsync(updateProductImageDTO); // ProductImage Service üzerinden güncelleme işlemi yapıyoruz
            return Ok("Ürün Resmi Başarıyla Güncellendi."); // Ürün Resmi başarıyla güncellendiyse 200 OK ile birlikte mesaj döndürüyoruz
        }
        [HttpGet("ProductImagesByProductID")]
        public async Task<IActionResult> ProductImagesByProductID(string id)
        {
            var values = await _productImageService.GetByProductIDProductImageAsync(id); // ProductImage Service üzerinden productId ile Ürün Resmileri alıyoruz
           
            return Ok(values); // Ürün Resmiler bulunduysa 200 OK ile birlikte Ürün Resmileri döndürüyoruz
        }
    }
}
