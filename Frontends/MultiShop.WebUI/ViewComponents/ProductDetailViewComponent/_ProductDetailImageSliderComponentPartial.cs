using Microsoft.AspNetCore.Mvc;
using MultiShop.DTOLayer.CatalogDTOs.ProductDTOs;
using MultiShop.DTOLayer.CatalogDTOs.ProductImageDTOs;
using Newtonsoft.Json;

namespace MultiShop.WebUI.ViewComponents.ProductDetailViewComponent
{
    public class _ProductDetailImageSliderComponentPartial: ViewComponent // Bu sınıf, ürün detayları için bir görüntü bileşeni sağlar
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _ProductDetailImageSliderComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task< IViewComponentResult> InvokeAsync(string id)
        {
            // Burada, ürün detayları için gerekli verileri alabilir ve görüntüleyebilirsiniz.
            // Örneğin, bir model veya veri kaynağı kullanabilirsiniz.
            // Şu anda sadece örnek bir görünüm döndürüyoruz.
            var client = _httpClientFactory.CreateClient(); // IHttpClientFactory kullanarak HttpClient oluşturulur.
            var responseMessage = await client.GetAsync($"https://localhost:1002/api/ProductImages/ProductImagesByProductID?id=/{id}"); // API'den kategori verisi alınır.
            if (responseMessage.IsSuccessStatusCode) // Eğer istek başarılıysa
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync(); // JSON verisi okunur.
                var values = JsonConvert.DeserializeObject<GetByIDProductImageDTO>(jsonData); // JSON verisi DTO nesnesine dönüştürülür.
                return View(values); // Dönüştürülen DTO nesnesi view'e gönderilir.
            }
            return View(); // Başarısız ise aynı view döndürülür.
        }
    }
}
