using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MultiShop.Catalog.Entities
{
    public class OfferDiscount
    {
        [BsonId] //Bu attribute, hangi property’nin MongoDB’de “_id” alanı olarak tutulacağını belirtir.

        [BsonRepresentation(BsonType.ObjectId)] // Bu attribute, MongoDB’deki ObjectId türündeki verilerin string olarak temsil edilmesini sağlar.
        public string OfferDiscountID { get; set; } //Özel Teklif ID
        public string OfferDiscountTitle { get; set; } //Özel Teklif Başlığı
        public string OfferDiscountSubTitle { get; set; } //Özel Teklif alt Başlığı
        public string OfferDiscountImageUrl { get; set; } //Özel Teklif Resim URL
        public string OfferDiscountButtonTitle { get; set; } // Özel Teklif Buton Başlığı
    }
}
