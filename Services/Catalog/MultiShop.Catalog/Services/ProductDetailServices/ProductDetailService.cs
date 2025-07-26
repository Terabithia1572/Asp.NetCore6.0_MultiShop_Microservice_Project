using AutoMapper;
using MongoDB.Driver;
using MultiShop.Catalog.DTOs.ProductDetailDTOs;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Services.ProductDetailServices;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Services.ProductDetailDetailServices
{
    public class ProductDetailService : IProductDetailService
    {
        private readonly IMapper _mapper; // AutoMapper kullanarak DTO'lar ve Entity'ler arasında dönüşüm yapar. AutoMapper kütüphanesi ile nesneler arası otomatik dönüşüm işlemlerinde kullanılır.
        private readonly IMongoCollection<ProductDetail> _ProductDetailCollection; // MongoDB'deki ProductDetail koleksiyonuna erişim sağlar.

        public ProductDetailService(IMapper mapper, IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString); // MongoDB ile bağlantı kurmak için istemci oluşturuluyor.
            var database = client.GetDatabase(_databaseSettings.DatabaseName); // Bağlantı kurulan MongoDB'de kullanılacak veritabanı seçiliyor.
            _ProductDetailCollection = database.GetCollection<ProductDetail>(_databaseSettings.ProductDetailCollectionName); // ProductDetail koleksiyonu üzerinden işlem yapabilmek için referans alınıyor.
            _mapper = mapper; // AutoMapper örneği sınıfın _mapper alanına atanıyor.
        }

        public async Task CreateProductDetailAsync(CreateProductDetailDTO createProductDetailDTO)
        {
            var values = _mapper.Map<ProductDetail>(createProductDetailDTO); // createProductDetailDTO nesnesindeki verileri, ProductDetail tipindeki bir nesneye dönüştürür (mapler).
            await _ProductDetailCollection.InsertOneAsync(values); // Dönüştürülen ProductDetail nesnesini, MongoDB'deki ProductDetail koleksiyonuna asenkron olarak ekler.
        }

        public async Task DeleteProductDetailAsync(string id)
        {
            await _ProductDetailCollection.DeleteOneAsync(x => x.ProductDetailID == id); // Verilen id'ye sahip olan ProductDetail nesnesini MongoDB'deki ProductDetail koleksiyonundan asenkron olarak siler.

        }

        public async Task<List<ResultProductDetailDTO>> GetAllProductDetailAsync()
        {
            var values = await _ProductDetailCollection.Find(x => true).ToListAsync(); // MongoDB'deki ProductDetail koleksiyonundan tüm ProductDetail nesnelerini asenkron olarak alır.
            return _mapper.Map<List<ResultProductDetailDTO>>(values); // Alınan ProductDetail nesnelerini ResultProductDetailDTO tipine dönüştürür ve döner.
        }

        public async Task<GetByIDProductDetailDTO> GetByIDProductDetailAsync(string id)
        {
            var values = await _ProductDetailCollection.Find<ProductDetail>(x => x.ProductDetailID == id).FirstOrDefaultAsync(); // Verilen id'ye sahip ilk ProductDetail nesnesini, MongoDB'deki ProductDetail koleksiyonundan asenkron olarak bulur (yoksa null döner).
            return _mapper.Map<GetByIDProductDetailDTO>(values); // Bulunan ProductDetail nesnesini GetByIDProductDetailDTO tipine mapleyip (dönüştürüp) döner.

        }

        public async Task UpdateProductDetailAsync(UpdateProductDetailDTO updateProductDetailDTO)
        {
            var values = _mapper.Map<ProductDetail>(updateProductDetailDTO); // updateProductDetailDTO nesnesindeki verileri, ProductDetail tipindeki yeni bir nesneye dönüştürür.
            await _ProductDetailCollection.FindOneAndReplaceAsync(
                x => x.ProductDetailID == updateProductDetailDTO.ProductDetailID,
                values); // Verilen ProductDetailID'ye sahip olan ProductDetail nesnesini MongoDB'deki ProductDetail koleksiyonunda bulur ve bulduğu nesneyi tamamen values ile değiştirerek asenkron olarak günceller (tüm alanları yeni değerlerle değiştirir).
        }
    }
}

