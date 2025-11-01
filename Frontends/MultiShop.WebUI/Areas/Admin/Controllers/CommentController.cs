using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DTOLayer.CommentDTOs;
using MultiShop.WebUI.Services.CatalogServices.ProductServices;
using MultiShop.WebUI.Services.CommentServices;
using MultiShop.WebUI.Services.Interfaces;
using MultiShop.WebUI.Services.UserIdentityServices;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [AllowAnonymous]
    [Area("Admin")]
    [Route("Admin/[controller]/[action]")]
    public class CommentController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IUserService _userService;
        private readonly ICommentService _commentService;
        private readonly IProductService _productService;

        public CommentController(IHttpClientFactory httpClientFactory, IUserService userService, ICommentService commentService, IProductService productService)
        {
            _httpClientFactory = httpClientFactory;
            _userService = userService;
            _commentService = commentService;
            _productService = productService;
        }

        // 🟨 ORJİNAL KODLAR (tamamı korundu)
        public async Task<IActionResult> Index()
        {
            ViewBag.v1 = "Ana Sayfa";
            ViewBag.v2 = "Yorumlar";
            ViewBag.v3 = "Yorum Listesi";
            ViewBag.v4 = "Yorum İşlemleri";

            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7297/api/Comments");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultCommentDTO>>(jsonData);
                return View(values);
            }
            return View();
        }

        [HttpGet]
        public IActionResult CreateComment()
        {
            ViewBag.v1 = "Ana Sayfa";
            ViewBag.v2 = "Yorumlar";
            ViewBag.v3 = "Yeni Yorum Ekleme";
            ViewBag.v4 = "Yorum İşlemleri";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateComment(CreateCommentDTO createCommentDTO)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createCommentDTO);
            StringContent content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7297/api/Comments", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Comment", new { area = "Admin" });
            }
            return View();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> DeleteComment(string id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"https://localhost:7297/api/Comments?id=" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Comment", new { area = "Admin" });
            }
            return View();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> UpdateComment(string id)
        {
            ViewBag.v1 = "Ana Sayfa";
            ViewBag.v2 = "Yorumlar";
            ViewBag.v3 = "Yorum Güncelleme";
            ViewBag.v4 = "Yorum İşlemleri";
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7297/api/Comments/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdateCommentDTO>(jsonData);
                return View(values);
            }
            return View();
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> UpdateComment(UpdateCommentDTO updateCommentDTO)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateCommentDTO);
            StringContent content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("https://localhost:7297/api/Comments/", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Comment", new { area = "Admin" });
            }
            return View();
        }

        // 🟩 YENİ KODLAR — GİRİŞ YAPAN KULLANICININ KENDİ YORUMLARINI GÖRMESİ
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> MyComments()
        {
            var user = await _userService.GetUserInfo();
            if (user == null)
                return RedirectToAction("Index", "Login");

            var comments = await _commentService.GetCommentsByUserIdAsync(user.ID);

            // Aynı ürüne iki yorum varsa tekrar tekrar çağırmamak için mini cache
            var cache = new Dictionary<string, (string Name, string Image)>();

            foreach (var c in comments)
            {
                if (string.IsNullOrWhiteSpace(c.ProductID))
                    continue;

                if (!cache.TryGetValue(c.ProductID, out var prod))
                {
                    try
                    {
                        // Senin ProductService’in
                        var p = await _productService.GetByIDProductAsync(c.ProductID);
                        if (p != null)
                        {
                            prod = (p.ProductName ?? "Ürün", p.ProductImageURL ?? "/img/no-image.png");
                            cache[c.ProductID] = prod;
                        }
                    }
                    catch
                    {
                        prod = ("Ürün", "/img/no-image.png");
                    }
                }

                c.ProductName = prod.Name;
                c.ProductImageURL = prod.Image;
            }

            ViewBag.v1 = "Profilim";
            ViewBag.v2 = "Yorumlarım";
            ViewBag.v3 = "Yaptığım Yorumlar";
            ViewBag.v4 = "Kullanıcı Paneli";

            return View("MyComments", comments);
        }

        // 🟩 KULLANICININ YORUMUNU DÜZENLEME
        [Authorize]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> EditMyComment(int id)
        {
            var c = await _commentService.GetCommentByIdAsync(id);
            if (c == null) return NotFound();

            var dto = new UpdateCommentDTO
            {
                UserCommentID = c.UserCommentID,
                UserCommentDetail = c.UserCommentDetail,
                UserCommentRating = c.UserCommentRating,
                ProductID = c.ProductID,
                UserCommentNameSurname = c.UserCommentNameSurname,
                UserCommentEmail = c.UserCommentEmail,
                UserCommentImageURL = c.UserCommentImageURL,
                UserCommentCreatedDate = c.UserCommentCreatedDate, // 🔹 eklendi
                UserCommentStatus = c.UserCommentStatus            // 🔹 eklendi
            };

            return Ok(dto); // modal JSON besleyecek
        }

        // 🔹 Düzenleme (POST - AJAX JSON)
        [Authorize]
        [HttpPost("{id:int}")]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> EditMyComment([FromBody] UpdateCommentDTO dto)
        {
            await _commentService.UpdateCommentAsync(dto);
            return Ok(new { success = true });
        }

        // 🔹 Silme (POST - AJAX)
        [Authorize]
        [HttpPost("{id:int}")]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> DeleteMyComment(int id)
        {
            await _commentService.DeleteCommentAsync(id.ToString());
            return Ok(new { success = true });
        }
    }
}
