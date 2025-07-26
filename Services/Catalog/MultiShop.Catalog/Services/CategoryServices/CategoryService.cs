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

        public CategoryService(IMapper mapper, IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString); // MongoDB ile bağlantı kurmak için istemci oluşturuluyor.
            var database = client.GetDatabase(_databaseSettings.DatabaseName); // Bağlantı kurulan MongoDB'de kullanılacak veritabanı seçiliyor.
            _categoryCollection = database.GetCollection<Category>(_databaseSettings.CategoryCollectionName); // Category koleksiyonu üzerinden işlem yapabilmek için referans alınıyor.
            _mapper = mapper; // AutoMapper örneği sınıfın _mapper alanına atanıyor.
                              //Dependency Injection ile gelen AutoMapper örneğini sınıfın içindeki değişkene atar.
        }

        public async Task CreateCategoryAsync(CreateCategoryDTO createCategoryDTO)
        {
            var values = _mapper.Map<Category>(createCategoryDTO); // createCategoryDTO nesnesindeki verileri, Category tipindeki bir nesneye dönüştürür (mapler).
            await _categoryCollection.InsertOneAsync(values); // Dönüştürülen Category nesnesini, MongoDB'deki Category koleksiyonuna asenkron olarak ekler.


        }

        public async Task DeleteCategoryAsync(string id)
        {
            await _categoryCollection.DeleteOneAsync(x => x.CategoryID == id); // Verilen id'ye sahip olan Category nesnesini MongoDB'deki Category koleksiyonundan asenkron olarak siler.
        }

        public async Task<List<ResultCategoryDTO>> GetAllCategoryAsync()
        {
            var values = await _categoryCollection.Find(x => true).ToListAsync(); // MongoDB'deki Category koleksiyonundan tüm Category nesnelerini asenkron olarak alır.
            return _mapper.Map<List<ResultCategoryDTO>>(values); // Alınan Category nesnelerini ResultCategoryDTO tipine dönüştürür ve döner.
        }

        public async Task<GetByIDCategoryDTO> GetByIDCategoryAsync(string id)
        {
            var values = await _categoryCollection.Find<Category>(x => x.CategoryID == id).FirstOrDefaultAsync(); // Verilen id'ye sahip ilk Category nesnesini, MongoDB'deki Category koleksiyonundan asenkron olarak bulur (yoksa null döner).
            return _mapper.Map<GetByIDCategoryDTO>(values); // Bulunan Category nesnesini GetByIDCategoryDTO tipine mapleyip (dönüştürüp) döner.
        }

        public async Task UpdateCategoryAsync(UpdateCategoryDTO updateCategoryDTO)
        {
            var values = _mapper.Map<Category>(updateCategoryDTO); // updateCategoryDTO nesnesindeki verileri, Category tipindeki yeni bir nesneye dönüştürür.
            await _categoryCollection.FindOneAndReplaceAsync(
                x => x.CategoryID == updateCategoryDTO.CategoryID,
                values); // Verilen CategoryID'ye sahip olan Category nesnesini MongoDB'deki Category koleksiyonunda bulur ve bulduğu nesneyi tamamen values ile değiştirerek asenkron olarak günceller (tüm alanları yeni değerlerle değiştirir).

        }
    }
}
