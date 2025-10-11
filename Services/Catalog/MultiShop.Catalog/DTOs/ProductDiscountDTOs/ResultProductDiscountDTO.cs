namespace MultiShop.Catalog.DTOs.ProductDiscountDTOs
{
    public class ResultProductDiscountDTO
    {
        public string ProductDiscountID { get; set; }
        public string ProductID { get; set; }
        public decimal DiscountRate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
    }
}
