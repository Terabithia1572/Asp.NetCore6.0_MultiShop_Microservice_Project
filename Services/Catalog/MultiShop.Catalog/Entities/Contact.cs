using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MultiShop.Catalog.Entities
{
    public class Contact
    {
        [BsonId] //Bu attribute, hangi property’nin MongoDB’de “_id” alanı olarak tutulacağını belirtir.

        [BsonRepresentation(BsonType.ObjectId)] // Bu attribute, MongoDB’deki ObjectId türündeki verilerin string olarak temsil edilmesini sağlar.
        public string ContactID { get; set; } // MongoDB'de benzersiz kimlik olarak kullanılacak
        public string ContactNameSurname { get; set; } //İletişim Adını Tuttuk.
        public string ContactEmail { get; set; } //İletişim Resim Görselini Tuttuk.
        public string ContactSubject { get; set; } //İletişim Konu Başlığını Tuttuk.
        public string ContactMessage { get; set; } //İletişim Mesajını Tuttuk.
        public DateTime ContactCreatedDate { get; set; } //İletişim Oluşturma Tarihini Tuttuk.
        public bool ContactIsRead { get; set; } //İletişim Durumunu Tuttuk.

    }
}
