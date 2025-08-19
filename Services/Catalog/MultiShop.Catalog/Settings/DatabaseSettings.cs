namespace MultiShop.Catalog.Settings
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public string CategoryCollectionName { get; set; } // Kategori koleksiyonunun adını tutar.
        public string ProductCollectionName { get; set; } // Ürün koleksiyonunun adını tutar.
        public string ProductDetailCollectionName { get; set; } // Ürün detay koleksiyonunun adını tutar.
        public string ProductImageCollectionName { get; set; } // Ürün resim koleksiyonunun adını tutar.
        public string ConnectionString { get; set; } // MongoDB bağlantı dizesini tutar.
        public string DatabaseName { get; set; } // Veritabanı adını tutar.
        public string FeatureSliderCollectionName { get;  set; } // Özellik kaydırıcı koleksiyonunun adını tutar.
        public string SpecialOfferCollectionName { get; set; } // Özel teklif koleksiyonunun adını tutar.
        public string FeatureCollectionName { get; set; } // Özellik koleksiyonunun adını tutar.
        public string OfferDiscountCollectionName { get; set; } // Teklif indirim koleksiyonunun adını tutar.
        public string BrandCollectionName { get; set; } // Marka koleksiyonunun adını tutar.
        public string AboutCollectionName { get; set; } // Hakkında koleksiyonunun adını tutar.
    }
}
