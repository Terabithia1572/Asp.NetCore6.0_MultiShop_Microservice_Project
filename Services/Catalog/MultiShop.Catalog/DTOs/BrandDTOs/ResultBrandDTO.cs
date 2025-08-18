namespace MultiShop.Catalog.DTOs.BrandDTOs
{
    public class ResultBrandDTO
    {
        public string BrandID { get; set; } // MongoDB'de benzersiz kimlik olarak kullanılacak
        public string BrandName { get; set; } //Marka Adını Tuttuk.
        public string BrandImageURL { get; set; } //Marka Resim Görselini Tuttuk.
    }
}
