using MongoDB.Driver;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Settings;

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

        public async Task<long> GetProduceCount()
        {
            return await _productCollection.CountDocumentsAsync(FilterDefinition<Product>.Empty); // Burada bizim toplam Ürün sayımızı döndürüyor
        }

        public decimal GetProductAvgPrice()
        {
            throw new NotImplementedException();
        }
    }
}
