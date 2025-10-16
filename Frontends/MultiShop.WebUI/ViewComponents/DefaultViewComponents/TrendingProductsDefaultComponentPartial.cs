using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Services.CatalogServices.ProductServices;
using MultiShop.WebUI.Services.CatalogServices.CategoryServices;
using MultiShop.WebUI.Services.CatalogServices.ProductDiscountServices;

namespace MultiShop.WebUI.ViewComponents
{
    public class _TrendingProductsDefaultComponentPartial : ViewComponent
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IProductDiscountService _productDiscountService;

        public _TrendingProductsDefaultComponentPartial(
            IProductService productService,
            ICategoryService categoryService,
            IProductDiscountService productDiscountService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _productDiscountService = productDiscountService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            // ✅ 1. Ürünleri al
            var products = await _productService.GetAllProductAsync();

            // ✅ 2. Trend sıralama (örnek: son eklenen 8 ürün)
            var trending = products.OrderByDescending(p => p.ProductID).Take(8).ToList();

            // ✅ 3. Kategorileri çek
            var categories = await _categoryService.GetAllCategoryAsync();
            var categoryDict = categories.ToDictionary(c => c.CategoryID, c => c.CategoryName);

            // ✅ 4. ProductDiscount’ları çek (aktif olanlar)
            var productDiscounts = await _productDiscountService.GetAllProductDiscountAsync();
            var activeDiscounts = productDiscounts
                .Where(d => d.IsActive == true && d.StartDate <= DateTime.Now && d.EndDate >= DateTime.Now)
                .ToDictionary(d => d.ProductID, d => d.DiscountRate);

            // ✅ 5. ViewBag olarak View’a gönder
            ViewBag.Categories = categoryDict;
            ViewBag.Discounts = activeDiscounts;

            // ✅ 6. View’a listeyi döndür
            return View(trending);
        }
    }
}
