using AutoMapper;
using MongoDB.Driver;
using MultiShop.Catalog.DTOs.OfferDiscountDTOs;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Services.OfferDiscountServices
{
    public class OfferDiscountService : IOfferDiscountService
    {
        private readonly IMongoCollection<OfferDiscount> _offerDiscountCollection; // MongoDB'deki OfferDiscount koleksiyonuna erişim sağlar.
        private readonly IMapper _mapper; // AutoMapper kullanarak DTO'lar ve Entity'ler arasında dönüşüm yapar. AutoMapper kütüphanesi ile nesneler arası otomatik dönüşüm işlemlerinde kullanılır.

        public OfferDiscountService(IMapper mapper, IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString); // MongoDB ile bağlantı kurmak için istemci oluşturuluyor.
            var database = client.GetDatabase(_databaseSettings.DatabaseName); // Bağlantı kurulan MongoDB'de kullanılacak veritabanı seçiliyor.
            _offerDiscountCollection = database.GetCollection<OfferDiscount>(_databaseSettings.OfferDiscountCollectionName); // OfferDiscount koleksiyonu üzerinden işlem yapabilmek için referans alınıyor.
            _mapper = mapper; // AutoMapper örneği sınıfın _mapper alanına atanıyor.
                              //Dependency Injection ile gelen AutoMapper örneğini sınıfın içindeki değişkene atar.
        }
        public async Task CreateOfferDiscountAsync(CreateOfferDiscountDTO createOfferDiscountDTO)
        {
            var values = _mapper.Map<OfferDiscount>(createOfferDiscountDTO); // createOfferDiscountDTO nesnesindeki verileri, OfferDiscount tipindeki bir nesneye dönüştürür (mapler).
            await _offerDiscountCollection.InsertOneAsync(values); // Dönüştürülen OfferDiscount nesnesini, MongoDB'deki OfferDiscount koleksiyonuna asenkron olarak ekler.
        }

        public async Task DeleteOfferDiscountAsync(string id)
        {
            await _offerDiscountCollection.DeleteOneAsync(x => x.OfferDiscountID == id); // Verilen id'ye sahip olan OfferDiscount nesnesini MongoDB'deki OfferDiscount koleksiyonundan asenkron olarak siler.
        }

        public async Task<List<ResultOfferDiscountDTO>> GetAllOfferDiscountAsync()
        {
           var values = await _offerDiscountCollection.Find(x => true).ToListAsync(); // MongoDB'deki OfferDiscount koleksiyonundan tüm OfferDiscount nesnelerini asenkron olarak alır.
            return _mapper.Map<List<ResultOfferDiscountDTO>>(values); // Alınan OfferDiscount nesnelerini ResultOfferDiscountDTO tipine dönüştürür ve döner.
        }

        public async Task<GetByIDOfferDiscountDTO> GetByIDOfferDiscountAsync(string id)
        {
            var values =await _offerDiscountCollection.Find<OfferDiscount>(x => x.OfferDiscountID == id).FirstOrDefaultAsync(); // Verilen id'ye sahip ilk OfferDiscount nesnesini, MongoDB'deki OfferDiscount koleksiyonundan asenkron olarak bulur (yoksa null döner).
            return _mapper.Map<GetByIDOfferDiscountDTO>(values); // Bulunan OfferDiscount nesnesini GetByIDOfferDiscountDTO tipine mapleyip (dönüştürüp) döner.
        }

        public async Task UpdateOfferDiscountAsync(UpdateOfferDiscountDTO updateOfferDiscountDTO)
        {
            var values = _mapper.Map<OfferDiscount>(updateOfferDiscountDTO); // updateOfferDiscountDTO nesnesindeki verileri, OfferDiscount tipindeki yeni bir nesneye dönüştürür.
            await _offerDiscountCollection.FindOneAndReplaceAsync(
                x => x.OfferDiscountID == updateOfferDiscountDTO.OfferDiscountID,
                values); // Verilen OfferDiscountID'ye sahip olan OfferDiscount nesnesini MongoDB'deki OfferDiscount koleksiyonunda bulur ve bulduğu nesneyi tamamen values ile değiştirerek asenkron olarak günceller (tüm alanları yeni değerlerle değiştirir).

        }
    }
}
