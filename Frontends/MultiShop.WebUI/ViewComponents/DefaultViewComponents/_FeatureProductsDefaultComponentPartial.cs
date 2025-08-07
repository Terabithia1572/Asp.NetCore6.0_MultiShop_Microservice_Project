using Microsoft.AspNetCore.Mvc;

namespace MultiShop.WebUI.ViewComponents.DefaultViewComponents
{
    public class _FeatureProductsDefaultComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke() //bu metot, bu view component çağrıldığında çalışır.
        {
            return View(); // View() metodu, bu view component için varsayılan view dosyasını döndürür.
        }
    }
}
