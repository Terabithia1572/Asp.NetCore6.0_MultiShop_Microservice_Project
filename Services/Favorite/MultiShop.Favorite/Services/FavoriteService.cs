using Microsoft.EntityFrameworkCore;
using MultiShop.Favorite.DataAccess;
using MultiShop.Favorite.DTOs;
using MultiShop.Favorite.Entities;

namespace MultiShop.Favorite.Services
{
    public class FavoriteService : IFavoriteService
    {
        private readonly FavoriteContext _context;

        public FavoriteService(FavoriteContext context)
        {
            _context = context;
        }

        public async Task<List<ResultFavoriteDTO>> GetUserFavoritesAsync(string userId)
        {
            return await _context.Favorites
                .Where(x => x.UserID == userId)
                .Select(x => new ResultFavoriteDTO
                {
                    FavoriteID = x.FavoriteID,
                    ProductID = x.ProductID,
                    ProductName = x.ProductName,
                    ProductPrice = x.ProductPrice,
                    ProductImageUrl = x.ProductImageUrl
                }).ToListAsync();
        }

        public async Task AddFavoriteAsync(CreateFavoriteDTO dto)
        {
            var existing = await _context.Favorites
                .FirstOrDefaultAsync(x => x.UserID == dto.UserID && x.ProductID == dto.ProductID);
            if (existing != null) return;

            var entity = new Favorite.Entities.Favorite
            {
                UserID = dto.UserID,
                ProductID = dto.ProductID,
                ProductName = dto.ProductName,
                ProductPrice = dto.ProductPrice,
                ProductImageUrl = dto.ProductImageUrl,
                CreatedDate = DateTime.Now
            };

            _context.Favorites.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteFavoriteAsync(int id)
        {
            var fav = await _context.Favorites.FindAsync(id);
            if (fav == null) return;
            _context.Favorites.Remove(fav);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByProductAsync(string userId, string productId)
        {
            var fav = await _context.Favorites
                .FirstOrDefaultAsync(x => x.UserID == userId && x.ProductID == productId);
            if (fav == null) return;
            _context.Favorites.Remove(fav);
            await _context.SaveChangesAsync();
        }
    }
}
