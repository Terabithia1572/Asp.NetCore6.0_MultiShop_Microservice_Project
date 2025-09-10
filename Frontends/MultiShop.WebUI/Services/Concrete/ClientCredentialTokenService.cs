using IdentityModel.AspNetCore.AccessTokenManagement;
using IdentityModel.Client;
using Microsoft.Extensions.Options;
using MultiShop.DTOLayer.IdentityDTOs.LoginDTOs;
using MultiShop.WebUI.Services.Interfaces;
using MultiShop.WebUI.Settings;

namespace MultiShop.WebUI.Services.Concrete
{
    public class ClientCredentialTokenService : IClientCredentialTokenService
    {
        private readonly ServiceApiSettings _serviceApiSettings; //appsettings.json'daki ServiceApiSettings'i okumak için
        private readonly HttpClient _httpClient; //token'ı almak için httpclient kullanacağız
        private readonly IClientAccessTokenCache _clientAccessTokenCache; //token'ı cache'de tutmak için
        private readonly ClientSettings _clientSettings; //appsettings.json'daki ClientSettings'i okumak için

        public ClientCredentialTokenService(IOptions<ServiceApiSettings> serviceApiSettings, HttpClient httpClient, IClientAccessTokenCache clientAccessTokenCache, IOptions<ClientSettings> clientSettings)
        {
            _serviceApiSettings = serviceApiSettings.Value; //IOptions'tan Value ile gerçek nesneye erişiyoruz
            _httpClient = httpClient; //HttpClient'i dependency injection ile alıyoruz
            _clientAccessTokenCache = clientAccessTokenCache; //IClientAccessTokenCache'i dependency injection ile alıyoruz
            _clientSettings = clientSettings.Value; //IOptions'tan Value ile gerçek nesneye erişiyoruz
        }

        public async Task<string> GetToken()
        {
            var currentToken = await _clientAccessTokenCache.GetAsync("multishoptoken"); //Cache'de token var mı kontrol et
            if (currentToken != null) //Eğer cache'de token varsa
            {
                return currentToken.AccessToken; //Cache'deki token'ı döndür
            }
            var disco = await _httpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
            {
                Address = _clientSettings.IdentityServerUrl, // appsettings'ten
                Policy = new DiscoveryPolicy
                {
                    RequireHttps = false // Dev ortamı için. Prod'da true olmalı.
                }
            });
            // 2) Password Grant ile Token Al
            var clientCredentialTokenReques = new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = _clientSettings.MultiShopVisitorClient.ClientID,
                ClientSecret = _clientSettings.MultiShopVisitorClient.ClientSecret,

            };
            var newToken = await _httpClient.RequestClientCredentialsTokenAsync(clientCredentialTokenReques);
            await _clientAccessTokenCache.SetAsync("multishoptoken", newToken.AccessToken, newToken.ExpiresIn); //Yeni token'ı cache'e ekle
            return newToken.AccessToken; //Yeni token'ı döndür

        }
    }
}
