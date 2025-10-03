using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Services.StatisticServices.CatalogStatisticServices;
using MultiShop.WebUI.Services.StatisticServices.UserStatisticServices;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class StatisticController : Controller
    {
        private readonly ICatalogStatisticService _catalogStatisticService;
        private readonly IUserStatisticService _userStatisticService;

        public StatisticController(ICatalogStatisticService catalogStatisticService, IUserStatisticService userStatisticService)
        {
            _catalogStatisticService = catalogStatisticService;
            _userStatisticService = userStatisticService;
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
