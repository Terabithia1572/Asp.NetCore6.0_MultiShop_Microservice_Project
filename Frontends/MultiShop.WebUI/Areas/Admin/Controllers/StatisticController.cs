using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Services.StatisticServices.CatalogStatisticServices;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class StatisticController : Controller
    {
        private readonly ICatalogStatisticService _catalogStatisticService;

        public StatisticController(ICatalogStatisticService catalogStatisticService)
        {
            _catalogStatisticService = catalogStatisticService;
        }

        public async Task< IActionResult> Index()
        {
            var getBrandCount = await _catalogStatisticService.GetBrandCount();
            ViewBag.getBrandCount = getBrandCount;
            var getCategoryCount = await _catalogStatisticService.GetCategoryCount();
            ViewBag.getCategoryCount = getCategoryCount;
            var getMaximumPriceProductName = await _catalogStatisticService.GetMaximumPriceProductName();
            ViewBag.getMaximumPriceProductName = getMaximumPriceProductName;
            var getMinimumPriceProductName = await _catalogStatisticService.GetMinimumPriceProductName();
            ViewBag.getMinimumPriceProductName = getMinimumPriceProductName;
            var getProduceCount = await _catalogStatisticService.GetProduceCount();
            ViewBag.getProduceCount = getProduceCount;
            var getProductAvgPrice = await _catalogStatisticService.GetProductAvgPrice();
            ViewBag.getProductAvgPrice = getProductAvgPrice;
            return View();
        }
    }
}
