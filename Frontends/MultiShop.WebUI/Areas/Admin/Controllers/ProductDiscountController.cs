using Microsoft.AspNetCore.Mvc;
using MultiShop.DTOLayer.CatalogDTOs.ProductDiscountDTOs;
using MultiShop.WebUI.Services.CatalogServices.ProductDiscountServices;
using MultiShop.WebUI.Services.CatalogServices.ProductServices;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]")]
    public class ProductDiscountController : Controller
    {
        private readonly IProductDiscountService _productDiscountService;
        private readonly IProductService _productService;

        public ProductDiscountController(IProductDiscountService productDiscountService, IProductService productService)
        {
            _productDiscountService = productDiscountService;
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var values = await _productDiscountService.GetAllProductDiscountAsync();
            return View(values);
        }

        [HttpGet]
        public async Task<IActionResult> AddProductDiscount()
        {
            var products = await _productService.GetAllProductAsync();
            ViewBag.Products = products.Select(p => new
            {
                p.ProductID,
                p.ProductName
            }).ToList();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddProductDiscount(CreateProductDiscountDTO dto)
        {
            await _productDiscountService.CreateProductDiscountAsync(dto);
            return RedirectToAction("Index", "ProductDiscount", new { area = "Admin" });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> DeleteProductDiscount(string id)
        {
            await _productDiscountService.DeleteProductDiscountAsync(id);
            return RedirectToAction("Index", "ProductDiscount", new { area = "Admin" });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> UpdateProductDiscount(string id)
        {
            // 🔹 Servisten veri çek
            var value = await _productDiscountService.GetByIdProductDiscountAsync(id);

            // 🔹 Update DTO’ya dönüştür
            var dto = new UpdateProductDiscountDTO
            {
                ProductDiscountID = value.ProductDiscountID,
                ProductID = value.ProductID,
                DiscountRate = value.DiscountRate,
                StartDate = value.StartDate,
                EndDate = value.EndDate,
                IsActive = value.IsActive
            };

            // 🔹 Ürün dropdown’ı
            var products = await _productService.GetAllProductAsync();
            ViewBag.Products = products.Select(p => new
            {
                p.ProductID,
                p.ProductName
            }).ToList();

            return View(dto);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> UpdateProductDiscount(UpdateProductDiscountDTO dto)
        {
            await _productDiscountService.UpdateProductDiscountAsync(dto);
            return RedirectToAction("Index", "ProductDiscount", new { area = "Admin" });
        }
    }
}
