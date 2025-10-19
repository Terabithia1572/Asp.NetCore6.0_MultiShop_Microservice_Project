using Microsoft.AspNetCore.Mvc;
using MultiShop.DTOLayer.CatalogDTOs.ProductDTOs;
using MultiShop.DTOLayer.CommentDTOs;
using MultiShop.WebUI.Services.CatalogServices.CategoryServices;
using MultiShop.WebUI.Services.CatalogServices.ProductServices;
using Newtonsoft.Json;
using System.Text;

namespace MultiShop.WebUI.Controllers
{
    public class ProductListController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;

        public ProductListController(IHttpClientFactory httpClientFactory, ICategoryService categoryService, IProductService productService)
        {
            _httpClientFactory = httpClientFactory;
            _categoryService = categoryService;
            _productService = productService;
        }
        public async Task< IActionResult> Index(string id)
        {
            ViewBag.directory1 = "Ana Sayfa";
            ViewBag.directory2 = "Ürünler";
            ViewBag.directory3 = "Ürün Listesi"; 
            ViewBag.id = id;
            var products = await _productService.GetProductsWithByCategoryByCategoryIDAsync(id);
            ViewBag.ProductCount = products?.Count() ?? 0;
            ViewBag.CategoryName = products?.FirstOrDefault()?.Category?.CategoryName ?? "Ürünler";

            // 🔹 Kategori bilgisi authentication istemeden çekilir
            var category = await _categoryService.GetByIDCategoryAsync(id);
            if (category != null)
                ViewBag.CategoryName = category.CategoryName;
            else
                ViewBag.CategoryName = "Kategori";

            return View(products);
        }
        

        public IActionResult ProductDetail(string id)
        {
            ViewBag.directory1 = "Ana Sayfa";
            ViewBag.directory2 = "Ürün Listesi";
            ViewBag.directory3 = "Ürün Detayları";
            ViewBag.x = id;
            return View();
        }
        [HttpGet]
        public PartialViewResult AddComment()
        {
            return PartialView();
        }
        [HttpPost]
        public async Task< IActionResult> AddComment(CreateCommentDTO createCommentDTO)
        {
            createCommentDTO.UserCommentImageURL = "test";
            createCommentDTO.UserCommentRating = 1;
            createCommentDTO.UserCommentCreatedDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            createCommentDTO.UserCommentStatus = false;
            createCommentDTO.ProductID = "68a60e7fc36ee1136596a4bd";
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createCommentDTO);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("http://localhost:7297/api/Comments", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Default");
            }
            return View();
        }
        // 🔥 GÜNCELLENMİŞ FİYAT FİLTRELEME
        [HttpGet]
        public IActionResult FilterByPrice(string ranges, string? categoryId)
        {
            // ❗ PartialView YOK, doğrudan ViewComponent çağırıyoruz
            // ranges: "all" | "₺1000 - ₺5000, ₺20000+"
            return ViewComponent("_ProductListComponentPartial", new { id = categoryId ?? "", ranges = ranges ?? "all" });
        }

    }


}

