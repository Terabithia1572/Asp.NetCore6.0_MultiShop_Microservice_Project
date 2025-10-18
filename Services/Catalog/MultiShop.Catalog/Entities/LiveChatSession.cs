using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace MultiShop.Catalog.Entities
{
    public class LiveChatSession
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string SessionID { get; set; }

        public string UserName { get; set; }  // Giriş yapan kullanıcının adı
        public string ConnectionId { get; set; } // SignalR bağlantı kimliği
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true; // Oturum halen aktif mi?
    }
}
