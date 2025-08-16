namespace MultiShop.Catalog.DTOs.FeatureDTOs
{
    public class GetByIDFeatureDTO
    {
        public string FeatureID { get; set; } // MongoDB'de benzersiz kimlik olarak kullanılacak
        public string FeatureTitle { get; set; } // Özelliğin başlığını tutar.
        public string FeatureIcon { get; set; } // Özelliğin simgesini tutar.
    }
}
