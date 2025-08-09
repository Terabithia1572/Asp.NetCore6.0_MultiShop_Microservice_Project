using Microsoft.AspNetCore.Mvc;

namespace MultiShop.WebUI.ViewComponents.ProductDetailViewComponent
{
    public class _ProductDetailFeatureComponentPartial: ViewComponent // Bu sınıf, ürün detayları için bir görüntü bileşeni sağlar
    {
        public IViewComponentResult Invoke()
        {
            // Burada, ürün detayları için gerekli verileri alabilir ve görüntüleyebilirsiniz.
            // Örneğin, bir model veya veri kaynağı kullanabilirsiniz.
            // Şu anda sadece örnek bir görünüm döndürüyoruz.
            return View();
        }
    }
}
