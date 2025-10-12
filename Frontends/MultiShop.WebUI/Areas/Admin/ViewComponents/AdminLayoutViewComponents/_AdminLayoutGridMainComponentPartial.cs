using Microsoft.AspNetCore.Mvc;

namespace MultiShop.WebUI.Areas.Admin.ViewComponents.AdminLayoutViewComponents
{
    public class _AdminLayoutGridMainComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            // ViewComponent içinde kullanılacak verileri hazırlayabilirsiniz.
            // Örneğin, menü öğeleri, kullanıcı bilgileri vb. gibi.
            // ViewComponent'ı render etmek için bir view döndürüyoruz.
            return View();
        }
    }
}
