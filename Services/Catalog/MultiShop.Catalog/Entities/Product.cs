using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MultiShop.Catalog.Entities
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ProductID { get; set; } // MongoDB'de benzersiz kimlik olarak kullanılacak
        public string ProductName { get; set; } // Ürün Adını Tuttuk.
        public decimal ProductPrice { get; set; } //Ürün Fiyatını Tuttuk.
        public string ProductDescription { get; set; } //Ürün Açıklamasını Tuttuk.
        public string ProductImageURL { get; set; } //Ürün Resmini Tuttuk.
        [BsonRepresentation(BsonType.ObjectId)]
        public string CategoryID { get; set; } // Ürünün ait olduğu kategori ID'sini tuttuk.
      
        [BsonIgnore] // Bu attribute, MongoDB'de bu property'nin serileştirilmemesini sağlar.
        public Category Category { get; set; }
      
    }
}
