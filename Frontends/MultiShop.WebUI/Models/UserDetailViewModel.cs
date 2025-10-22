using System.Text.Json.Serialization;

namespace MultiShop.WebUI.Models
{
    public class UserDetailViewModel
    {
        [JsonPropertyName("id")]
        public string ID { get; set; }        // Burada ID 'nin türünü string olarak varsaydım, gerekirse türü değiştirin
        [JsonPropertyName("userName")]
        public string Username { get; set; } // Buda Username 'nin türünü string olarak varsaydım, gerekirse türü değiştirin
        [JsonPropertyName("email")]
        public string Email { get; set; } // Buda Email 'nin türünü string olarak varsaydım, gerekirse türü değiştirin
        [JsonPropertyName("name")]
        public string Name { get; set; } // Buda Name 'nin türünü string olarak varsaydım, gerekirse türü değiştirin
        [JsonPropertyName("surname")]
        public string Surname { get; set; } // Buda Surname 'nin türünü string olarak varsaydım, gerekirse türü değiştirin

        [JsonPropertyName("imageUrl")]
        public string ProfileImageUrl { get; set; } // Buda ProfileImageUrl 'nin türünü string olarak varsaydım, gerekirse türü değiştirin

        // 🔹 Yeni eklenen alanlar
        [JsonPropertyName("about")] 
        public string About { get; set; } // Buda About 'nin türünü string olarak varsaydım, gerekirse türü değiştirin

        [JsonPropertyName("city")]
        public string City { get; set; } // Buda City 'nin türünü string olarak varsaydım, gerekirse türü değiştirin

        [JsonPropertyName("gender")]
        public string Gender { get; set; } // Buda Gender 'nin türünü string olarak varsaydım, gerekirse türü değiştirin

        [JsonPropertyName("lastLoginDate")]
        public DateTime? LastLoginDate { get; set; } // Buda LastLoginDate 'nin türünü DateTime? olarak varsaydım, gerekirse türü değiştirin

        [JsonPropertyName("registerDate")]
        public DateTime RegisterDate { get; set; } // Buda RegisterDate 'nin türünü DateTime olarak varsaydım, gerekirse türü değiştirin
    }
} 

