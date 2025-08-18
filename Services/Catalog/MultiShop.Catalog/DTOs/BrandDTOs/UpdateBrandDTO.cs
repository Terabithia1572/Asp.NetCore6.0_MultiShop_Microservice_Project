namespace MultiShop.Catalog.DTOs.BrandDTOs
{
    public class UpdateBrandDTO
    {
        public string BrandID { get; set; } // MongoDB'de benzersiz kimlik olarak kullanılacak
        public string BrandName { get; set; } //Marka Adını Tuttuk.
        public string BrandImageURL { get; set; } //Marka Resim Görselini Tuttuk.
    }
}
