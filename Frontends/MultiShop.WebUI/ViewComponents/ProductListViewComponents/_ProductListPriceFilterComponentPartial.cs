using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Services.CatalogServices.ProductServices;
using MultiShop.DTOLayer.CatalogDTOs.ProductDTOs;

namespace MultiShop.WebUI.ViewComponents.ProductListViewComponents
{
    public class _ProductListPriceFilterComponentPartial : ViewComponent
    {
        private readonly IProductService _productService;

        public _ProductListPriceFilterComponentPartial(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string? categoryId = null)
        {
            List<ResultProductWithCategoryDTO> products;

            // 🔹 Eğer kategori varsa sadece o kategoriye ait ürünleri getir
            if (!string.IsNullOrWhiteSpace(categoryId))
            {
                products = await _productService.GetProductsWithByCategoryByCategoryIDAsync(categoryId);
            }
            else
            {
                // 🔹 Tüm ürünleri kategorileriyle birlikte getir
                products = await _productService.GetProductsWithCategoryAsync();
            }

            if (products == null || !products.Any())
            {
                ViewBag.TotalCount = 0;
                return View(new List<dynamic>());
            }

            // 🔹 Fiyat aralıkları (₺ bazlı, daha gerçekçi)
            var ranges = new List<(decimal Min, decimal Max, string Label)>
            {
                (0, 5000, "₺0 - ₺5.000"),
                (5000, 10000, "₺5.000 - ₺10.000"),
                (10000, 20000, "₺10.000 - ₺20.000"),
                (20000, 50000, "₺20.000 - ₺50.000"),
                (50000, decimal.MaxValue, "₺50.000+")
            };

            // 🔹 Her aralık için ürün sayısı hesapla
            var rangeCounts = ranges
                .Select(r => new
                {
                    r.Label,
                    Count = products.Count(p => p.ProductPrice >= r.Min && p.ProductPrice < r.Max)
                })
                .ToList();

            ViewBag.TotalCount = products.Count();
            return View(rangeCounts);
        }
    }
}
