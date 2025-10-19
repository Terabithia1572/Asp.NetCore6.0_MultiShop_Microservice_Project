using Microsoft.AspNetCore.Mvc;

namespace MultiShop.WebUI.ViewComponents.ProductListViewComponents
{
    public class _ProductListGetProductCountComponentPartial: ViewComponent // bu metot parçası, ürün sayısını göstermek için kullanılır
    {
        public IViewComponentResult Invoke(int productCount) // Invoke metodu, bu view component çağrıldığında çalışır
        {
            //ViewBag.ProductCount = productCount; // ViewBag ile ürün sayısını view'a aktarır
            return View(); // View() metodu, ilgili view dosyasını döndürür
        }
    }
}
