using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MultiShop.Catalog.Entities
{
    public class ProductDetail
    {
        [BsonId] //Bu attribute, hangi property’nin MongoDB’de “_id” alanı olarak tutulacağını belirtir.

        [BsonRepresentation(BsonType.ObjectId)] // Bu attribute, MongoDB’deki ObjectId türündeki verilerin string olarak temsil edilmesini sağlar.
        public string ProductDetailID { get; set; } // MongoDB'de benzersiz kimlik olarak kullanılacak
        public string ProductDetailDescription { get; set; } // Ürün Detay Açıklamasını Tuttuk.
        public string ProductDetailInfo { get; set; } //Ürün Detay Bilgilerini Tuttuk.,
        public string ProductID { get; set; } //Ürün ile ilişkili olduğu için Ürün ID'sini aldık.
        [BsonIgnore] // Bu attribute, MongoDB'de bu property'nin serileştirilmemesini sağlar.
        public Product Product { get; set; } //Product tablosuyla ilişki ekledik.
    }
}
