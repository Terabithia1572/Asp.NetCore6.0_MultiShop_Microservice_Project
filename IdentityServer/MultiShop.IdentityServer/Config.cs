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
            }, // Katalog API'si için kaynak tanımı
            new ApiResource("ResourceDiscount")
            {
                Scopes = { "DiscountFullPermission" }
            }, // İndirim API'si için kaynak tanımı
            new ApiResource("ResourceOrder")
            {
                Scopes = { "OrderFullPermission" }
            }, // Sipariş API'si için kaynak tanımı
            new ApiResource("ResourceCargo"){Scopes={"CargoFullPermission"}}, // Kargo API'si için kaynak tanımı
            new ApiResource("ResourceBasket"){Scopes={"BasketFullPermission"}}, // Sepet API'si için kaynak tanımı
            new ApiResource("ResourceComment"){Scopes={"CommentFullPermission"}}, // Sepet API'si için kaynak tanımı
            new ApiResource("ResourcePayment"){Scopes={"PaymentFullPermission"}}, // Sepet API'si için kaynak tanımı
            new ApiResource("ResourceImages"){Scopes={"ImagesFullPermission"}}, // Sepet API'si için kaynak tanımı
            new ApiResource("ResourceOcelot"){Scopes={"OcelotFullPermission"}}, // Sepet API'si için kaynak tanımı
            new ApiResource(IdentityServerConstants.LocalApi.ScopeName) // Local API erişimi için kaynak tanımı
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
            new ApiScope("CatalogFullPermission", "Full access to the Catalog API"), // Katalog API'sine tam erişim izni
            new ApiScope("CatalogReadPermission", "Read access to the Catalog API"), // Katalog API'sine okuma erişimi izni
            new ApiScope("DiscountFullPermission", "Full access to the Discount API"), // İndirim API'sine tam erişim izni
            new ApiScope("OrderFullPermission", "Full access to the Order API"), // Sipariş API'sine tam erişim izni
            new ApiScope("CargoFullPermission", "Full access to the Cargo API"), // Kargo API'sine tam erişim izni
            new ApiScope("BasketFullPermission", "Full access to the Basket API"), // Sepet API'sine tam erişim izni
            new ApiScope("CommentFullPermission", "Full access to the Comment API"), // Yorum API'sine tam erişim izni
            new ApiScope("PaymentFullPermission", "Full access to the Payment API"), // Satın alma API'sine tam erişim izni
            new ApiScope("ImagesFullPermission", "Full access to the Payment API"), // Resimler API'sine tam erişim izni
            new ApiScope("OcelotFullPermission", "Full access to the Ocelot API"), // Ocelot API'sine tam erişim izni
             new ApiScope(IdentityServerConstants.LocalApi.ScopeName) // Local API erişim izni
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
                AllowedScopes = { "CatalogReadPermission","CatalogFullPermission", "CommentFullPermission", "OcelotFullPermission", "PaymentFullPermission", "ImagesFullPermission" } // Bu client yalnızca "CatalogReadPermission" yetkisiyle API'ye erişebilir.
            },

            // Katalog yönetici paneli için, kod akışıyla giriş yapan client (kullanıcı kimliğiyle giriş)
            new Client
            {
                ClientId = "MultiShopManagerID", // İstemcinin (client) benzersiz kimliği.
                ClientName = "MultiShop Manager User", // İstemcinin açıklayıcı adı.
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword, // Kimlik doğrulama için kullanılan grant type (authorization code akışı).
                 ClientSecrets = { new Secret("multishopsecret".Sha256()) }, // Client için güvenlik anahtarı (SHA-256 ile şifrelenmiş).
                AllowedScopes =
                {
                    "CatalogReadPermission", // Katalog API'sine okuma izni ile erişebilir.
                    "CatalogFullPermission",  // Katalog API'sine tam erişim izni ile erişebilir.
                    "BasketFullPermission", // Sipariş API'sine tam erişim izni ile erişebilir.
                    "CommentFullPermission", //Yorumlar API'sine tam erişim izni ile erişebilir.
                    "PaymentFullPermission", // Satın Alma API'sine tam erişim izni ile erişebilir.
                    "ImagesFullPermission", //Resimler API'sine tam erişim izni ile erişebilir.
                    "OcelotFullPermission" // Ocelot API'sine tam erişim izni ile erişebilir.
                }
            },

            // Yönetici uygulaması için, client credentials ile tam yetkili erişimi olan client
            new Client
            {
                ClientId = "MultiShopAdminID", // İstemcinin (client) benzersiz kimliği.
                ClientName = "MultiShop Admin User", // İstemcinin açıklayıcı adı.
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword, // (DÜZELTİLDİ) Kimlik doğrulama için client credentials grant type kullanılır.
                ClientSecrets = { new Secret("multishopsecret".Sha256()) }, // Client için güvenlik anahtarı (SHA-256 ile şifrelenmiş).
                AllowedScopes =
                {
                    "CatalogFullPermission",       // Katalog API'sine tam erişim
                    "DiscountFullPermission",      // İndirim API'sine tam erişim
                    "OrderFullPermission",         // Sipariş API'sine tam erişim
                    "CatalogReadPermission",       // Katalog API'sine okuma erişimi
                    "CargoFullPermission",         // Kargo API'sine tam erişim
                    "BasketFullPermission",        // Sepet API'sine tam erişim
                    "CommentFullPermission",        // Yorumlar API'sine tam erişim
                    "PaymentFullPermission",        // Satın Alma API'sine tam erişim
                    "ImagesFullPermission",        // Resimler API'sine tam erişim
                    "OcelotFullPermission",        // Ocelot API'sine tam erişim
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
