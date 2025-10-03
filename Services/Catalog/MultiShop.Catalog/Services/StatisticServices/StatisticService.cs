using MongoDB.Bson;
using MongoDB.Driver;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Settings;
using System.Threading.Tasks;

namespace MultiShop.Catalog.Services.StatisticServices
{
    public class StatisticService : IStatisticService
    {
        private readonly IMongoCollection<Product> _productCollection; // MongoDB'deki Product koleksiyonuna erişim sağlar.
        private readonly IMongoCollection<Category> _categoryCollection; // MongoDB'deki Category koleksiyonuna erişim sağlar.
        private readonly IMongoCollection<Brand> _brandCollection; // MongoDB'deki Markalar koleksiyonuna erişim sağlar.

        public StatisticService(IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString); // MongoDB ile bağlantı kurmak için istemci oluşturuluyor.
            var database = client.GetDatabase(_databaseSettings.DatabaseName); // Bağlantı kurulan MongoDB'de kullanılacak veritabanı seçiliyor.
            _productCollection = database.GetCollection<Product>(_databaseSettings.ProductCollectionName); // Product koleksiyonu üzerinden işlem yapabilmek için referans alınıyor.
            _categoryCollection = database.GetCollection<Category>(_databaseSettings.CategoryCollectionName); // Category koleksiyonu üzerinden işlem yapabilmek için referans alınıyor.
            _brandCollection=database.GetCollection<Brand>(_databaseSettings.BrandCollectionName); // Brand koleksiyonu üzerinden işlem yapabilmek için referans alınıyor.
        }

        public async Task<long> GetBrandCount()
        {
            return await _brandCollection.CountDocumentsAsync(FilterDefinition<Brand>.Empty); // 
        }

        public async Task<long> GetCategoryCount()
        {
            return await _categoryCollection.CountDocumentsAsync(FilterDefinition<Category>.Empty); // 
        }

        public async Task<string> GetMaximumPriceProductName()
        {
            var filter = Builders<Product>.Filter.Empty;
            var sort = Builders<Product>.Sort.Descending(x => x.ProductPrice); // 
            var projection = Builders<Product>.Projection.Include(y => y.ProductName)
                .Exclude("ProductID");
            var product=await _productCollection.Find(filter)
                .Sort(sort)
                .Project(projection)
                .FirstOrDefaultAsync();
            return product.GetValue("ProductName").AsString; // 
        }

        public async Task<string> GetMinimumPriceProductName()
        {
            var filter = Builders<Product>.Filter.Empty;
            var sort = Builders<Product>.Sort.Ascending(x => x.ProductPrice); // 
            var projection = Builders<Product>.Projection.Include(y => y.ProductName)
                .Exclude("ProductID");
            var product = await _productCollection.Find(filter)
                .Sort(sort)
                .Project(projection)
                .FirstOrDefaultAsync();
            return product.GetValue("ProductName").AsString; // 
        }

        public async Task<long> GetProductCount()
        {
            return await _productCollection.CountDocumentsAsync(FilterDefinition<Product>.Empty); // Burada bizim toplam Ürün sayımızı döndürüyor
        }

        public async Task<decimal> GetProductAvgPrice()
        {
            var pipeline = new BsonDocument[]
  {
    new BsonDocument("$group", new BsonDocument
    {
        { "_id", BsonNull.Value },
        { "averagePrice", new BsonDocument("$avg", "$ProductPrice") }
    })
  };

            var result = await _productCollection.AggregateAsync<BsonDocument>(pipeline);
            var values = result.FirstOrDefault()?.GetValue("averagePrice", BsonDecimal128.Create(0)).AsDecimal ?? 0m;

            // 2 ondalık hane
            return Math.Round(values, 2);

        }
    }
}
