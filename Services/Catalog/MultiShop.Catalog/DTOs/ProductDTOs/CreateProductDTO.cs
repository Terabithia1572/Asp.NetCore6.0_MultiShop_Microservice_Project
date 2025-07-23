namespace MultiShop.Catalog.DTOs.ProductDTOs
{
    public class CreateProductDTO
    {
     
        public string ProductName { get; set; } // Ürün Adını Tuttuk.
        public decimal ProductPrice { get; set; } //Ürün Fiyatını Tuttuk.
        public string ProductDescription { get; set; } //Ürün Açıklamasını Tuttuk.
        public string ProductImageURL { get; set; } //Ürün Resmini Tuttuk.
        public string CategoryID { get; set; } // Ürünün ait olduğu kategori ID'sini tuttuk.
    }
}
