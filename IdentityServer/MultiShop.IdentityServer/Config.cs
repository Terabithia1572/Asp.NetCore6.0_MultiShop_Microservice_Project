using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace MultiShop.IdentityServer
{
    public static class Config
    {
        // API kaynaklarını tanımlar. Her bir API kaynağı, erişim izinlerini (scopes) içerir.
        // "ResourceCatalog": Katalog API'si için bir kaynak tanımlanır.
        // "ResourceDiscount": İndirim API'si için bir kaynak tanımlanır.
        // "ResourceOrder": Sipariş API'si için bir kaynak tanımlanır.
        // Her kaynak, ilgili erişim izinlerini (scopes) içerir.
        public static IEnumerable<ApiResource> ApiResources => new ApiResource[]
        {
            new ApiResource("ResourceCatalog")
            {
                Scopes = { "CatalogFullPermission", "CatalogReadPermission" }
            },
            new ApiResource("ResourceDiscount")
            {
                Scopes = { "DiscountFullPermission" }
            },
            new ApiResource("ResourceOrder")
            {
                Scopes = { "OrderFullPermission" }
            },
        };

        // Kimlik doğrulama sırasında kullanıcının kimliğini ve temel bilgilerini içeren standart identity kaynakları tanımlanır.
        // OpenId: Kullanıcı kimliğini (id) sağlar. Kimlik doğrulamanın temelidir ve zorunludur.
        // Email: Kullanıcının e-posta bilgisini sağlar.
        // Profile: Kullanıcının profil bilgilerini (ad, soyad, vs.) sağlar.
        public static IEnumerable<IdentityResource> IdentityResources => new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Email(),
            new IdentityResources.Profile()
        };

        // API'ye erişim kapsamlarını (scopes) tanımlar.
        // "CatalogFullPermission": Katalog API'sine tam yetkili erişimi temsil eder.
        // "CatalogReadPermission": Sadece okuma yetkisiyle erişimi temsil eder.
        public static IEnumerable<ApiScope> ApiScopes => new ApiScope[]
        {
            new ApiScope("CatalogFullPermission", "Full access to the Catalog API"),
            new ApiScope("CatalogReadPermission", "Read access to the Catalog API"),
            new ApiScope("DiscountFullPermission", "Full access to the Discount API"),
            new ApiScope("DiscountReadPermission", "Read access to the Discount API"),
            new ApiScope("OrderFullPermission", "Full access to the Order API"),
            new ApiScope("OrderReadPermission", "Read access to the Order API")
        };

        // IdentityServer için kullanılacak istemci (client) uygulamaları tanımlanır.
        // Her client'ın hangi grant type'ları ve hangi scope'ları (izinleri) kullanabileceği belirtilir.
        public static IEnumerable<Client> Clients => new Client[]
        {
            // Sadece katalog okuma yetkisine sahip, client credentials ile erişen client
            new Client
            {
                ClientId = "MultiShopVisitorID", // İstemcinin (client) benzersiz kimliği.
                ClientName = "MultiShop Visitor User", // İstemcinin açıklayıcı adı.
                AllowedGrantTypes = GrantTypes.ClientCredentials, // Kimlik doğrulama için kullanılan grant type (client credentials akışı).
                ClientSecrets = { new Secret("multishopsecret".Sha256()) }, // Client için güvenlik anahtarı (SHA-256 ile şifrelenmiş).
                AllowedScopes = { "CatalogReadPermission" } // Bu client yalnızca "CatalogReadPermission" yetkisiyle API'ye erişebilir.
            },

            // Katalog yönetici paneli için, kod akışıyla giriş yapan client (kullanıcı kimliğiyle giriş)
            new Client
            {
                ClientId = "MultiShopManagerID", // İstemcinin (client) benzersiz kimliği.
                ClientName = "MultiShop Manager User", // İstemcinin açıklayıcı adı.
                AllowedGrantTypes = GrantTypes.Code, // Kimlik doğrulama için kullanılan grant type (authorization code akışı).
                AllowedScopes =
                {
                    "CatalogReadPermission", // Katalog API'sine okuma izni ile erişebilir.
                    "CatalogFullPermission"  // Katalog API'sine tam erişim izni ile erişebilir.
                }
            },

            // Yönetici uygulaması için, client credentials ile tam yetkili erişimi olan client
            new Client
            {
                ClientId = "MultiShopAdminID", // İstemcinin (client) benzersiz kimliği.
                ClientName = "MultiShop Admin User", // İstemcinin açıklayıcı adı.
                AllowedGrantTypes = GrantTypes.ClientCredentials, // (DÜZELTİLDİ) Kimlik doğrulama için client credentials grant type kullanılır.
                ClientSecrets = { new Secret("multishopsecret".Sha256()) }, // Client için güvenlik anahtarı (SHA-256 ile şifrelenmiş).
                AllowedScopes =
                {
                    "CatalogFullPermission",       // Katalog API'sine tam erişim
                    "DiscountFullPermission",      // İndirim API'sine tam erişim
                    "OrderFullPermission",         // Sipariş API'sine tam erişim
                    "CatalogReadPermission",       // Katalog API'sine okuma erişimi
                    IdentityServerConstants.LocalApi.ScopeName,         // Local API erişimi
                    IdentityServerConstants.StandardScopes.OpenId,      // OpenID kimlik doğrulama
                    IdentityServerConstants.StandardScopes.Profile,     // Kullanıcı profili bilgileri
                    IdentityServerConstants.StandardScopes.Email        // Kullanıcı e-posta bilgileri
                },
                AccessTokenLifetime = 600 // Erişim token'ının ömrü (saniye cinsinden).
            }
        };
    }
}
