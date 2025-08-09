using Microsoft.AspNetCore.Mvc;

namespace MultiShop.WebUI.ViewComponents.ProductListViewComponents
{
    public class _ProductListComponentPartial: ViewComponent // bu sınıf ViewComponent sınıfından türetilir 
    {
        public IViewComponentResult Invoke() // Invoke metodu, bu ViewComponent çağrıldığında çalışacak metottur.
        {
            return View(); // View() metodu, varsayılan olarak _ProductListComponentPartial.cshtml dosyasını render eder.
        }
    }
}
