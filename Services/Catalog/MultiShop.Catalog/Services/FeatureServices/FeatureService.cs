using AutoMapper;
using MongoDB.Driver;
using MultiShop.Catalog.DTOs.FeatureDTOs;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Services.FeatureServices
{
    public class FeatureService : IFeatureService
    {
        private readonly IMongoCollection<Feature> _featureCollection; // MongoDB'deki Feature koleksiyonuna erişim sağlar.
        private readonly IMapper _mapper; // AutoMapper kullanarak DTO'lar ve Entity'ler arasında dönüşüm yapar. AutoMapper kütüphanesi ile nesneler arası otomatik dönüşüm işlemlerinde kullanılır.

        public FeatureService(IMapper mapper, IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString); // MongoDB ile bağlantı kurmak için istemci oluşturuluyor.
            var database = client.GetDatabase(_databaseSettings.DatabaseName); // Bağlantı kurulan MongoDB'de kullanılacak veritabanı seçiliyor.
            _featureCollection = database.GetCollection<Feature>(_databaseSettings.FeatureCollectionName); // Feature koleksiyonu üzerinden işlem yapabilmek için referans alınıyor.
            _mapper = mapper; // AutoMapper örneği sınıfın _mapper alanına atanıyor.
                              //Dependency Injection ile gelen AutoMapper örneğini sınıfın içindeki değişkene atar.
        }

        public async Task CreateFeatureAsync(CreateFeatureDTO createFeatureDTO)
        {
            var values = _mapper.Map<Feature>(createFeatureDTO); // createFeatureDTO nesnesindeki verileri, Feature tipindeki bir nesneye dönüştürür (mapler).
            await _featureCollection.InsertOneAsync(values); // Dönüştürülen Feature nesnesini, MongoDB'deki Feature koleksiyonuna asenkron olarak ekler.


        }

        public async Task DeleteFeatureAsync(string id)
        {
            await _featureCollection.DeleteOneAsync(x => x.FeatureID == id); // Verilen id'ye sahip olan Feature nesnesini MongoDB'deki Feature koleksiyonundan asenkron olarak siler.
        }

        public async Task<List<ResultFeatureDTO>> GetAllFeatureAsync()
        {
            var values = await _featureCollection.Find(x => true).ToListAsync(); // MongoDB'deki Feature koleksiyonundan tüm Feature nesnelerini asenkron olarak alır.
            return _mapper.Map<List<ResultFeatureDTO>>(values); // Alınan Feature nesnelerini ResultFeatureDTO tipine dönüştürür ve döner.
        }

        public async Task<GetByIDFeatureDTO> GetByIDFeatureAsync(string id)
        {
            var values = await _featureCollection.Find<Feature>(x => x.FeatureID == id).FirstOrDefaultAsync(); // Verilen id'ye sahip ilk Feature nesnesini, MongoDB'deki Feature koleksiyonundan asenkron olarak bulur (yoksa null döner).
            return _mapper.Map<GetByIDFeatureDTO>(values); // Bulunan Feature nesnesini GetByIDFeatureDTO tipine mapleyip (dönüştürüp) döner.
        }

        public async Task UpdateFeatureAsync(UpdateFeatureDTO updateFeatureDTO)
        {
            var values = _mapper.Map<Feature>(updateFeatureDTO); // updateFeatureDTO nesnesindeki verileri, Feature tipindeki yeni bir nesneye dönüştürür.
            await _featureCollection.FindOneAndReplaceAsync(
                x => x.FeatureID == updateFeatureDTO.FeatureID,
                values); // Verilen FeatureID'ye sahip olan Feature nesnesini MongoDB'deki Feature koleksiyonunda bulur ve bulduğu nesneyi tamamen values ile değiştirerek asenkron olarak günceller (tüm alanları yeni değerlerle değiştirir).

        }
    }
}
