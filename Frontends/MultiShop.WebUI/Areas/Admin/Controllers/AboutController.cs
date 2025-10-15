using Microsoft.AspNetCore.Mvc;
using MultiShop.DTOLayer.CatalogDTOs.AboutDTOs;
using MultiShop.WebUI.Services.CatalogServices.AboutServices;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]")]
    public class AboutController : Controller
    {
        private readonly IAboutService _aboutService;

        public AboutController(IAboutService aboutService)
        {
            _aboutService = aboutService;
        }

        // 🔹 Listeleme
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var aboutList = await _aboutService.GetAllAboutAsync();
            return View(aboutList);
        }

        // 🔹 Tam Sayfa Ekleme (GET)
        [HttpGet]
        public IActionResult CreateAbout()
        {
            return View();
        }

        // 🔹 Tam Sayfa Ekleme (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAbout(CreateAboutDTO dto)
        {
            var all = await _aboutService.GetAllAboutAsync();
            if (all.Any())
            {
                TempData["ErrorMessage"] = "Hakkımda kartı yalnızca bir adet olabilir. Lütfen önce mevcut olanı silin.";
                return RedirectToAction("Index");
            }

            await _aboutService.CreateAboutAsync(dto);
            TempData["SuccessMessage"] = "Yeni hakkımda bilgisi başarıyla eklendi.";
            return RedirectToAction("Index");
        }

        // 🔹 AJAX Ekleme (Hızlı Modal)
        [HttpPost]
        public async Task<IActionResult> CreateAboutAjax([FromForm] CreateAboutDTO dto)
        {
            try
            {
                var all = await _aboutService.GetAllAboutAsync();
                if (all.Any())
                {
                    return Json(new
                    {
                        success = false,
                        message = "Hakkımda kartı yalnızca bir adet olabilir. Lütfen önce mevcut olanı silin."
                    });
                }

                await _aboutService.CreateAboutAsync(dto);
                return Json(new
                {
                    success = true,
                    message = "Yeni hakkımda bilgisi başarıyla eklendi."
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = "Hata: " + ex.Message
                });
            }
        }

        // 🔹 Güncelleme (GET)
        [HttpGet("{id}")]
        public async Task<IActionResult> UpdateAbout(string id)
        {
            var value = await _aboutService.GetByIDAboutAsync(id);
            if (value == null)
            {
                TempData["ErrorMessage"] = "Hakkımda bilgisi bulunamadı.";
                return RedirectToAction("Index");
            }
            return View(value);
        }

        // 🔹 Güncelleme (POST)
        [HttpPost("{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateAbout(UpdateAboutDTO dto)
        {
            await _aboutService.UpdateAboutAsync(dto);
            TempData["SuccessMessage"] = "Hakkımda bilgisi başarıyla güncellendi.";
            return RedirectToAction("Index");
        }

        // 🔹 Silme
        [HttpPost("{id}")]
        public async Task<IActionResult> DeleteAbout(string id)
        {
            await _aboutService.DeleteAboutAsync(id);
            TempData["SuccessMessage"] = "Hakkımda bilgisi silindi.";
            return RedirectToAction("Index");
        }

        // 🔹 AJAX Silme
        [HttpGet("{id}")]
        public async Task<IActionResult> DeleteAboutAjax(string id)
        {
            try
            {
                await _aboutService.DeleteAboutAsync(id);
                return Json(new { success = true, message = "Kayıt başarıyla silindi." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Silme sırasında hata: " + ex.Message });
            }
        }
    }
}
