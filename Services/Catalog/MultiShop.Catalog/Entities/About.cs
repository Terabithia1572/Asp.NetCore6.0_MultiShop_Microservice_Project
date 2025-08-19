using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MultiShop.Catalog.Entities
{
    public class About
    {

        [BsonId] //Bu attribute, hangi property’nin MongoDB’de “_id” alanı olarak tutulacağını belirtir.

        [BsonRepresentation(BsonType.ObjectId)] // Bu attribute, MongoDB’deki ObjectId türündeki verilerin string olarak temsil edilmesini sağlar.
        public string AboutID { get; set; } // MongoDB'de benzersiz kimlik olarak kullanılacak
        public string AboutDescription { get; set; } //Hakkında Açıklama Bilgilerini burada tutacak. 
        public string AboutAddress { get; set; } //Hakkında Adres Bilgilerini burada tutacak.
        public string AboutEmail { get; set; } // Hakkında Mail Bilgilerini burada tutacak.
        public string AboutPhone { get; set; } // Hakkında Telefon Bilgilerini burada tutacak.
    } 
}
