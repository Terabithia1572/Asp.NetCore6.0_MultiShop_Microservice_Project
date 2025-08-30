namespace MultiShop.WebUI.Settings
{
    public class ClientSettings
    {
        public Client MultiShopVisitorClient { get; set; } // Ziyaretçi client
        public Client MultiShopManagerClient { get; set; } // Yönetici client
        public Client MultiShopAdminClient { get; set; }   // Admin client
        public string IdentityServerUrl { get; set; }      // EKLENDİ: Discovery adresi
    }

    public class Client
    {
        public string ClientID { get; set; }
        public string ClientSecret { get; set; }
    }
}
