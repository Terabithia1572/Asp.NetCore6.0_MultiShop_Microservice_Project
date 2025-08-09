using Microsoft.AspNetCore.Mvc;

namespace MultiShop.WebUI.ViewComponents.ShoppingCartViewComponents
{
    public class _ShoppingCartDiscountCouponComponentPartial:ViewComponent //bu sınıf ViewComponent sınıfından türetilmiştir
    {
        public IViewComponentResult Invoke() //Invoke metodu, ViewComponent'ın çağrıldığında ne yapacağını belirler
        {
            return View(); //View() metodu, varsayılan olarak "_ShoppingCartDiscountCouponComponentPartial.cshtml" dosyasını render eder
        }
    }
   
}
