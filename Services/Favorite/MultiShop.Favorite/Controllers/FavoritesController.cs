using Microsoft.AspNetCore.Mvc;
using MultiShop.Favorite.DTOs;
using MultiShop.Favorite.Services;

namespace MultiShop.Favorite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoritesController : ControllerBase
    {
        private readonly IFavoriteService _favoriteService;

        public FavoritesController(IFavoriteService favoriteService)
        {
            _favoriteService = favoriteService;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserFavorites(string userId)
        {
            var result = await _favoriteService.GetUserFavoritesAsync(userId);
            return Ok(result);
        }

        [HttpPost("AddFavorite")]
        public async Task<IActionResult> AddFavorite(CreateFavoriteDTO dto)
        {
            await _favoriteService.AddFavoriteAsync(dto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFavorite(int id)
        {
            await _favoriteService.DeleteFavoriteAsync(id);
            return Ok();
        }

        [HttpDelete("DeleteByProduct")]
        public async Task<IActionResult> DeleteByProduct(string userId, string productId)
        {
            await _favoriteService.DeleteByProductAsync(userId, productId);
            return Ok();
        }
    }
}
