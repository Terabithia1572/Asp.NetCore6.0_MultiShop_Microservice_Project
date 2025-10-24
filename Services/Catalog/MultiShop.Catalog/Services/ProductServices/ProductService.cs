using AutoMapper;
using MongoDB.Driver;
using MultiShop.Catalog.DTOs.ProductDTOs;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Settings;
using Newtonsoft.Json;
using System.Net.Http;

namespace MultiShop.Catalog.Services.ProductServices
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper; // AutoMapper kullanarak DTO'lar ve Entity'ler arasında dönüşüm yapar. AutoMapper kütüphanesi ile nesneler arası otomatik dönüşüm işlemlerinde kullanılır.
        private readonly IMongoCollection<Product> _productCollection; // MongoDB'deki Product koleksiyonuna erişim sağlar.
        private readonly IMongoCollection<Category> _categoryCollection; // MongoDB'deki Category koleksiyonuna erişim sağlar.
        private readonly HttpClient _httpClient; // ✅ Yeni eklendi: Gateway'e istek atmak için HttpClient kullanıyoruz.
        private readonly IMongoCollection<ProductDiscount> _productDiscountCollection; // ✅ ürün bazlı indirim koleksiyonu


        public ProductService(IMapper mapper, IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString); // MongoDB ile bağlantı kurmak için istemci oluşturuluyor.
            var database = client.GetDatabase(_databaseSettings.DatabaseName); // Bağlantı kurulan MongoDB'de kullanılacak veritabanı seçiliyor.
            _productCollection = database.GetCollection<Product>(_databaseSettings.ProductCollectionName); // Product koleksiyonu üzerinden işlem yapabilmek için referans alınıyor.
            _categoryCollection = database.GetCollection<Category>(_databaseSettings.CategoryCollectionName); // Category koleksiyonu üzerinden işlem yapabilmek için referans alınıyor.
            _productDiscountCollection = database.GetCollection<ProductDiscount>(_databaseSettings.ProductDiscountCollectionName); // ✅ indirim koleksiyonu
            _mapper = mapper; // AutoMapper örneği sınıfın _mapper alanına atanıyor.

            _httpClient = new HttpClient(); // ✅ HttpClient örneği oluşturuluyor (Gateway'e API istekleri atmak için kullanılacak).
        }

        public async Task CreateProductAsync(CreateProductDTO createProductDTO)
        {
            var values = _mapper.Map<Product>(createProductDTO); // createProductDTO nesnesindeki verileri, Product tipindeki bir nesneye dönüştürür (mapler).
            await _productCollection.InsertOneAsync(values); // Dönüştürülen Product nesnesini, MongoDB'deki Product koleksiyonuna asenkron olarak ekler.
        }

        public async Task DeleteProductAsync(string id)
        {
            await _productCollection.DeleteOneAsync(x => x.ProductID == id); // Verilen id'ye sahip olan Product nesnesini MongoDB'deki Product koleksiyonundan asenkron olarak siler.
        }

        public async Task<List<ResultProductDTO>> GetAllProductAsync()
        {
            var values = await _productCollection.Find(x => true).ToListAsync(); // MongoDB'deki Product koleksiyonundan tüm Product nesnelerini asenkron olarak alır.
            return _mapper.Map<List<ResultProductDTO>>(values); // Alınan Product nesnelerini ResultProductDTO tipine dönüştürür ve döner.
        }

        // 🔥 Yeni metod – indirimlerle birlikte ürünleri getirir
        // ✅ Yeni metod – Ürünleri aktif ProductDiscount ile birleştir
        public async Task<List<ResultProductWithDiscountDTO>> GetAllProductWithDiscountAsync()
        {
            var now = DateTime.Now;

            // ürünleri çek
            var products = await _productCollection.Find(_ => true).ToListAsync();

            // aktif ve tarih aralığında kalan indirimler
            var activeDiscounts = await _productDiscountCollection
                .Find(d => d.IsActive && d.StartDate <= now && d.EndDate >= now)
                .ToListAsync();

            // ürün + indirim merge
            var result = products.Select(p =>
            {
                var d = activeDiscounts
                    .Where(x => x.ProductID == p.ProductID)
                    .OrderByDescending(x => x.DiscountRate) // aynı ürüne birden fazla indirim varsa en yüksek oranı uygula
                    .FirstOrDefault();

                return new ResultProductWithDiscountDTO
                {
                    ProductID = p.ProductID,
                    ProductName = p.ProductName,
                    ProductPrice = p.ProductPrice,
                    ProductImageURL = p.ProductImageURL,
                    DiscountRate = d != null ? d.DiscountRate : (decimal?)null
                    // DiscountedPrice'e atama YOK; hesaplanan property.
                };
            }).ToList();

            return result;
        }



        public async Task<GetByIDProductDTO> GetByIDProductAsync(string id)
        {
            var values = await _productCollection.Find<Product>(x => x.ProductID == id).FirstOrDefaultAsync(); // Verilen id'ye sahip ilk Product nesnesini, MongoDB'deki Product koleksiyonundan asenkron olarak bulur (yoksa null döner).
            return _mapper.Map<GetByIDProductDTO>(values); // Bulunan Product nesnesini GetByIDProductDTO tipine mapleyip (dönüştürüp) döner.
        }

        public async Task<List<ResultProductsWithCategoryDTO>> GetProductsWithByCategoryByCategoryIDAsync(string categoryID)
        {
            var values = await _productCollection.Find(x => x.CategoryID == categoryID).ToListAsync(); // Belirli bir kategoriye ait tüm ürünleri alır.

            foreach (var item in values)
            {
                item.Category = await _categoryCollection
                    .Find<Category>(x => x.CategoryID == item.CategoryID)
                    .FirstOrDefaultAsync(); // Her ürün için ilgili kategoriyi getirir (kategori yoksa null olur, exception atmaz).
            }

            return _mapper.Map<List<ResultProductsWithCategoryDTO>>(values); // Ürün + kategori listesini DTO'ya dönüştürür ve döner.
        }

        public async Task<List<ResultProductsWithCategoryDTO>> GetProductsWithCategoryAsync()
        {
            var values = await _productCollection.Find(x => true).ToListAsync(); // MongoDB'deki Product koleksiyonundan tüm Product nesnelerini asenkron olarak alır.
            foreach (var item in values)
            {
                item.Category = await _categoryCollection.Find<Category>(x => x.CategoryID == item.CategoryID).FirstAsync();  // Her Product nesnesi için, ilgili Category nesnesini MongoDB'deki Category koleksiyonundan bulur ve Product nesnesinin Category özelliğine atar.
            }
            return _mapper.Map<List<ResultProductsWithCategoryDTO>>(values); // Alınan Product nesnelerini ResultProductsWithCategoryDTO tipine dönüştürür ve döner.
        }

        public async Task UpdateProductAsync(UpdateProductDTO updateProductDTO)
        {
            var values = _mapper.Map<Product>(updateProductDTO); // updateProductDTO nesnesindeki verileri, Product tipindeki yeni bir nesneye dönüştürür.
            await _productCollection.FindOneAndReplaceAsync(
                x => x.ProductID == updateProductDTO.ProductID,
                values); // Verilen ProductID'ye sahip olan Product nesnesini MongoDB'deki Product koleksiyonunda bulur ve bulduğu nesneyi tamamen values ile değiştirerek asenkron olarak günceller (tüm alanları yeni değerlerle değiştirir).
        }
        public async Task<List<ResultProductsWithCategoryDTO>> GetAllProductsWithCategoryAsync()
        {
            var values = await _productCollection.Find(x => true).ToListAsync(); // Tüm ürünler
            foreach (var item in values)
            {
                item.Category = await _categoryCollection
                    .Find<Category>(x => x.CategoryID == item.CategoryID)
                    .FirstOrDefaultAsync();
            }

            return _mapper.Map<List<ResultProductsWithCategoryDTO>>(values);
        }


    }
}
