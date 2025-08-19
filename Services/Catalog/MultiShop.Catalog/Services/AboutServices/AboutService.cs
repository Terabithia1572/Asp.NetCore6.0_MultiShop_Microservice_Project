using AutoMapper;
using MongoDB.Driver;
using MultiShop.Catalog.DTOs.AboutDTOs;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Services.AboutServices
{
    public class AboutService:IAboutService
    {
        private readonly IMongoCollection<About> _aboutCollection; // MongoDB'deki About koleksiyonuna erişim sağlar.
        private readonly IMapper _mapper; // AutoMapper kullanarak DTO'lar ve Entity'ler arasında dönüşüm yapar. AutoMapper kütüphanesi ile nesneler arası otomatik dönüşüm işlemlerinde kullanılır.

        public AboutService(IMapper mapper, IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString); // MongoDB ile bağlantı kurmak için istemci oluşturuluyor.
            var database = client.GetDatabase(_databaseSettings.DatabaseName); // Bağlantı kurulan MongoDB'de kullanılacak veritabanı seçiliyor.
            _aboutCollection = database.GetCollection<About>(_databaseSettings.AboutCollectionName); // About koleksiyonu üzerinden işlem yapabilmek için referans alınıyor.
            _mapper = mapper; // AutoMapper örneği sınıfın _mapper alanına atanıyor.
                              //Dependency Injection ile gelen AutoMapper örneğini sınıfın içindeki değişkene atar.
        }

        public async Task CreateAboutAsync(CreateAboutDTO createAboutDTO)
        {
            var values = _mapper.Map<About>(createAboutDTO); // createAboutDTO nesnesindeki verileri, About tipindeki bir nesneye dönüştürür (mapler).
            await _aboutCollection.InsertOneAsync(values); // Dönüştürülen About nesnesini, MongoDB'deki About koleksiyonuna asenkron olarak ekler.


        }

        public async Task DeleteAboutAsync(string id)
        {
            await _aboutCollection.DeleteOneAsync(x => x.AboutID == id); // Verilen id'ye sahip olan About nesnesini MongoDB'deki About koleksiyonundan asenkron olarak siler.
        }

        public async Task<List<ResultAboutDTO>> GetAllAboutAsync()
        {
            var values = await _aboutCollection.Find(x => true).ToListAsync(); // MongoDB'deki About koleksiyonundan tüm About nesnelerini asenkron olarak alır.
            return _mapper.Map<List<ResultAboutDTO>>(values); // Alınan About nesnelerini ResultAboutDTO tipine dönüştürür ve döner.
        }

        public async Task<GetByIDAboutDTO> GetByIDAboutAsync(string id)
        {
            var values = await _aboutCollection.Find<About>(x => x.AboutID == id).FirstOrDefaultAsync(); // Verilen id'ye sahip ilk About nesnesini, MongoDB'deki About koleksiyonundan asenkron olarak bulur (yoksa null döner).
            return _mapper.Map<GetByIDAboutDTO>(values); // Bulunan About nesnesini GetByIDAboutDTO tipine mapleyip (dönüştürüp) döner.
        }

        public async Task UpdateAboutAsync(UpdateAboutDTO updateAboutDTO)
        {
            var values = _mapper.Map<About>(updateAboutDTO); // updateAboutDTO nesnesindeki verileri, About tipindeki yeni bir nesneye dönüştürür.
            await _aboutCollection.FindOneAndReplaceAsync(
                x => x.AboutID == updateAboutDTO.AboutID,
                values); // Verilen AboutID'ye sahip olan About nesnesini MongoDB'deki About koleksiyonunda bulur ve bulduğu nesneyi tamamen values ile değiştirerek asenkron olarak günceller (tüm alanları yeni değerlerle değiştirir).

        }
    }
}
