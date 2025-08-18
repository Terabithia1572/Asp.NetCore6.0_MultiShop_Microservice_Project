namespace MultiShop.Catalog.DTOs.OfferDiscountDTOs
{
    public class CreateOfferDiscountDTO
    {
        public string OfferDiscountTitle { get; set; } //Özel Teklif Başlığı
        public string OfferDiscountSubTitle { get; set; } //Özel Teklif alt Başlığı
        public string OfferDiscountImageUrl { get; set; } //Özel Teklif Resim URL
        public string OfferDiscountButtonTitle { get; set; } // Özel Teklif Buton Başlığı
    }
}
