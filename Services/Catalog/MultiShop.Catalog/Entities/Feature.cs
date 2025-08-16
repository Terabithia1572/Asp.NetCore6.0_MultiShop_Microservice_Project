using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MultiShop.Catalog.Entities
{
    public class Feature
    {
        [BsonId] //Bu attribute, hangi property’nin MongoDB’de “_id” alanı olarak tutulacağını belirtir.

        [BsonRepresentation(BsonType.ObjectId)] // Bu attribute, MongoDB’deki ObjectId türündeki verilerin string olarak temsil edilmesini sağlar.
        public string FeatureID { get; set; } // MongoDB'de benzersiz kimlik olarak kullanılacak
        public string FeatureTitle { get; set; } // Özelliğin başlığını tutar.
        public string FeatureIcon { get; set; } // Özelliğin simgesini tutar.

    }
}
