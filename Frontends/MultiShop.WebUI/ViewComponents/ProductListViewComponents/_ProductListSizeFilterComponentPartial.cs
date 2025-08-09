using Microsoft.AspNetCore.Mvc;

namespace MultiShop.WebUI.ViewComponents.ProductListViewComponents
{
    public class _ProductListSizeFilterComponentPartial: ViewComponent // bu metot parçası, boyut filtresi için kullanılır
    {
        public IViewComponentResult Invoke() // Invoke metodu, bu view component çağrıldığında çalışır
        {
            return View(); // View() metodu, ilgili view dosyasını döndürür
        }
    }
}
