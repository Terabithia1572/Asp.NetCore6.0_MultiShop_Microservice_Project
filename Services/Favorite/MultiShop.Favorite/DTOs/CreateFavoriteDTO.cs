namespace MultiShop.Favorite.DTOs
{
    public class CreateFavoriteDTO
    {
        public string UserID { get; set; } = string.Empty;
        public string ProductID { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public decimal ProductPrice { get; set; }
        public string ProductImageUrl { get; set; } = string.Empty;
    }
}
