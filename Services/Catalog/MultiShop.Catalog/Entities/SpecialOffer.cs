using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MultiShop.Catalog.Entities
{
    public class SpecialOffer
    {
        [BsonId] //Bu attribute, hangi property’nin MongoDB’de “_id” alanı olarak tutulacağını belirtir.

        [BsonRepresentation(BsonType.ObjectId)] // Bu attribute, MongoDB’deki ObjectId türündeki verilerin string olarak temsil edilmesini sağlar.
        public string SpecialOfferID { get; set; } //Özel Teklif ID
        public string SpecialOfferTitle { get; set; } //Özel Teklif Başlığı
        public string SpecialOfferSubTitle { get; set; } //Özel Teklif alt Başlığı
        public string SpecialOfferImageUrl { get; set; } //Özel Teklif Resim URL

    }
}
