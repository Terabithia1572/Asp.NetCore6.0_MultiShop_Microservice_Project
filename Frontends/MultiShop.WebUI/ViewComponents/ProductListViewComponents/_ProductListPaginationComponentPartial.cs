using Microsoft.AspNetCore.Mvc;

namespace MultiShop.WebUI.ViewComponents.ProductListViewComponents
{
    public class _ProductListPaginationComponentPartial: ViewComponent // Bu sınıf ViewComponent sınıfından türetilir
    {
        public IViewComponentResult Invoke(int currentPage, int totalPages)
        {
            // ViewComponent, sayfalama bilgilerini alır ve ilgili view dosyasını render eder.
            ViewBag.CurrentPage = currentPage; // Geçerli sayfa numarasını ViewBag'e ekler
            ViewBag.TotalPages = totalPages; // Toplam sayfa sayısını ViewBag'e ekler
            return View(); // Bu, varsayılan olarak _ProductListPaginationComponentPartial.cshtml dosyasını render eder.
        }
    }
}
