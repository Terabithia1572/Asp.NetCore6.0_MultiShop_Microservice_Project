using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MultiShop.Catalog.Entities
{
    public class ProductDiscount
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ProductDiscountID { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string ProductID { get; set; } // Hangi ürüne ait indirim

        public decimal DiscountRate { get; set; } // % indirim (örn: 25 => %25)
        // İstersen ileride sabit tutar indirimi de ekleriz:
        // public decimal? DiscountAmount { get; set; } 

        public DateTime StartDate { get; set; } // Kampanya başlangıcı
        public DateTime EndDate { get; set; }   // Kampanya bitişi
        public bool IsActive { get; set; }      // Şu an aktif mi?
    }
}

