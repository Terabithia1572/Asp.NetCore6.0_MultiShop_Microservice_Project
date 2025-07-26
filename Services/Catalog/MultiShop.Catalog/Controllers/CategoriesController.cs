using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.DTOs.CategoryDTOs;
using MultiShop.Catalog.Services.CategoryServices;

namespace MultiShop.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService; // Category Service için Dependency Injection 

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService; // Constructor üzerinden Category Service'i alıyoruz
        }
        [HttpGet]
        public async Task<IActionResult> CategoryList()
        {
            var values = await _categoryService.GetAllCategoryAsync(); // Category Service üzerinden tüm kategorileri alıyoruz
            if (values == null || !values.Any())
            {
                return NotFound("Kategori Bulunamadı."); // Eğer kategori bulunamazsa 404 döndürüyoruz
            }
            return Ok(values); // Kategoriler bulunduysa 200 OK ile birlikte kategorileri döndürüyoruz
        }
        [HttpGet("{id}")] // Belirli bir kategori için id parametresi alıyoruz 
        public async Task<IActionResult> GetCategoryByID(string id)
        {
            var value = await _categoryService.GetByIDCategoryAsync(id); // Category Service üzerinden id ile kategori alıyoruz
            if (value == null)
            {
                return NotFound("Kategori Bulunamadı."); // Eğer kategori bulunamazsa 404 döndürüyoruz
            }
            return Ok(value); // Kategori bulunduysa 200 OK ile birlikte kategoriyi döndürüyoruz
        }
        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryDTO createCategoryDTO)
        {
            await _categoryService.CreateCategoryAsync(createCategoryDTO); // Category Service üzerinden yeni kategori oluşturuyoruz
            return Ok("Kategori Başarıyla Oluşturuldu."); // Kategori başarıyla oluşturulduysa 200 OK ile birlikte mesaj döndürüyoruz
        }
        [HttpDelete] // Kategori silme işlemi için
        public async Task<IActionResult> DeleteCategory(string id)
        {
            await _categoryService.DeleteCategoryAsync(id); // Category Service üzerinden id ile kategori siliyoruz
            return Ok("Kategori Başarıyla Silindi."); // Kategori başarıyla silindiyse 200 OK ile birlikte mesaj döndürüyoruz
        }
        [HttpPut] // Kategori güncelleme işlemi için
        public async Task<IActionResult> UpdateCategory(UpdateCategoryDTO updateCategoryDTO)
        {
            await _categoryService.UpdateCategoryAsync(updateCategoryDTO); // Category Service üzerinden güncelleme işlemi yapıyoruz
            return Ok("Kategori Başarıyla Güncellendi."); // Kategori başarıyla güncellendiyse 200 OK ile birlikte mesaj döndürüyoruz
        }
    }
}
