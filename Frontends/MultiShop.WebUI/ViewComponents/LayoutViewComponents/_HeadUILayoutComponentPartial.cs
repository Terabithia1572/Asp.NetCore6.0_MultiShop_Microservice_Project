using Microsoft.AspNetCore.Mvc;

namespace MultiShop.WebUI.ViewComponents.LayoutViewComponents
{
    public class _HeadUILayoutComponentPartial: ViewComponent // Burada ViewComponent sınıfından türetiliyor
    {
        public IViewComponentResult Invoke() // Invoke metodu, bu ViewComponent'in nasıl çağrılacağını belirler
        {
            return View(); // View() metodu, ilgili View dosyasını döndürür
        }
    }
}
