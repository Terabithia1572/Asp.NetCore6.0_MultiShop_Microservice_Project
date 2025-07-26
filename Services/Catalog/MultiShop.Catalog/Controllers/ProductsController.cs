using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.DTOs.ProductDTOs;
using MultiShop.Catalog.Services.ProductServices;

namespace MultiShop.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _ProductService; // Product Service için Dependency Injection 

        public ProductsController(IProductService ProductService)
        {
            _ProductService = ProductService; // Constructor üzerinden Product Service'i alıyoruz
        }
        [HttpGet]
        public async Task<IActionResult> ProductList()
        {
            var values = await _ProductService.GetAllProductAsync(); // Product Service üzerinden tüm Ürünleri alıyoruz
            if (values == null || !values.Any())
            {
                return NotFound("Ürün Bulunamadı."); // Eğer Ürün bulunamazsa 404 döndürüyoruz
            }
            return Ok(values); // Ürünler bulunduysa 200 OK ile birlikte Ürünleri döndürüyoruz
        }
        [HttpGet("{id}")] // Belirli bir Ürün için id parametresi alıyoruz 
        public async Task<IActionResult> GetProductByID(string id)
        {
            var value = await _ProductService.GetByIDProductAsync(id); // Product Service üzerinden id ile Ürün alıyoruz
            if (value == null)
            {
                return NotFound("Ürün Bulunamadı."); // Eğer Ürün bulunamazsa 404 döndürüyoruz
            }
            return Ok(value); // Ürün bulunduysa 200 OK ile birlikte Ürünyi döndürüyoruz
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductDTO createProductDTO)
        {
            await _ProductService.CreateProductAsync(createProductDTO); // Product Service üzerinden yeni Ürün oluşturuyoruz
            return Ok("Ürün Başarıyla Oluşturuldu."); // Ürün başarıyla oluşturulduysa 200 OK ile birlikte mesaj döndürüyoruz
        }
        [HttpDelete] // Ürün silme işlemi için
        public async Task<IActionResult> DeleteProduct(string id)
        {
            await _ProductService.DeleteProductAsync(id); // Product Service üzerinden id ile Ürün siliyoruz
            return Ok("Ürün Başarıyla Silindi."); // Ürün başarıyla silindiyse 200 OK ile birlikte mesaj döndürüyoruz
        }
        [HttpPut] // Ürün güncelleme işlemi için
        public async Task<IActionResult> UpdateProduct(UpdateProductDTO updateProductDTO)
        {
            await _ProductService.UpdateProductAsync(updateProductDTO); // Product Service üzerinden güncelleme işlemi yapıyoruz
            return Ok("Ürün Başarıyla Güncellendi."); // Ürün başarıyla güncellendiyse 200 OK ile birlikte mesaj döndürüyoruz
        }
    }
}
