namespace MultiShop.WebUI.Settings
{
    public class ClientSettings
    {
        public Client MultiShopVisitorClient { get; set; } // Ziyaretçi istemcisi için kimlik bilgileri
        public Client MultiShopManagerClient { get; set; } // Yönetici istemcisi için kimlik bilgileri
        public Client MultiShopAdminClient { get; set; } // Admin istemcisi için kimlik bilgileri
    }
    public class Client
    {
        public string ClientID { get; set; } // İstemci kimliği
        public string ClientSecret { get; set; } // İstemci güvenlik anahtarı
    }
}
