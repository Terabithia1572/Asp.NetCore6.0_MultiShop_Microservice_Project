using System.ComponentModel.DataAnnotations;

namespace MultiShop.Favorite.Entities
{
    public class Favorite
    {
        [Key]
        public int FavoriteID { get; set; }
        public string UserID { get; set; } = string.Empty;
        public string ProductID { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public decimal ProductPrice { get; set; }
        public string ProductImageUrl { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
