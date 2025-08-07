using Microsoft.AspNetCore.Mvc;

namespace MultiShop.WebUI.ViewComponents.UILayoutViewComponents
{
    public class _NavbarUILayoutComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke() //bu metod, bu view component çağrıldığında çalışır
        {
            return View(); // View() metodu, bu view component için varsayılan view'ı döndürür.
        }
    }
}
