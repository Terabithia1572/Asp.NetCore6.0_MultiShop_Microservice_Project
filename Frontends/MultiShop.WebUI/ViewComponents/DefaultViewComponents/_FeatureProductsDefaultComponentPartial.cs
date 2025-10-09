using Microsoft.AspNetCore.Mvc;
using MultiShop.DTOLayer.CatalogDTOs.ProductDTOs;
using MultiShop.WebUI.Services.CatalogServices.ProductServices;
using Newtonsoft.Json;

namespace MultiShop.WebUI.ViewComponents.DefaultViewComponents
{
    public class _FeatureProductsDefaultComponentPartial : ViewComponent
    {
        //private readonly IHttpClientFactory _httpClientFactory;

        //public _FeatureProductsDefaultComponentPartial(IHttpClientFactory httpClientFactory)
        //{
        //    _httpClientFactory = httpClientFactory;
        //}

        private readonly IProductService _productService; // Ürün verilerine erişim sağlayan servis (Dependency Injection ile gelir)

        public _FeatureProductsDefaultComponentPartial(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            //var client = _httpClientFactory.CreateClient(); // IHttpClientFactory kullanarak HttpClient oluşturulur.
            //var responseMessage = await client.GetAsync("https://localhost:1002/api/Products"); // API'den ürün verilerini almak için GET isteği yapılır.
            //if (responseMessage.IsSuccessStatusCode) // Eğer istek başarılıysa
            //{
            //    var jsonData = await responseMessage.Content.ReadAsStringAsync(); // JSON verisi okunur.
            //    var values = JsonConvert.DeserializeObject<List<ResultProductDTO>>(jsonData); // JSON verisi dinamik bir listeye dönüştürülür.
            //    return View(values); // Dönüştürülen liste view'e gönderilir.
            //}
            //return View(); // Eğer istek başarısızsa, boş bir view döndürülür.

            // 🟢 Burada hangi veriyi göstermek istiyorsak onu seçiyoruz:
            // GetAllProductAsync() → sadece ürünleri getirir
            // GetAllProductWithDiscountAsync() → ürün + indirim oranlarını getirir

            // ✅ Yeni metod: indirimli ürünlerle birlikte tüm ürünleri getirir
            var values = await _productService.GetAllProductAsync(); // 🔥
            if (values == null || !values.Any())
            {
                // Fallback (istersen)
                // var fallback = await _productService.GetAllProductAsync();
                // return View(fallback);
                return View(values);
            }
            return View(values);
        }
    }
}
