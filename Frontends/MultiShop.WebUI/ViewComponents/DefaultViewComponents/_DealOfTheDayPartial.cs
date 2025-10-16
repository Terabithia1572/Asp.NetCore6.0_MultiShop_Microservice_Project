using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Services.CatalogServices.ProductDiscountServices;

namespace MultiShop.WebUI.ViewComponents
{
    public class _DealOfTheDayPartial : ViewComponent
    {
        private readonly IProductDiscountService _discountService;

        public _DealOfTheDayPartial(IProductDiscountService discountService)
        {
            _discountService = discountService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var discounts = await _discountService.GetAllProductDiscountAsync();

            var bestDeal = discounts
                .Where(d => d.IsActive && d.StartDate <= DateTime.Now && d.EndDate >= DateTime.Now)
                .OrderByDescending(d => d.DiscountRate)
                .FirstOrDefault();

            return View(bestDeal);
        }
    }
}
