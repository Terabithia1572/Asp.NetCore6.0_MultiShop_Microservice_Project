using AutoMapper;
using MongoDB.Driver;
using MultiShop.Catalog.DTOs.CategoryDTOs;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Services.CategoryServices
{
    public class CategoryService : ICategoryService
    {
        private readonly IMongoCollection<Category> _categoryCollection; // MongoDB'deki Category koleksiyonuna erişim sağlar.
        private readonly IMapper _mapper; // AutoMapper kullanarak DTO'lar ve Entity'ler arasında dönüşüm yapar. AutoMapper kütüphanesi ile nesneler arası otomatik dönüşüm işlemlerinde kullanılır.

        public CategoryService(IMapper mapper,IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString); // MongoDB ile bağlantı kurmak için istemci oluşturuluyor.
            var database = client.GetDatabase(_databaseSettings.DatabaseName); // Bağlantı kurulan MongoDB'de kullanılacak veritabanı seçiliyor.
            _categoryCollection = database.GetCollection<Category>(_databaseSettings.CategoryCollectionName); // Category koleksiyonu üzerinden işlem yapabilmek için referans alınıyor.
            _mapper = mapper; // AutoMapper örneği sınıfın _mapper alanına atanıyor.
                              //Dependency Injection ile gelen AutoMapper örneğini sınıfın içindeki değişkene atar.
        }

        public async Task CreateCategoryAsync(CreateCategoryDTO createCategoryDTO)
        {
            var values=_mapper.Map<Category>(createCategoryDTO); // CreateCategoryDTO nesnesini Category entity'sine dönüştürür.
            await _categoryCollection.InsertOneAsync(values); // Dönüştürülen Category nesnesini MongoDB'deki koleksiyona ekler.
        }

        public Task DeleteCategoryAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<List<ResultCategoryDTO>> GetAllCategoryAsync()
        {
            throw new NotImplementedException();
        }

        public Task<GetByIDCategoryDTO> GetByIDCategoryAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateCategoryAsync(UpdateCategoryDTO updateCategoryDTO)
        {
            throw new NotImplementedException();
        }
    }
}
