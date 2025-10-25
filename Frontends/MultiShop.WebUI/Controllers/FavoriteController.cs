using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DTOLayer.FavoriteDTOs;
using MultiShop.WebUI.Services.FavoriteServices;

namespace MultiShop.WebUI.Controllers
{
    [Authorize]
    public class FavoriteController : Controller
    {
        private readonly IFavoriteService _favoriteService;

        public FavoriteController(IFavoriteService favoriteService)
        {
            _favoriteService = favoriteService;
        }

        // 🔹 Mini paneli ilk yüklemek / tazelemek için (PartialView döner)
        [HttpGet]
        public async Task<IActionResult> GetMiniFavorite()
        {
            var userId = User?.Identity?.Name ?? string.Empty;
            var favorites = string.IsNullOrEmpty(userId)
                ? new List<ResultFavoriteDTO>()
                : await _favoriteService.GetUserFavoritesAsync(userId);

            return PartialView(@"~/Views/Shared/Components/_MiniFavoritePartialView/_MiniFavoritePartialView.cshtml", favorites);
        }

        // 🔹 Navbar’daki favori adetini almak için
        [HttpGet]
        public async Task<IActionResult> GetFavCount()
        {
            var userId = User?.Identity?.Name ?? string.Empty;
            if (string.IsNullOrEmpty(userId))
                return Json(0);

            var favorites = await _favoriteService.GetUserFavoritesAsync(userId);
            return Json(favorites?.Count ?? 0);
        }

        // 🔹 Ürünü favorilere ekler, sonra mini panel partial’ı geri yollar
        [HttpPost]
        public async Task<IActionResult> AddFavoriteAjax([FromBody] CreateFavoriteDTO dto)
        {
            if (dto == null) return BadRequest();

            dto.UserID = User?.Identity?.Name;   // ❗ zorunlu
            if (string.IsNullOrEmpty(dto.UserID)) return Unauthorized();

            await _favoriteService.AddFavoriteAsync(dto);

            var favorites = await _favoriteService.GetUserFavoritesAsync(dto.UserID);
            return PartialView(@"~/Views/Shared/Components/_MiniFavoritePartialView/_MiniFavoritePartialView.cshtml", favorites);
        }

        // 🔹 Favoriden kaldır (id: FavoriteID). Sonrasında mini panel partial’ı geri yollar
        [HttpPost]
        public async Task<IActionResult> RemoveFavoriteAjax(int id)
        {
            await _favoriteService.DeleteFavoriteAsync(id);

            var userId = User?.Identity?.Name ?? string.Empty;
            var favorites = string.IsNullOrEmpty(userId)
                ? new List<ResultFavoriteDTO>()
                : await _favoriteService.GetUserFavoritesAsync(userId);

            return PartialView(@"~/Views/Shared/Components/_MiniFavoritePartialView/_MiniFavoritePartialView.cshtml", favorites);
        }
    }
}
