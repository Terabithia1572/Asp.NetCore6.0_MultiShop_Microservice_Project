namespace MultiShop.Catalog.DTOs.ProductDiscountDTOs
{
    public class ResultProductDiscountWithNameDTO
    {
        public string ProductDiscountID { get; set; }
        public string ProductID { get; set; }
        public string ProductName { get; set; } // 🔥 yeni alan
        public string ProductImageURL { get; set; } // 🔥 yeni alan
        public decimal DiscountRate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
        
    }
}
