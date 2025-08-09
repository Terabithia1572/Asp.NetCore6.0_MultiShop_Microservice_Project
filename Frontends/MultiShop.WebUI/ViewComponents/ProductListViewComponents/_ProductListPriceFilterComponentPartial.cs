using Microsoft.AspNetCore.Mvc;

namespace MultiShop.WebUI.ViewComponents.ProductListViewComponents
{
    public class _ProductListPriceFilterComponentPartial: ViewComponent // bu metot parçası, fiyat filtresi için kullanılır
    {
        public IViewComponentResult Invoke() // Invoke metodu, bu view component çağrıldığında çalışır
        {
           
            return View(); // View() metodu, ilgili view dosyasını döndürür
        }
    }
}
