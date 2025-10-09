namespace MultiShop.Catalog.DTOs.ProductDTOs
{
    public class ResultProductWithDiscountDTO
    {
        public string ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public string ProductImageURL { get; set; }

        // İndirim bilgileri (DiscountService'ten gelecek)
        public decimal? DiscountRate { get; set; }
        public decimal DiscountedPrice
        {
            get
            {
                if (DiscountRate.HasValue)
                    return ProductPrice - (ProductPrice * DiscountRate.Value / 100);
                return ProductPrice;
            }
        }
    }
}

