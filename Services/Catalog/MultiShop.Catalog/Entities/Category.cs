using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MultiShop.Catalog.Entities
{
    public class Category
    {
        [BsonId] //Bu attribute, hangi property’nin MongoDB’de “_id” alanı olarak tutulacağını belirtir.

        [BsonRepresentation(BsonType.ObjectId)] // Bu attribute, MongoDB’deki ObjectId türündeki verilerin string olarak temsil edilmesini sağlar.
        public string CategoryID { get; set; } // MongoDB'de benzersiz kimlik olarak kullanılacak
        public string CategoryName { get; set; } //Kategori Adını Tuttuk.
        public string CategoryImageURL { get; set; } //Kategori Resim Görselini Tuttuk.

    }
}
