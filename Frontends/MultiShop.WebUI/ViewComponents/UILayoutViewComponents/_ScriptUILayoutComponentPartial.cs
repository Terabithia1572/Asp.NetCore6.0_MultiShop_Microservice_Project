using Microsoft.AspNetCore.Mvc;

namespace MultiShop.WebUI.ViewComponents.UILayoutViewComponents
{
    public class _ScriptUILayoutComponentPartial: ViewComponent // ViewComponent sınıfından türetilir
    {
        public IViewComponentResult Invoke() // bu metod, ViewComponent çağrıldığında çalışır
        {
            return View(); // View() metodu, varsayılan olarak "_ScriptUILayoutComponentPartial.cshtml" dosyasını arar ve render eder
        }
    }
}
