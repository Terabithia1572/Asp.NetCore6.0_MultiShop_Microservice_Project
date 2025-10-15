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
        private readonly IMongoCollection<Product> _productCollection; // 🆕 Ürünler

        public ProductDiscountsController(IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);

            _productDiscountCollection = database.GetCollection<ProductDiscount>(
                databaseSettings.ProductDiscountCollectionName ?? "ProductDiscounts");

            _productCollection = database.GetCollection<Product>(
                databaseSettings.ProductCollectionName ?? "Products"); // 🆕
        }

        // =========================================
        //  ÜRÜN LİSTESİ (Dropdown / Typeahead için)
        //  GET: /api/ProductDiscounts/productlist?q=kulak&take=20
        // =========================================
        [HttpGet("productlist")]
        public async Task<IActionResult> GetAllProductsForDiscount([FromQuery] string? q = null, [FromQuery] int take = 20)
        {
            if (take <= 0 || take > 100) take = 20;

            var filter = string.IsNullOrWhiteSpace(q)
                ? Builders<Product>.Filter.Empty
                : Builders<Product>.Filter.Regex(p => p.ProductName, new MongoDB.Bson.BsonRegularExpression(q, "i"));

            // Sadece dropdown için gerekli alanları döndür (hafif payload)
            var projection = Builders<Product>.Projection.Expression(p => new
            {
                p.ProductID,
                p.ProductName
            });

            var list = await _productCollection
                .Find(filter)
                .Project(projection)
                .Limit(take)
                .ToListAsync();

            return Ok(list);
        }

        // (Opsiyonel) Belirli bir ürün için aktif indirim var mı?
        // GET: /api/ProductDiscounts/byproduct/{productId}
        [HttpGet("byproduct/{productId}")]
        public async Task<IActionResult> GetActiveDiscountByProduct(string productId)
        {
            var now = DateTime.UtcNow;
            var filter = Builders<ProductDiscount>.Filter.And(
                Builders<ProductDiscount>.Filter.Eq(x => x.ProductID, productId),
                Builders<ProductDiscount>.Filter.Eq(x => x.IsActive, true),
                Builders<ProductDiscount>.Filter.Lte(x => x.StartDate, now),
                Builders<ProductDiscount>.Filter.Gte(x => x.EndDate, now)
            );

            var discount = await _productDiscountCollection.Find(filter)
                .SortByDescending(x => x.DiscountRate)
                .FirstOrDefaultAsync();

            if (discount == null) return NotFound();
            return Ok(discount);
        }

        // ======================
        //  CRUD: ProductDiscount
        // ======================
        [HttpGet]
        public async Task<IActionResult> GetAllDiscounts()
        {
            var discounts = await _productDiscountCollection.Find(x => true).ToListAsync();
            return Ok(discounts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var discount = await _productDiscountCollection
                .Find(x => x.ProductDiscountID == id)
                .FirstOrDefaultAsync();

            if (discount == null) return NotFound();
            return Ok(discount);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDiscount(CreateProductDiscountDTO dto)
        {
            // 🔒 Aynı ürün için aktif ve tarihleri çakışan indirim var mı?
            var hasOverlap = await _productDiscountCollection.Find(x =>
                x.ProductID == dto.ProductID &&
                x.IsActive &&
                x.StartDate <= dto.EndDate &&
                x.EndDate >= dto.StartDate
            ).AnyAsync();

            if (hasOverlap)
                return Conflict("Bu ürün için tarihleri çakışan aktif bir indirim zaten var.");

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
        [HttpGet("withnames")]
        public async Task<IActionResult> GetAllDiscountsWithNames()
        {
            var discounts = await _productDiscountCollection.Find(_ => true).ToListAsync();
            var products = await _productCollection.Find(_ => true).Project(p => new { p.ProductID, p.ProductName,p.ProductImageURL }).ToListAsync();

            var result = (from d in discounts
                          join p in products on d.ProductID equals p.ProductID into gj
                          from sub in gj.DefaultIfEmpty()
                          select new ResultProductDiscountDTO
                          {
                              ProductDiscountID = d.ProductDiscountID,
                              ProductID = d.ProductID,
                              ProductName = sub?.ProductName ?? "Ürün bulunamadı", // 🧩 Burada doldurduk
                              ProductImageURL = sub?.ProductImageURL ?? "Fotoğraf bulunamadı", // 🧩 Burada doldurduk
                              DiscountRate = d.DiscountRate,
                              StartDate = d.StartDate,
                              EndDate = d.EndDate,
                              IsActive = d.IsActive
                          }).ToList();

            return Ok(result);
        }

    }
}
