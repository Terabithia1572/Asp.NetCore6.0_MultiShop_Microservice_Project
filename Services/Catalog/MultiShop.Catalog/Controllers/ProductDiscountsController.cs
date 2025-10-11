using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Settings;
using MultiShop.Catalog.DTOs.ProductDiscountDTOs;

namespace MultiShop.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductDiscountsController : ControllerBase
    {
        private readonly IMongoCollection<ProductDiscount> _productDiscountCollection;

        public ProductDiscountsController(IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _productDiscountCollection = database.GetCollection<ProductDiscount>("ProductDiscounts");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDiscounts()
        {
            var discounts = await _productDiscountCollection.Find(x => true).ToListAsync();
            return Ok(discounts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var discount = await _productDiscountCollection.Find(x => x.ProductDiscountID == id).FirstOrDefaultAsync();
            if (discount == null) return NotFound();
            return Ok(discount);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDiscount(CreateProductDiscountDTO dto)
        {
            var entity = new ProductDiscount
            {
                ProductID = dto.ProductID,
                DiscountRate = dto.DiscountRate,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                IsActive = dto.IsActive
            };

            await _productDiscountCollection.InsertOneAsync(entity);
            return Ok("İndirim başarıyla eklendi.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDiscount(UpdateProductDiscountDTO dto)
        {
            var entity = new ProductDiscount
            {
                ProductDiscountID = dto.ProductDiscountID,
                ProductID = dto.ProductID,
                DiscountRate = dto.DiscountRate,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                IsActive = dto.IsActive
            };

            var result = await _productDiscountCollection.ReplaceOneAsync(
                x => x.ProductDiscountID == dto.ProductDiscountID, entity);

            if (result.MatchedCount == 0)
                return NotFound("İndirim bulunamadı.");

            return Ok("İndirim başarıyla güncellendi.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDiscount(string id)
        {
            var result = await _productDiscountCollection.DeleteOneAsync(x => x.ProductDiscountID == id);
            if (result.DeletedCount == 0)
                return NotFound("İndirim bulunamadı.");
            return Ok("İndirim başarıyla silindi.");
        }
    }
}
