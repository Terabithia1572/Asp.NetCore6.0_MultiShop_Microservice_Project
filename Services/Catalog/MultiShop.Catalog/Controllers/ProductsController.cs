using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MultiShop.Catalog.DTOs.ProductDTOs;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Settings;
using MultiShop.Catalog.Services.ProductServices;

namespace MultiShop.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService; // Product Service için Dependency Injection 

        // 🔥 İndirimleri okuyabilmek için ProductDiscounts koleksiyonuna da erişeceğiz
        private readonly IMongoCollection<ProductDiscount> _productDiscountCollection; // MongoDB'deki ProductDiscounts koleksiyonu

        public ProductsController(IProductService productService, IDatabaseSettings databaseSettings)
        {
            _productService = productService; // Constructor üzerinden Product Service'i alıyoruz

            // 🔧 Mongo bağlamı kurup ProductDiscounts koleksiyonunu elde ediyoruz
            var client = new MongoClient(databaseSettings.ConnectionString);                  // MongoDB bağlantısı
            var database = client.GetDatabase(databaseSettings.DatabaseName);                 // Veritabanı seçimi
            _productDiscountCollection = database.GetCollection<ProductDiscount>("ProductDiscounts"); // İndirim koleksiyonu
        }

        // 🔓 [AllowAnonymous] — Ürün listesi public (ön yüzde herkes görebilsin)
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> ProductList()
        {
            var values = await _productService.GetAllProductAsync(); // Product Service üzerinden tüm Ürünleri alıyoruz
            if (values == null || !values.Any())
            {
                return NotFound("Ürün Bulunamadı."); // Eğer Ürün bulunamazsa 404 döndürüyoruz
            }
            return Ok(values); // Ürünler bulunduysa 200 OK ile birlikte Ürünleri döndürüyoruz
        }

        // 🔓 [AllowAnonymous] — Belirli bir ürünü herkes görebilsin
        [AllowAnonymous]
        [HttpGet("{id}")] // Belirli bir Ürün için id parametresi alıyoruz 
        public async Task<IActionResult> GetProductByID(string id)
        {
            var value = await _productService.GetByIDProductAsync(id); // Product Service üzerinden id ile Ürün alıyoruz
            if (value == null)
            {
                return NotFound("Ürün Bulunamadı."); // Eğer Ürün bulunamazsa 404 döndürüyoruz
            }
            return Ok(value); // Ürün bulunduysa 200 OK ile birlikte Ürünü döndürüyoruz
        }

        // 🔒 [Authorize] — Yeni ürün oluşturma sadece admin içindir
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductDTO createProductDTO)
        {
            await _productService.CreateProductAsync(createProductDTO); // Product Service üzerinden yeni Ürün oluşturuyoruz
            return Ok("Ürün Başarıyla Oluşturuldu."); // Ürün başarıyla oluşturulduysa 200 OK ile birlikte mesaj döndürüyoruz
        }

        // 🔒 [Authorize] — Ürün silme işlemi için admin yetkisi gerekir
        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            await _productService.DeleteProductAsync(id); // Product Service üzerinden id ile Ürün siliyoruz
            return Ok("Ürün Başarıyla Silindi."); // Ürün başarıyla silindiyse 200 OK ile birlikte mesaj döndürüyoruz
        }

        // 🔒 [Authorize] — Ürün güncelleme işlemi sadece admin için
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateProduct(UpdateProductDTO updateProductDTO)
        {
            await _productService.UpdateProductAsync(updateProductDTO); // Product Service üzerinden güncelleme işlemi yapıyoruz
            return Ok("Ürün Başarıyla Güncellendi."); // Ürün başarıyla güncellendiyse 200 OK ile birlikte mesaj döndürüyoruz
        }

        // 🔓 [AllowAnonymous] — Kategorilerle birlikte Ürünleri getirme işlemi public olabilir
        [AllowAnonymous]
        [HttpGet("ProductListWithCategory")] // Kategorilerle birlikte Ürünleri getirme işlemi için
        public async Task<IActionResult> ProductListWithCategory()
        {
            var values = await _productService.GetProductsWithCategoryAsync(); // Product Service üzerinden Kategorilerle birlikte Ürünleri alıyoruz
            if (values == null || !values.Any())
            {
                return NotFound("Ürün Bulunamadı."); // Eğer Ürün bulunamazsa 404 döndürüyoruz
            }
            return Ok(values); // Ürünler bulunduysa 200 OK ile birlikte Ürünleri döndürüyoruz
        }

        // 🔓 [AllowAnonymous] — Belirli kategorideki ürünleri getirme işlemi de public
        [AllowAnonymous]
        [HttpGet("ProductListWithCategoryByCategoryID/{id}")]
        public async Task<IActionResult> ProductListWithCategoryByCategoryID(string id)
        {
            var values = await _productService.GetProductsWithByCategoryByCategoryIDAsync(id);

            if (values == null || values.Count == 0)
                return NotFound("Ürün Bulunamadı.");

            return Ok(values);
        }

        // 🆕 🟢 ÜRÜNLERİ İNDİRİMLERLE DÖNDÜREN ENDPOINT
        // 🔓 [AllowAnonymous] — Ön yüzde kullanıcı login olmasa da anasayfada indirimli ürünler görebilmeli
        [AllowAnonymous]
        [HttpGet("GetProductsWithDiscount")]
        public async Task<IActionResult> GetProductsWithDiscount()
        {
            // 1) Ürünleri çek
            var products = await _productService.GetAllProductAsync(); // Sadece temel ürün DTO'ları (fiyat, ad vb.)
            if (products == null || !products.Any())
                return Ok(new List<ResultProductWithDiscountDTO>());   // Ürün yoksa boş liste

            // 2) Şu an aktif İNDİRİMLERİ çek (tarih aralığı + IsActive)
            var now = DateTime.UtcNow; // ⏱ UTC bazlı kontrol (tavsiye edilen)
            var activeDiscounts = await _productDiscountCollection
                .Find(x => x.IsActive && x.StartDate <= now && x.EndDate >= now)
                .ToListAsync();

            // 3) Ürünlere indirimleri uygula (ProductID ile eşleştir)
            var result = products.Select(p =>
            {
                var match = activeDiscounts.FirstOrDefault(d => d.ProductID == p.ProductID); // Aynı ProductID varsa indirimi yakala

                decimal? discountRate = match?.DiscountRate; // Oran yoksa null
                decimal discountedPrice = discountRate.HasValue
                    ? p.ProductPrice - (p.ProductPrice * discountRate.Value / 100) // % indirimi uygula
                    : p.ProductPrice;                                              // İndirim yok → orijinal fiyat

                return new ResultProductWithDiscountDTO
                {
                    ProductID = p.ProductID,                 // Ürün bilgileri
                    ProductName = p.ProductName,
                    ProductPrice = p.ProductPrice,
                    ProductImageURL = p.ProductImageURL,

                    DiscountRate = discountRate,             // İndirim oranı (null olabilir)
                    DiscountedPrice = discountedPrice        // Uygulanmış fiyat
                };
            }).ToList();

            return Ok(result); // 200 OK + indirimli ürün listesi
        }
    }
}
