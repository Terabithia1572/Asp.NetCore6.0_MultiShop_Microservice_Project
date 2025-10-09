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

        [HttpGet]
        public async Task<IActionResult> GetAllDiscounts()
        {
            var discounts = await _productDiscountCollection.Find(x => true).ToListAsync();
            return Ok(discounts);
        }
    }
}
