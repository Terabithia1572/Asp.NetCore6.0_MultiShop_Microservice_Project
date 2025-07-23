namespace MultiShop.Catalog.DTOs.ProductDetailDTOs
{
    public class ResultProductDetailDTO
    {
        public string ProductDetailID { get; set; } // MongoDB'de benzersiz kimlik olarak kullanılacak
        public string ProductDetailDescription { get; set; } // Ürün Detay Açıklamasını Tuttuk.
        public string ProductDetailInfo { get; set; } //Ürün Detay Bilgilerini Tuttuk.
    }
}
