using Microsoft.AspNetCore.Mvc;

namespace MultiShop.WebUI.ViewComponents.ShoppingCartViewComponents
{
    public class _ShoppingCartProductListComponentPartial: ViewComponent //bu sınıf ViewComponent sınıfından türetilmiştir
    {
        public IViewComponentResult Invoke() //Invoke metodu, ViewComponent'ın çağrıldığında ne yapacağını belirler
        {
            return View(); //View() metodu, varsayılan olarak "_ShoppingCartProductListComponentPartial.cshtml" dosyasını render eder
        }
    }
}
