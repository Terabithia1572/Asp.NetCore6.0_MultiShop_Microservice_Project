using MongoDB.Bson; // MongoDB'nin ObjectId türü için
using MongoDB.Bson.Serialization.Attributes; // BsonId, BsonRepresentation gibi attribute'lar için

namespace MultiShop.Catalog.Entities
{
    /// <summary>
    /// 🔹 Bu sınıf, ürün bazlı indirimleri (kampanyaları) tutmak için oluşturuldu.
    /// 🔹 Her kayıt bir ürün için geçerli olan yüzde indirimi, tarih aralığını ve aktiflik durumunu barındırır.
    /// 🔹 Örnek: "Bilgisayar ürününe 01.10.2025 - 31.12.2025 arası %25 indirim"
    /// </summary>
    public class ProductDiscount
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ProductDiscountID { get; set; }
        // 👉 MongoDB'deki "_id" alanıdır.
        // ObjectId tipindedir ancak burada string olarak temsil edilir.
        // Her yeni kayıt eklendiğinde MongoDB tarafından otomatik oluşturulur.
        // Örnek: "6710f3aa9a8c8d18489f2a55"

        [BsonRepresentation(BsonType.ObjectId)]
        public string ProductID { get; set; }
        // 👉 İndirim hangi ürüne aitse o ürünün ID'sini tutar.
        // Bu değer "Products" koleksiyonundaki ProductID ile ilişkilidir.
        // ObjectId olarak saklanır ancak string biçiminde okunur.
        // Örnek: "68a0c6cf225291e81c547121"

        public decimal DiscountRate { get; set; }
        // 👉 Yüzdelik indirim oranıdır.
        // 25 => %25 indirim anlamına gelir.
        // İstersen buraya ilerde "DiscountAmount" (sabit tutar indirimi) özelliği de ekleyebiliriz.

        public DateTime StartDate { get; set; }
        // 👉 İndirimin başlama tarihidir.
        // Örnek: 2025-10-01T00:00:00Z

        public DateTime EndDate { get; set; }
        // 👉 İndirimin bitiş tarihidir.
        // Örnek: 2025-12-31T23:59:59Z

        public bool IsActive { get; set; }
        // 👉 Bu kampanyanın şu anda aktif olup olmadığını belirtir.
        // true => aktif kampanya
        // false => pasif (geçmiş veya iptal edilmiş)
    }
}
