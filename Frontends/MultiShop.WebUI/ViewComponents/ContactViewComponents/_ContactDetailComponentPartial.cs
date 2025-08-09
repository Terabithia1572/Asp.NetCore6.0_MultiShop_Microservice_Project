using Microsoft.AspNetCore.Mvc;

namespace MultiShop.WebUI.ViewComponents.ContactViewComponents
{
    public class _ContactDetailComponentPartial: ViewComponent //bu sınıf ViewComponent sınıfından türetilmiştir
    {
        public IViewComponentResult Invoke() //Invoke metodu, ViewComponent'ın çağrıldığında ne yapacağını belirler
        {
            return View(); //View() metodu, varsayılan olarak "_ContactDetailComponentPartial.cshtml" dosyasını render eder
        }
    }
}
