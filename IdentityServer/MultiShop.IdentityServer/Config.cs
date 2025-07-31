// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.Models;
using System.Collections.Generic;

namespace MultiShop.IdentityServer
{
    public static class Config
    {
        public static IEnumerable<ApiResource> ApiResources => new ApiResource[]
    {
    new ApiResource("ResourceCatalog")
    {
        Scopes = { "CatalogFullPermission", "CatalogReadPermission" }
    }
    };
        // "ResourceCatalog" adında bir API kaynağı tanımlanır. 
        // Bu API kaynağına erişmek için iki farklı yetki kapsamı (scope) belirlenmiştir:
        // 1. "CatalogFullPermission": Katalog API'sine tam erişim izni sağlar (okuma, yazma, güncelleme, silme).
        // 2. "CatalogReadPermission": Katalog API'sini sadece okuma (read) izniyle kullanmaya imkan tanır.

        public static IEnumerable<IdentityResource> IdentityResources => new IdentityResource[]
        {
    new IdentityResources.OpenId(),
    new IdentityResources.Email(),
    new IdentityResources.Profile()
        };
        // Kimlik doğrulama sırasında kullanıcının kimliğini ve temel bilgilerini içeren standart identity kaynakları tanımlanır.
        // OpenId: Kullanıcı kimliğini (id) sağlar. Kimlik doğrulamanın temelidir ve zorunludur.
        // Email: Kullanıcının e-posta bilgisini sağlar.
        // Profile: Kullanıcının profil bilgilerini (ad, soyad, vs.) sağlar.

        public static IEnumerable<ApiScope> ApiScopes => new ApiScope[]
        {
    new ApiScope("CatalogFullPermission", "Full access to the Catalog API"),
    new ApiScope("CatalogReadPermission", "Read access to the Catalog API")
        };
        // API'ye erişim kapsamlarını (scopes) tanımlar.
        // "CatalogFullPermission": Katalog API'sine tam yetkili erişimi temsil eder.
        // "CatalogReadPermission": Sadece okuma yetkisiyle erişimi temsil eder.
    }
}