using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DTOLayer.CommentDTOs;
using MultiShop.WebUI.Services.CatalogServices.CategoryServices;
using MultiShop.WebUI.Services.CatalogServices.ProductServices;
using MultiShop.WebUI.Services.CommentServices;
using Newtonsoft.Json;
using System.Text;

namespace MultiShop.WebUI.Controllers
{
    public class ProductListController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        private readonly ICommentService _commentService;

        public ProductListController(IHttpClientFactory httpClientFactory, ICategoryService categoryService, IProductService productService, ICommentService commentService)
        {
            _httpClientFactory = httpClientFactory;
            _categoryService = categoryService;
            _productService = productService;
            _commentService = commentService;
        }

        // ✅ Kategoriye göre ürün listesi
        public async Task<IActionResult> Index(string id)
        {
            ViewBag.directory1 = "Ana Sayfa";
            ViewBag.directory2 = "Ürünler";
            ViewBag.directory3 = "Ürün Listesi";
            ViewBag.id = id;

            var products = await _productService.GetProductsWithByCategoryByCategoryIDAsync(id);
            ViewBag.ProductCount = products?.Count() ?? 0;
            ViewBag.CategoryName = products?.FirstOrDefault()?.Category?.CategoryName ?? "Ürünler";

            var category = await _categoryService.GetByIDCategoryAsync(id);
            ViewBag.CategoryName = category?.CategoryName ?? "Kategori";

            return View(products);
        }

        // ✅ Ürün detay sayfası
        public IActionResult ProductDetail(string id)
        {
            ViewBag.directory1 = "Ana Sayfa";
            ViewBag.directory2 = "Ürün Listesi";
            ViewBag.directory3 = "Ürün Detayları";
            ViewBag.x = id;
            return View();
        }

        // ✅ Fiyat filtreleme
        [HttpGet]
        public IActionResult FilterByPrice(string ranges, string? categoryId)
        {
            return ViewComponent("_ProductListComponentPartial", new { id = categoryId ?? "", ranges = ranges ?? "all" });
        }

        // ✅ AJAX ile yorum ekleme
        // ✅ AJAX ile yorum ekleme
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddComment([FromBody] CreateCommentDTO dto)
        {
            try
            {
                // 🔹 Kullanıcı bilgilerini alıyoruz (Authenticate olmuş kullanıcıdan)
                var claims = User.Claims.ToList();
                var firstName = claims.FirstOrDefault(c => c.Type == "name")?.Value ?? "Ziyaretçi";
                var lastName = claims.FirstOrDefault(c => c.Type == "surname")?.Value ?? "";
                var email = claims.FirstOrDefault(c => c.Type == "email")?.Value ?? "unknown@mail.com";
                // 🔹 Kullanıcı ID’sini alıyoruz
                var userId = claims.FirstOrDefault(c =>
                    c.Type == "sub" || c.Type == "nameidentifier" || c.Type == "userid"
                )?.Value;


                // 🔹 Profil fotoğrafı claim'i (farklı claim adlarını da kapsıyoruz)
                var image = claims.FirstOrDefault(c =>
                    c.Type == "profileImage" ||
                    c.Type == "profile_image" ||
                    c.Type == "profileimage"
                )?.Value ?? "/img/default-user.png";

                // 🔹 DTO'yu dolduruyoruz
                dto.UserId = userId;
                dto.UserCommentNameSurname = $"{firstName} {lastName}".Trim();
                dto.UserCommentEmail = email;
                dto.UserCommentImageURL = image;
                dto.UserCommentCreatedDate = DateTime.Now;
                dto.UserCommentStatus = true; // Onaylı olarak eklenebilir (ya da admin onayı beklenebilir)
                


                // 🔹 Servis üzerinden POST işlemi (baseAddress zaten Program.cs içinde)
                await _commentService.CreateCommentAsync(dto);

                // 🔹 Başarılı cevap (AJAX için JSON)
                return Json(new
                {
                    success = true,
                    message = "Yorum başarıyla eklendi.",
                    comment = new
                    {
                        name = dto.UserCommentNameSurname,
                        image = dto.UserCommentImageURL,
                        detail = dto.UserCommentDetail,
                        date = dto.UserCommentCreatedDate.ToString("dd.MM.yyyy"),
                        rating = dto.UserCommentRating
                    }
                });
            }
            catch (Exception ex)
            {
                // 🔹 Hata olduğunda kullanıcıya JSON döndür
                return Json(new
                {
                    success = false,
                    message = "Sunucu hatası: " + ex.Message
                });
            }
        }

        [AllowAnonymous]
        public async Task<IActionResult> All()
        {
            var categories = await _categoryService.GetAllCategoryAsync();
            ViewBag.Categories = categories;
            ViewBag.directory1 = "Ana Sayfa";
            ViewBag.directory2 = "Ürünler";
            ViewBag.directory3 = "Tüm Ürünler";
            var discountList = await _productService.GetAllProductWithDiscountAsync();
            ViewBag.Discounts = discountList
                .Where(d => d.DiscountRate.HasValue)
                .ToDictionary(d => d.ProductID, d => d.DiscountRate!.Value);
            // Catalog → /api/products/ProductListWithCategoryAll (veya mevcut tüm+kategori metodun)
            var products = await _productService.GetAllProductsWithCategoryAsync(); // List<ResultProductWithCategoryDTO>

            return View("All", products); // 🔥 Bu view'e direkt model gönderiyoruz
        }
        [AllowAnonymous]
        public async Task<IActionResult> FilterProducts(string categoryId = "", string sort = "default")
        {
            // Tüm ürünleri çek
            var products = await _productService.GetAllProductsWithCategoryAsync();

            // Kategori filtresi
            if (!string.IsNullOrEmpty(categoryId))
                products = products.Where(p => p.Category?.CategoryID == categoryId).ToList();

            // Sıralama işlemi
            products = sort switch
            {
                "price-asc" => products.OrderBy(p => p.ProductPrice).ToList(),
                "price-desc" => products.OrderByDescending(p => p.ProductPrice).ToList(),
                "name-asc" => products.OrderBy(p => p.ProductName).ToList(),
                "name-desc" => products.OrderByDescending(p => p.ProductName).ToList(),
                _ => products
            };

            // İndirimleri ViewBag ile gönderelim (mevcut yapıyı koruyoruz)
            var discountList = await _productService.GetAllProductWithDiscountAsync();
            ViewBag.Discounts = discountList
                .Where(d => d.DiscountRate.HasValue)
                .ToDictionary(d => d.ProductID, d => d.DiscountRate!.Value);

            // Partial View döndür
            return PartialView("_ProductGridPartial", products);
        }



    }
}
