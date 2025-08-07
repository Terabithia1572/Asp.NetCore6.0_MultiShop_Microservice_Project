using Microsoft.AspNetCore.Mvc;

namespace MultiShop.WebUI.ViewComponents.DefaultViewComponents
{
    public class _CarouselDefaultComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke() //bu metod, bu view component çağrıldığında çalışır
        {
            return View(); // View() metodu, bu view component için varsayılan view'ı döndürür.
        }
    }
}
