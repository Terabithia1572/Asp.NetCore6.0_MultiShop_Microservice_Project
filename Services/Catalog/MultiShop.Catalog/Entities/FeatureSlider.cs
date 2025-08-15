using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MultiShop.Catalog.Entities
{
    public class FeatureSlider
    {
        [BsonId] //Bu attribute, hangi property’nin MongoDB’de “_id” alanı olarak tutulacağını belirtir.

        [BsonRepresentation(BsonType.ObjectId)] // Bu attribute, MongoDB’deki ObjectId türündeki verilerin string olarak temsil edilmesini sağlar.
        public string FeatureSliderID { get; set; } 
        public string FeatureSliderTitle { get; set; } //Feature Adını Tuttuk.
        public string FeatureSliderDescription { get; set; } //Feature Açıklamasını Tuttuk.
        public string FeatureSliderImageURL { get; set; } //Feature Resim Url'sini Tuttuk.
        public bool FeatureSliderStatus { get; set; } // 
    }
}
