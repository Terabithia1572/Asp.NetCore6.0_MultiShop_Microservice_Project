using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MultiShop.Catalog.Entities
{
    public class ProductImage
    {
        [BsonId] //Bu attribute, hangi property’nin MongoDB’de “_id” alanı olarak tutulacağını belirtir.

        [BsonRepresentation(BsonType.ObjectId)] // Bu attribute, MongoDB’deki ObjectId türündeki verilerin string olarak temsil edilmesini sağlar.
        public string ProductImagesID { get; set; } // MongoDB'de benzersiz kimlik olarak kullanılacak
        public string ProductImage1 { get; set; } //Birden fazla ürünün resmi varsa yolunu birden fazla tabloda tutacağız.
        public string ProductImage2 { get; set; } //İkinci resim için
        public string ProductImage3 { get; set; } //Üçüncü resim için
        public string ProductID { get; set; } //Ürün ile ilişkili hale getirmek için ProductID ekledik.
        [BsonIgnore] // Bu attribute, MongoDB'de bu property'nin serileştirilmemesini sağlar.
        public Product Product { get; set; } //Product tablosuyla ilişki ekledik.

    }
}
