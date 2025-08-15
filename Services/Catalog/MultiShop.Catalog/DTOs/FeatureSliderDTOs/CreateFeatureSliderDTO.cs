namespace MultiShop.Catalog.DTOs.FeatureSliderDTOs
{
    public class CreateFeatureSliderDTO
    {
        public string FeatureSliderTitle { get; set; } //Feature Adını Tuttuk.
        public string FeatureSliderDescription { get; set; } //Feature Açıklamasını Tuttuk.
        public string FeatureSliderImageURL { get; set; } //Feature Resim Url'sini Tuttuk.
        public bool FeatureSliderStatus { get; set; } // 
    }
}
