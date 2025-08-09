using Microsoft.AspNetCore.Mvc;

namespace MultiShop.WebUI.ViewComponents.ProductDetailViewComponent
{
    public class _ProductDetailDescriptionComponentPartial: ViewComponent // bu sınıf ViewComponent sınıfından türetilmiştir
    {
        public IViewComponentResult Invoke() // Invoke metodu, bu ViewComponent çağrıldığında çalışacak metottur
        {
            return View(); // View() metodu, varsayılan olarak _ProductDetailDescriptionComponentPartial.cshtml dosyasını render eder
        }
    }
}
