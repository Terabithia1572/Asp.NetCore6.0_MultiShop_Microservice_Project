using AutoMapper;
using MongoDB.Driver;
using MultiShop.Catalog.DTOs.SpecialOfferDTOs;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Services.SpecialOfferServices
{
    public class SpecialOfferService : ISpecialOfferService
    {
        private readonly IMongoCollection<SpecialOffer> _specialOfferCollection; // MongoDB'deki SpecialOffer koleksiyonuna erişim sağlar.
        private readonly IMapper _mapper; // AutoMapper kullanarak DTO'lar ve Entity'ler arasında dönüşüm yapar. AutoMapper kütüphanesi ile nesneler arası otomatik dönüşüm işlemlerinde kullanılır.

        public SpecialOfferService(IMapper mapper, IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString); // MongoDB ile bağlantı kurmak için istemci oluşturuluyor.
            var database = client.GetDatabase(_databaseSettings.DatabaseName); // Bağlantı kurulan MongoDB'de kullanılacak veritabanı seçiliyor.
            _specialOfferCollection = database.GetCollection<SpecialOffer>(_databaseSettings.SpecialOfferCollectionName); // SpecialOffer koleksiyonu üzerinden işlem yapabilmek için referans alınıyor.
            _mapper = mapper; // AutoMapper örneği sınıfın _mapper alanına atanıyor.
                              //Dependency Injection ile gelen AutoMapper örneğini sınıfın içindeki değişkene atar.
        }

        public async Task CreateSpecialOfferAsync(CreateSpecialOfferDTO createSpecialOfferDTO)
        {
            var values = _mapper.Map<SpecialOffer>(createSpecialOfferDTO); // createSpecialOfferDTO nesnesindeki verileri, SpecialOffer tipindeki bir nesneye dönüştürür (mapler).
            await _specialOfferCollection.InsertOneAsync(values); // Dönüştürülen SpecialOffer nesnesini, MongoDB'deki SpecialOffer koleksiyonuna asenkron olarak ekler.


        }

        public async Task DeleteSpecialOfferAsync(string id)
        {
            await _specialOfferCollection.DeleteOneAsync(x => x.SpecialOfferID == id); // Verilen id'ye sahip olan SpecialOffer nesnesini MongoDB'deki SpecialOffer koleksiyonundan asenkron olarak siler.
        }

        public async Task<List<ResultSpecialOfferDTO>> GetAllSpecialOfferAsync()
        {
            var values = await _specialOfferCollection.Find(x => true).ToListAsync(); // MongoDB'deki SpecialOffer koleksiyonundan tüm SpecialOffer nesnelerini asenkron olarak alır.
            return _mapper.Map<List<ResultSpecialOfferDTO>>(values); // Alınan SpecialOffer nesnelerini ResultSpecialOfferDTO tipine dönüştürür ve döner.
        }

        public async Task<GetByIDSpecialOfferDTO> GetByIDSpecialOfferAsync(string id)
        {
            var values = await _specialOfferCollection.Find<SpecialOffer>(x => x.SpecialOfferID == id).FirstOrDefaultAsync(); // Verilen id'ye sahip ilk SpecialOffer nesnesini, MongoDB'deki SpecialOffer koleksiyonundan asenkron olarak bulur (yoksa null döner).
            return _mapper.Map<GetByIDSpecialOfferDTO>(values); // Bulunan SpecialOffer nesnesini GetByIDSpecialOfferDTO tipine mapleyip (dönüştürüp) döner.
        }

        public async Task UpdateSpecialOfferAsync(UpdateSpecialOfferDTO updateSpecialOfferDTO)
        {
            var values = _mapper.Map<SpecialOffer>(updateSpecialOfferDTO); // updateSpecialOfferDTO nesnesindeki verileri, SpecialOffer tipindeki yeni bir nesneye dönüştürür.
            await _specialOfferCollection.FindOneAndReplaceAsync(
                x => x.SpecialOfferID == updateSpecialOfferDTO.SpecialOfferID,
                values); // Verilen SpecialOfferID'ye sahip olan SpecialOffer nesnesini MongoDB'deki SpecialOffer koleksiyonunda bulur ve bulduğu nesneyi tamamen values ile değiştirerek asenkron olarak günceller (tüm alanları yeni değerlerle değiştirir).

        }
    }
}
