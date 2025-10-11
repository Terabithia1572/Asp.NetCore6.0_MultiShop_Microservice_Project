namespace MultiShop.Catalog.DTOs.ProductDTOs
{
    public class ResultProductWithDiscountDTO
    {
        public string ProductID { get; set; }              // Ürünün benzersiz ID'si (Mongo ObjectId string)
        public string ProductName { get; set; }            // Ürün adı
        public decimal ProductPrice { get; set; }          // Ürünün orijinal fiyatı
        public string ProductImageURL { get; set; }        // Ürün görsel yolu

        // 🔽 İndirim bilgileri (ProductDiscounts koleksiyonundan gelecek)
        public decimal? DiscountRate { get; set; }         // İndirim % oranı (örn: 25 => %25). Yoksa null.

        // ⚠️ Controller bu alanı set edecek, bu yüzden set; ekledik.
        public decimal DiscountedPrice { get; set; }       // İndirim uygulanmış fiyat (yoksa = ProductPrice)
    }
}

