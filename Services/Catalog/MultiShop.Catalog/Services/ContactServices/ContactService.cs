using AutoMapper;
using MongoDB.Driver;
using MultiShop.Catalog.DTOs.ContactDTOs;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Services.ContactServices
{
    public class ContactService: IContactService
    {
        private readonly IMongoCollection<Contact> _contactCollection; // MongoDB'deki Contact koleksiyonuna erişim sağlar.
        private readonly IMapper _mapper; // AutoMapper kullanarak DTO'lar ve Entity'ler arasında dönüşüm yapar. AutoMapper kütüphanesi ile nesneler arası otomatik dönüşüm işlemlerinde kullanılır.

        public ContactService(IMapper mapper, IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString); // MongoDB ile bağlantı kurmak için istemci oluşturuluyor.
            var database = client.GetDatabase(_databaseSettings.DatabaseName); // Bağlantı kurulan MongoDB'de kullanılacak veritabanı seçiliyor.
            _contactCollection = database.GetCollection<Contact>(_databaseSettings.ContactCollectionName); // Contact koleksiyonu üzerinden işlem yapabilmek için referans alınıyor.
            _mapper = mapper; // AutoMapper örneği sınıfın _mapper alanına atanıyor.
                              //Dependency Injection ile gelen AutoMapper örneğini sınıfın içindeki değişkene atar.
        }

        public async Task CreateContactAsync(CreateContactDTO createContactDTO)
        {
            var values = _mapper.Map<Contact>(createContactDTO); // createContactDTO nesnesindeki verileri, Contact tipindeki bir nesneye dönüştürür (mapler).
            await _contactCollection.InsertOneAsync(values); // Dönüştürülen Contact nesnesini, MongoDB'deki Contact koleksiyonuna asenkron olarak ekler.


        }

        public async Task DeleteContactAsync(string id)
        {
            await _contactCollection.DeleteOneAsync(x => x.ContactID == id); // Verilen id'ye sahip olan Contact nesnesini MongoDB'deki Contact koleksiyonundan asenkron olarak siler.
        }

        public async Task<List<ResultContactDTO>> GetAllContactAsync()
        {
            var values = await _contactCollection.Find(x => true).ToListAsync(); // MongoDB'deki Contact koleksiyonundan tüm Contact nesnelerini asenkron olarak alır.
            return _mapper.Map<List<ResultContactDTO>>(values); // Alınan Contact nesnelerini ResultContactDTO tipine dönüştürür ve döner.
        }

        public async Task<GetByIDContactDTO> GetByIDContactAsync(string id)
        {
            var values = await _contactCollection.Find<Contact>(x => x.ContactID == id).FirstOrDefaultAsync(); // Verilen id'ye sahip ilk Contact nesnesini, MongoDB'deki Contact koleksiyonundan asenkron olarak bulur (yoksa null döner).
            return _mapper.Map<GetByIDContactDTO>(values); // Bulunan Contact nesnesini GetByIDContactDTO tipine mapleyip (dönüştürüp) döner.
        }

        public async Task UpdateContactAsync(UpdateContactDTO updateContactDTO)
        {
            var values = _mapper.Map<Contact>(updateContactDTO); // updateContactDTO nesnesindeki verileri, Contact tipindeki yeni bir nesneye dönüştürür.
            await _contactCollection.FindOneAndReplaceAsync(
                x => x.ContactID == updateContactDTO.ContactID,
                values); // Verilen ContactID'ye sahip olan Contact nesnesini MongoDB'deki Contact koleksiyonunda bulur ve bulduğu nesneyi tamamen values ile değiştirerek asenkron olarak günceller (tüm alanları yeni değerlerle değiştirir).

        }
    }
}
