using Microsoft.AspNetCore.Mvc;
using MultiShop.DTOLayer.CatalogDTOs.ProductDTOs;
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
        private readonly IProductService _productService; // IProductService arayüzü, ürünle ilgili işlemleri sağlar.

        public _ProductDetailFeatureComponentPartial(IProductService productService) // Yapıcı metod, IProductService bağımlılığını alır.
        { 
            _productService = productService; // IProductService bağımlılığı, yapıcı metod aracılığıyla enjekte edilir.
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
            var values=await _productService.GetByIDProductAsync(id); // Ürün verisi, IProductService kullanılarak alınır.
            return View(values); // Alınan ürün verisi view'e gönderilir.
        }
    }
}
