using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Settings;

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

        // 🔹 Tüm indirimleri getir
        [HttpGet]
        public async Task<IActionResult> GetAllDiscounts()
        {
            var discounts = await _productDiscountCollection.Find(x => true).ToListAsync();
            return Ok(discounts);
        }

        // 🔹 Belirli bir indirimi getir
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var discount = await _productDiscountCollection.Find(x => x.ProductDiscountID == id).FirstOrDefaultAsync();
            if (discount == null) return NotFound();
            return Ok(discount);
        }

        // 🔹 Yeni indirim ekle
        [HttpPost]
        public async Task<IActionResult> CreateDiscount(ProductDiscount discount)
        {
            await _productDiscountCollection.InsertOneAsync(discount);
            return Ok("İndirim başarıyla eklendi.");
        }

        // 🔹 İndirim güncelle
        [HttpPut]
        public async Task<IActionResult> UpdateDiscount(ProductDiscount discount)
        {
            var result = await _productDiscountCollection.ReplaceOneAsync(
                x => x.ProductDiscountID == discount.ProductDiscountID, discount);

            if (result.MatchedCount == 0)
                return NotFound("İndirim bulunamadı.");

            return Ok("İndirim başarıyla güncellendi.");
        }

        // 🔹 İndirim sil
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
