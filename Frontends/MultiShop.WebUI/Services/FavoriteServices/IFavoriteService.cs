using MultiShop.DTOLayer.FavoriteDTOs;

namespace MultiShop.WebUI.Services.FavoriteServices
{
    public interface IFavoriteService
    {

        Task<List<ResultFavoriteDTO>> GetUserFavoritesAsync(string userId);
        Task AddFavoriteAsync(CreateFavoriteDTO dto);
        Task DeleteFavoriteAsync(int id);
    }
}
