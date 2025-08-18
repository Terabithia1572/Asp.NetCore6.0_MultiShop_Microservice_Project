using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MultiShop.Catalog.Entities
{
    public class Brand
    {
        [BsonId] //Bu attribute, hangi property’nin MongoDB’de “_id” alanı olarak tutulacağını belirtir.

        [BsonRepresentation(BsonType.ObjectId)] // Bu attribute, MongoDB’deki ObjectId türündeki verilerin string olarak temsil edilmesini sağlar.
        public string BrandID { get; set; } // MongoDB'de benzersiz kimlik olarak kullanılacak
        public string BrandName { get; set; } //Marka Adını Tuttuk.
        public string BrandImageURL { get; set; } //Marka Resim Görselini Tuttuk.
    }
}
