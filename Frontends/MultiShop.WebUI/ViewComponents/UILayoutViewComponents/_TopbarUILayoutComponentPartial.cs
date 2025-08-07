using Microsoft.AspNetCore.Mvc;

namespace MultiShop.WebUI.ViewComponents.UILayoutViewComponents
{
    public class _TopbarUILayoutComponentPartial: ViewComponent // ViewComponent sınıfından türetilen bir sınıf
    {
        public IViewComponentResult Invoke() //bu metod, bu view component çağrıldığında çalışır
        {
            return View(); // View() metodu, bu view component için varsayılan view'ı döndürür.
        }
    }
}
