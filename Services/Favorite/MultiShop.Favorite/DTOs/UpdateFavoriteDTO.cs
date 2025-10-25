namespace MultiShop.Favorite.DTOs
{
    public class UpdateFavoriteDTO
    {
        public int FavoriteID { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal ProductPrice { get; set; }
        public string ProductImageUrl { get; set; } = string.Empty;
    }
}
