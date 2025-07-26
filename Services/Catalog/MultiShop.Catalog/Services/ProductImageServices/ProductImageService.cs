using AutoMapper;
using MongoDB.Driver;
using MultiShop.Catalog.DTOs.ProductImageDTOs;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Services.ProductImageServices
{
    public class ProductImageService : IProductImageService
    {
        private readonly IMongoCollection<ProductImage> _ProductImageCollection; // MongoDB'deki ProductImage koleksiyonuna erişim sağlar.
        private readonly IMapper _mapper; // AutoMapper kullanarak DTO'lar ve Entity'ler arasında dönüşüm yapar. AutoMapper kütüphanesi ile nesneler arası otomatik dönüşüm işlemlerinde kullanılır.

        public ProductImageService(IMapper mapper, IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString); // MongoDB ile bağlantı kurmak için istemci oluşturuluyor.
            var database = client.GetDatabase(_databaseSettings.DatabaseName); // Bağlantı kurulan MongoDB'de kullanılacak veritabanı seçiliyor.
            _ProductImageCollection = database.GetCollection<ProductImage>(_databaseSettings.ProductImageCollectionName); // ProductImage koleksiyonu üzerinden işlem yapabilmek için referans alınıyor.
            _mapper = mapper; // AutoMapper örneği sınıfın _mapper alanına atanıyor.
                              //Dependency Injection ile gelen AutoMapper örneğini sınıfın içindeki değişkene atar.
        }

        public async Task CreateProductImageAsync(CreateProductImageDTO createProductImageDTO)
        {
            var values = _mapper.Map<ProductImage>(createProductImageDTO); // createProductImageDTO nesnesindeki verileri, ProductImage tipindeki bir nesneye dönüştürür (mapler).
            await _ProductImageCollection.InsertOneAsync(values); // Dönüştürülen ProductImage nesnesini, MongoDB'deki ProductImage koleksiyonuna asenkron olarak ekler.


        }

        public async Task DeleteProductImageAsync(string id)
        {
            await _ProductImageCollection.DeleteOneAsync(x => x.ProductImagesID == id); // Verilen id'ye sahip olan ProductImage nesnesini MongoDB'deki ProductImage koleksiyonundan asenkron olarak siler.
        }

        public async Task<List<ResultProductImageDTO>> GetAllProductImageAsync()
        {
            var values = await _ProductImageCollection.Find(x => true).ToListAsync(); // MongoDB'deki ProductImage koleksiyonundan tüm ProductImage nesnelerini asenkron olarak alır.
            return _mapper.Map<List<ResultProductImageDTO>>(values); // Alınan ProductImage nesnelerini ResultProductImageDTO tipine dönüştürür ve döner.
        }

        public async Task<GetByIDProductImageDTO> GetByIDProductImageAsync(string id)
        {
            var values = await _ProductImageCollection.Find<ProductImage>(x => x.ProductImagesID == id).FirstOrDefaultAsync(); // Verilen id'ye sahip ilk ProductImage nesnesini, MongoDB'deki ProductImage koleksiyonundan asenkron olarak bulur (yoksa null döner).
            return _mapper.Map<GetByIDProductImageDTO>(values); // Bulunan ProductImage nesnesini GetByIDProductImageDTO tipine mapleyip (dönüştürüp) döner.
        }

        public async Task UpdateProductImageAsync(UpdateProductImageDTO updateProductImageDTO)
        {
            var values = _mapper.Map<ProductImage>(updateProductImageDTO); // updateProductImageDTO nesnesindeki verileri, ProductImage tipindeki yeni bir nesneye dönüştürür.
            await _ProductImageCollection.FindOneAndReplaceAsync(
                x => x.ProductImagesID == updateProductImageDTO.ProductImagesID,
                values); // Verilen ProductImageID'ye sahip olan ProductImage nesnesini MongoDB'deki ProductImage koleksiyonunda bulur ve bulduğu nesneyi tamamen values ile değiştirerek asenkron olarak günceller (tüm alanları yeni değerlerle değiştirir).

        }
    }
}
