using AutoMapper;
using MongoDB.Driver;
using MultiShop.Catalog.DTOs.FeatureSliderDTOs;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Services.FeatureSliderServices
{
    public class FeatureSliderService : IFeatureSliderService
    {
        private readonly IMongoCollection<FeatureSlider> _featureSliderCollection; // MongoDB'deki FeatureSlider koleksiyonuna erişim sağlar.
        private readonly IMapper _mapper; // AutoMapper kullanarak DTO'lar ve Entity'ler arasında dönüşüm yapar. AutoMapper kütüphanesi ile nesneler arası otomatik dönüşüm işlemlerinde kullanılır.

        public FeatureSliderService(IMapper mapper, IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString); // MongoDB ile bağlantı kurmak için istemci oluşturuluyor.
            var database = client.GetDatabase(_databaseSettings.DatabaseName); // Bağlantı kurulan MongoDB'de kullanılacak veritabanı seçiliyor.
            _featureSliderCollection = database.GetCollection<FeatureSlider>(_databaseSettings.FeatureSliderCollectionName); // FeatureSlider koleksiyonu üzerinden işlem yapabilmek için referans alınıyor.
            _mapper = mapper; // AutoMapper örneği sınıfın _mapper alanına atanıyor.
                              //Dependency Injection ile gelen AutoMapper örneğini sınıfın içindeki değişkene atar.
        }

        public async Task CreateFeatureSliderAsync(CreateFeatureSliderDTO createFeatureSliderDTO)
        {
            var values = _mapper.Map<FeatureSlider>(createFeatureSliderDTO); // createCategoryDTO nesnesindeki verileri, Category tipindeki bir nesneye dönüştürür (mapler).
            await _featureSliderCollection.InsertOneAsync(values); // Dönüştürülen Category nesnesini, MongoDB'deki Category koleksiyonuna asenkron olarak ekler.
        }

        public async Task DeleteFeatureSliderAsync(string id)
        {
           await _featureSliderCollection.DeleteOneAsync(x => x.FeatureSliderID == id); // Verilen id'ye sahip olan FeatureSlider nesnesini MongoDB'deki FeatureSlider koleksiyonundan asenkron olarak siler.
        }

        public Task FeatureSliderChangeStatusToFalse(string id)
        {
            throw new NotImplementedException();
        }

        public Task FeatureSliderChangeStatusToTrue(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ResultFeatureSliderDTO>> GetAllFeatureSliderAsync()
        {
           var values =await _featureSliderCollection.Find(x => true).ToListAsync(); // MongoDB'deki FeatureSlider koleksiyonundan tüm FeatureSlider nesnelerini asenkron olarak alır.
            return _mapper.Map<List<ResultFeatureSliderDTO>>(values); // Alınan FeatureSlider nesnelerini ResultFeatureSliderDTO tipine dönüştürür ve döner.
        }

        public async Task<GetByIDFeatureSliderDTO> GetByIDFeatureSliderAsync(string id)
        {
            var values =await _featureSliderCollection.Find<FeatureSlider>(x => x.FeatureSliderID == id).FirstOrDefaultAsync(); // Verilen id'ye sahip ilk FeatureSlider nesnesini, MongoDB'deki FeatureSlider koleksiyonundan asenkron olarak bulur (yoksa null döner).
            return _mapper.Map<GetByIDFeatureSliderDTO>(values); // Bulunan FeatureSlider nesnesini GetByIDFeatureSliderDTO tipine mapleyip (dönüştürüp) döner.
        }

        public async Task UpdateFeatureSliderAsync(UpdateFeatureSliderDTO updateFeatureSliderDTO)
        {
            var values = _mapper.Map<FeatureSlider>(updateFeatureSliderDTO); // updateCategoryDTO nesnesindeki verileri, FeatureSlider tipindeki yeni bir nesneye dönüştürür.
            await _featureSliderCollection.FindOneAndReplaceAsync(
                x => x.FeatureSliderID == updateFeatureSliderDTO.FeatureSliderID,
                values); // Verilen FeatureSliderID'ye sahip olan FeatureSlider nesnesini MongoDB'deki FeatureSlider koleksiyonunda bulur ve bulduğu nesneyi tamamen values ile değiştirerek asenkron olarak günceller (tüm alanları yeni değerlerle değiştirir).
        }
    }
}
