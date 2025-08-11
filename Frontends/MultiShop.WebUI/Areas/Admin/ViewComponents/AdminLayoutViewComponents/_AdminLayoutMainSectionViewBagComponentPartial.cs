using Microsoft.AspNetCore.Mvc;

namespace MultiShop.WebUI.Areas.Admin.ViewComponents.AdminLayoutViewComponents
{
    public class _AdminLayoutMainSectionViewBagComponentPartial:ViewComponent // ViewComponent sınıfından türetilir
    {
        public IViewComponentResult Invoke()
        {
            // ViewComponent içinde kullanılacak verileri hazırlayabilirsiniz.
            // Örneğin, başlık, meta etiketleri vb. gibi.
            // ViewComponent'ı render etmek için bir view döndürüyoruz.
            return View();
        }
    }
}
