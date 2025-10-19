using Microsoft.AspNetCore.Mvc;
using MultiShop.DTOLayer.CatalogDTOs.ProductDTOs;
using MultiShop.WebUI.Services.CatalogServices.CategoryServices;
using MultiShop.WebUI.Services.CatalogServices.ProductDiscountServices;
using MultiShop.WebUI.Services.CatalogServices.ProductServices;

namespace MultiShop.WebUI.ViewComponents.ProductListViewComponents
{
    public class _ProductListComponentPartial : ViewComponent
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IProductDiscountService _productDiscountService;

        public _ProductListComponentPartial(
            IProductService productService,
            ICategoryService categoryService,
            IProductDiscountService productDiscountService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _productDiscountService = productDiscountService;
        }

        // 🔹 ranges: "all" | "₺0 - ₺5000, ₺5000 - ₺10000" | "₺20000+"
        public async Task<IViewComponentResult> InvokeAsync(string id, string? ranges = "all")
        {
            // 1️⃣ Ürünleri kategoriye göre getir
            List<ResultProductWithCategoryDTO> products;
            if (!string.IsNullOrWhiteSpace(id))
                products = await _productService.GetProductsWithByCategoryByCategoryIDAsync(id);
            else
                products = await _productService.GetProductsWithCategoryAsync();

            // 2️⃣ Fiyat filtresi uygula
            if (!string.IsNullOrWhiteSpace(ranges) && ranges != "all")
            {
                var selectedRanges = ranges.Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(r => r.Replace("₺", "").Replace(".", "").Replace("+", "").Trim())
                    .ToList();

                var filtered = new List<ResultProductWithCategoryDTO>();

                foreach (var range in selectedRanges)
                {
                    if (range.Contains("-"))
                    {
                        var parts = range.Split('-', StringSplitOptions.RemoveEmptyEntries);
                        if (decimal.TryParse(parts[0].Trim(), out var min) &&
                            decimal.TryParse(parts[1].Trim(), out var max))
                        {
                            filtered.AddRange(products.Where(p => p.ProductPrice >= min && p.ProductPrice <= max));
                        }
                    }
                    else if (decimal.TryParse(range, out var minOnly))
                    {
                        filtered.AddRange(products.Where(p => p.ProductPrice >= minOnly));
                    }
                }

                // 🔹 Aynı ürün farklı aralıkta denk gelirse sadece birini al
                products = filtered.GroupBy(p => p.ProductID).Select(g => g.First()).ToList();
            }

            // 3️⃣ Kategorileri çek ve ViewBag’e ata
            var categories = await _categoryService.GetAllCategoryAsync();
            var categoryDict = categories.ToDictionary(c => c.CategoryID, c => c.CategoryName);

            // 4️⃣ Aktif indirimleri çek ve ViewBag’e ata
            var discounts = await _productDiscountService.GetAllProductDiscountAsync();
            var activeDiscounts = discounts
                .Where(d => d.IsActive && d.StartDate <= DateTime.Now && d.EndDate >= DateTime.Now)
                .ToDictionary(d => d.ProductID, d => d.DiscountRate);

            ViewBag.Categories = categoryDict;
            ViewBag.Discounts = activeDiscounts;

            // 5️⃣ Default.cshtml (ürün kartlarının olduğu view) döndürülür
            return View(products);
        }
    }
}
