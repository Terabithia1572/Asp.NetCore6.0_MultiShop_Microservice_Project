using Microsoft.AspNetCore.Mvc;
using MultiShop.DTOLayer.CatalogDTOs.ProductDTOs;
using MultiShop.WebUI.Services.CatalogServices.CategoryServices;
using MultiShop.WebUI.Services.CatalogServices.ProductServices;
using Newtonsoft.Json;

namespace MultiShop.WebUI.ViewComponents.ProductDetailViewComponent
{
    public class _ProductDetailFeatureComponentPartial: ViewComponent // Bu sınıf, ürün detayları için bir görüntü bileşeni sağlar
    {
        //private readonly IHttpClientFactory _httpClientFactory;

        //public _ProductDetailFeatureComponentPartial(IHttpClientFactory httpClientFactory)
        //{
        //    _httpClientFactory = httpClientFactory;
        //}
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public _ProductDetailFeatureComponentPartial(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        public async Task< IViewComponentResult> InvokeAsync(string id)
        {
            // Burada, ürün detayları için gerekli verileri alabilir ve görüntüleyebilirsiniz.
            // Örneğin, bir model veya veri kaynağı kullanabilirsiniz.
            // Şu anda sadece örnek bir görünüm döndürüyoruz.
            //var client = _httpClientFactory.CreateClient(); // IHttpClientFactory kullanarak HttpClient oluşturulur.
            //var responseMessage = await client.GetAsync($"https://localhost:1002/api/Products/{id}"); // API'den kategori verisi alınır.
            //if (responseMessage.IsSuccessStatusCode) // Eğer istek başarılıysa
            //{
            //    var jsonData = await responseMessage.Content.ReadAsStringAsync(); // JSON verisi okunur.
            //    var values = JsonConvert.DeserializeObject<UpdateProductDTO>(jsonData); // JSON verisi DTO nesnesine dönüştürülür.
            //    return View(values); // Dönüştürülen DTO nesnesi view'e gönderilir.
            //}
            //return View(); // Başarısız ise aynı view döndürülür.
            //var values=await _productService.GetByIDProductAsync(id); // Ürün verisi, IProductService kullanılarak alınır.
            //return View(values); // Alınan ürün verisi view'e gönderilir.
            // Ürünü getir
            UpdateProductDTO product = await _productService.GetByIDProductAsync(id);

            // Kategori adını bul (isteğe bağlı, hataya düşmesin diye try)
            string categoryName = "";
            try
            {
                var categories = await _categoryService.GetAllCategoryAsync();
                categoryName = categories.FirstOrDefault(c => c.CategoryID == product.CategoryID)?.CategoryName ?? "";
            }
            catch { /* yutuyoruz, kritik değil */ }

            // Giyim/ayakkabı vb. ise varyantları göster
            var fashionKeywords = new[] { "giyim", "ayakkabı", "tekstil", "elbise", "t-shirt", "pantolon", "kazak" };
            bool showVariantOptions = !string.IsNullOrWhiteSpace(categoryName)
                                      && fashionKeywords.Any(k => categoryName.ToLower().Contains(k));

            ViewBag.ShowVariantOptions = showVariantOptions;
            ViewBag.CategoryName = categoryName; // istersen view’da da kullanırsın

            return View(product); // Default.cshtml (model: UpdateProductDTO)
        }
    }
    }

