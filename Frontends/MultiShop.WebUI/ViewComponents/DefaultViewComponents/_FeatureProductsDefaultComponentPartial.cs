// 📁 MultiShop.WebUI/ViewComponents/DefaultViewComponents/_FeatureProductsDefaultComponentPartial.cs
using Microsoft.AspNetCore.Mvc;
using MultiShop.DTOLayer.CatalogDTOs.ProductDTOs;
using MultiShop.WebUI.Services.CatalogServices.ProductServices;

namespace MultiShop.WebUI.ViewComponents.DefaultViewComponents
{
    public class _FeatureProductsDefaultComponentPartial : ViewComponent
    {
        private readonly IProductService _productService; // Ürün verilerine erişim sağlayan servis (Dependency Injection ile gelir)

        public _FeatureProductsDefaultComponentPartial(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            // 🟢 Önce indirimli listeyi dene
            var discounted = await _productService.GetAllProductWithDiscountAsync();

            if (discounted != null && discounted.Any())
                return View(discounted); // View'in modeli ResultProductWithDiscountDTO listesi olacak

            // 🔁 Fallback: Hiç ürün dönmediyse standart listeye düş
            var plain = await _productService.GetAllProductAsync();
            // Dilersen burada plain -> discounted DTO'ya map edip tek view kullanabilirsin.
            return View(plain.Select(p => new ResultProductWithDiscountDTO
            {
                ProductID = p.ProductID,
                ProductName = p.ProductName,
                ProductPrice = p.ProductPrice,
                ProductImageURL = p.ProductImageURL,
                DiscountRate = null,
                DiscountedPrice = p.ProductPrice
            }).ToList());
        }
    }
}
