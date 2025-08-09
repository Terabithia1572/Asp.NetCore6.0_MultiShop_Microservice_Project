using Microsoft.AspNetCore.Mvc;

namespace MultiShop.WebUI.ViewComponents.ProductDetailViewComponent
{
    public class _ProductDetailReviewComponentPartial:ViewComponent //bu sınıf ViewComponent sınıfından türetilir 
    {
        public IViewComponentResult Invoke() // Invoke metodu, bu ViewComponent çağrıldığında çalışacak metottur.
        {
            return View(); // View() metodu, varsayılan olarak _ProductDetailReviewComponentPartial.cshtml dosyasını render eder.
        }
    }
   
}
