using Microsoft.EntityFrameworkCore;

namespace MultiShop.Favorite.DataAccess
{
    public class FavoriteContext:DbContext
    {
        public FavoriteContext(DbContextOptions<FavoriteContext> options) : base(options)
        {
        }
        public DbSet<Entities.Favorite> Favorites { get; set; }
    }
}
