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

        // 🔸 Listeleme
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var discounts = await _productDiscountService.GetAllProductDiscountAsync();
            var products = await _productService.GetAllProductAsync();

            ViewBag.Products = products.Select(p => new
            {
                p.ProductID,
                p.ProductName
            }).ToList();

            // Aktiflik durumlarını doğru hesaplayalım
            var now = DateTime.Now;
            foreach (var item in discounts)
            {
                item.IsActive = item.IsActive && item.StartDate <= now && item.EndDate >= now;
            }

            return View(discounts);
        }

        // 🔸 Ekleme (GET)
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

        // 🔸 Ekleme (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddProductDiscount(CreateProductDiscountDTO dto)
        {
            var allDiscounts = await _productDiscountService.GetAllProductDiscountAsync();

            // 🎯 Aynı ürün için zaten aktif veya geçerli bir indirim varsa engelle
            bool alreadyExists = allDiscounts.Any(x =>
                x.ProductID == dto.ProductID &&
                x.EndDate >= DateTime.Now);

            if (alreadyExists)
            {
                TempData["DuplicateError"] = "Bu ürün için zaten bir indirim tanımlanmış. Lütfen önce mevcut indirimi silin.";
                return RedirectToAction("Index");
            }

            // 🎯 Tarih kontrolü: tarih aralığı doğru mu
            if (dto.StartDate > dto.EndDate)
            {
                TempData["DateError"] = "Bitiş tarihi, başlangıç tarihinden önce olamaz!";
                return RedirectToAction("Index");
            }

            // 🎯 Aktiflik durumunu otomatik belirle
            var now = DateTime.Now;
            dto.IsActive = dto.StartDate <= now && dto.EndDate >= now;

            await _productDiscountService.CreateProductDiscountAsync(dto);
            TempData["SuccessMessage"] = "İndirim başarıyla eklendi!";
            return RedirectToAction("Index");
        }

        // 🔸 Silme
        [HttpPost("{id}")]
        public async Task<IActionResult> DeleteProductDiscount(string id)
        {
            await _productDiscountService.DeleteProductDiscountAsync(id);
            TempData["SuccessMessage"] = "İndirim silindi.";
            return RedirectToAction("Index");
        }

        // 🔸 Güncelleme (GET)
        [HttpGet("{id}")]
        public async Task<IActionResult> UpdateProductDiscount(string id)
        {
            var value = await _productDiscountService.GetByIdProductDiscountAsync(id);
            var dto = new UpdateProductDiscountDTO
            {
                ProductDiscountID = value.ProductDiscountID,
                ProductID = value.ProductID,
                DiscountRate = value.DiscountRate,
                StartDate = value.StartDate,
                EndDate = value.EndDate,
                IsActive = value.IsActive
            };

            var products = await _productService.GetAllProductAsync();
            ViewBag.Products = products.Select(p => new
            {
                p.ProductID,
                p.ProductName
            }).ToList();

            return View(dto);
        }

        // 🔸 Güncelleme (POST)
        [HttpPost("{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateProductDiscount(UpdateProductDiscountDTO dto)
        {
            // 🎯 Aktiflik güncelle
            var now = DateTime.Now;
            dto.IsActive = dto.StartDate <= now && dto.EndDate >= now;

            await _productDiscountService.UpdateProductDiscountAsync(dto);
            TempData["SuccessMessage"] = "İndirim güncellendi!";
            return RedirectToAction("Index");
        }
    }
}
