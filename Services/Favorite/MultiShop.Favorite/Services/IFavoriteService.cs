using MultiShop.Favorite.DTOs;

namespace MultiShop.Favorite.Services
{
    public interface IFavoriteService
    {
        Task<List<ResultFavoriteDTO>> GetUserFavoritesAsync(string userId);
        Task AddFavoriteAsync(CreateFavoriteDTO dto);
        Task DeleteFavoriteAsync(int id);
        Task DeleteByProductAsync(string userId, string productId);
    }
}
