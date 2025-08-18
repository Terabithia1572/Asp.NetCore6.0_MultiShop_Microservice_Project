using AutoMapper;
using MongoDB.Driver;
using MultiShop.Catalog.DTOs.BrandDTOs;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Services.BrandServices
{
    public class BrandService: IBrandService // BrandService sınıfı, IBrandService arayüzünü implement eder. Bu sınıf, marka işlemlerini gerçekleştirmek için gerekli metotları içerir.
    {
        private readonly IMongoCollection<Brand> _brandCollection; // MongoDB'deki Brand koleksiyonuna erişim sağlar.
        private readonly IMapper _mapper; // AutoMapper kullanarak DTO'lar ve Entity'ler arasında dönüşüm yapar. AutoMapper kütüphanesi ile nesneler arası otomatik dönüşüm işlemlerinde kullanılır.

        public BrandService(IMapper mapper, IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString); // MongoDB ile bağlantı kurmak için istemci oluşturuluyor.
            var database = client.GetDatabase(_databaseSettings.DatabaseName); // Bağlantı kurulan MongoDB'de kullanılacak veritabanı seçiliyor.
            _brandCollection = database.GetCollection<Brand>(_databaseSettings.BrandCollectionName); // Brand koleksiyonu üzerinden işlem yapabilmek için referans alınıyor.
            _mapper = mapper; // AutoMapper örneği sınıfın _mapper alanına atanıyor.
                              //Dependency Injection ile gelen AutoMapper örneğini sınıfın içindeki değişkene atar.
        }

        public async Task CreateBrandAsync(CreateBrandDTO createBrandDTO)
        {
            var values = _mapper.Map<Brand>(createBrandDTO); // createBrandDTO nesnesindeki verileri, Brand tipindeki bir nesneye dönüştürür (mapler).
            await _brandCollection.InsertOneAsync(values); // Dönüştürülen Brand nesnesini, MongoDB'deki Brand koleksiyonuna asenkron olarak ekler.


        }

        public async Task DeleteBrandAsync(string id)
        {
            await _brandCollection.DeleteOneAsync(x => x.BrandID == id); // Verilen id'ye sahip olan Brand nesnesini MongoDB'deki Brand koleksiyonundan asenkron olarak siler.
        }

        public async Task<List<ResultBrandDTO>> GetAllBrandAsync()
        {
            var values = await _brandCollection.Find(x => true).ToListAsync(); // MongoDB'deki Brand koleksiyonundan tüm Brand nesnelerini asenkron olarak alır.
            return _mapper.Map<List<ResultBrandDTO>>(values); // Alınan Brand nesnelerini ResultBrandDTO tipine dönüştürür ve döner.
        }

        public async Task<GetByIDBrandDTO> GetByIDBrandAsync(string id)
        {
            var values = await _brandCollection.Find<Brand>(x => x.BrandID == id).FirstOrDefaultAsync(); // Verilen id'ye sahip ilk Brand nesnesini, MongoDB'deki Brand koleksiyonundan asenkron olarak bulur (yoksa null döner).
            return _mapper.Map<GetByIDBrandDTO>(values); // Bulunan Brand nesnesini GetByIDBrandDTO tipine mapleyip (dönüştürüp) döner.
        }

        public async Task UpdateBrandAsync(UpdateBrandDTO updateBrandDTO)
        {
            var values = _mapper.Map<Brand>(updateBrandDTO); // updateBrandDTO nesnesindeki verileri, Brand tipindeki yeni bir nesneye dönüştürür.
            await _brandCollection.FindOneAndReplaceAsync(
                x => x.BrandID == updateBrandDTO.BrandID,
                values); // Verilen BrandID'ye sahip olan Brand nesnesini MongoDB'deki Brand koleksiyonunda bulur ve bulduğu nesneyi tamamen values ile değiştirerek asenkron olarak günceller (tüm alanları yeni değerlerle değiştirir).

        }
    }
}
