using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using MultiShop.DTOLayer.IdentityDTOs.LoginDTOs;
using MultiShop.WebUI.Services.Interfaces;
using MultiShop.WebUI.Settings;
using System.Security.Claims;

namespace MultiShop.WebUI.Services.Concrete
{
    public class IdentityService : IIdentityService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ClientSettings _clientSettings;
        private readonly ServiceApiSettings _serviceApiSettings;

        public IdentityService(
            HttpClient httpClient,
            IHttpContextAccessor httpContextAccessor,
            IOptions<ClientSettings> clientSettings,
            IOptions<ServiceApiSettings> serviceApiSettings)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
            _clientSettings = clientSettings.Value; // Options binding ile gelir (null ise Program.cs/JSON kontrol et)
            _serviceApiSettings = serviceApiSettings.Value;
        }

        public async Task<bool> GetRefreshToken()
        {
            var discoveryEndPoint = await _httpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
            {
                Address = _serviceApiSettings.IdentityServerUrl,
                Policy = new DiscoveryPolicy
                {
                    RequireHttps = false
                }
            });

            var refreshToken = await _httpContextAccessor.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.RefreshToken);

            RefreshTokenRequest refreshTokenRequest = new()
            {
                ClientId = _clientSettings.MultiShopManagerClient.ClientID,
                ClientSecret = _clientSettings.MultiShopManagerClient.ClientSecret,
                RefreshToken = refreshToken,
                Address = discoveryEndPoint.TokenEndpoint
            };

            var token = await _httpClient.RequestRefreshTokenAsync(refreshTokenRequest);

            var authenticationToken = new List<AuthenticationToken>()
            {
                new AuthenticationToken
                {
                    Name=OpenIdConnectParameterNames.AccessToken,
                    Value = token.AccessToken
                },
                new AuthenticationToken
                {
                    Name=OpenIdConnectParameterNames.RefreshToken,
                    Value = token.RefreshToken
                },
                new AuthenticationToken
                { //NULL UPDATE
                    Name=OpenIdConnectParameterNames.ExpiresIn,
                    Value=DateTime.Now.AddSeconds(token.ExpiresIn).ToString()
                }
            };

            var result = await _httpContextAccessor.HttpContext.AuthenticateAsync();

            var properties = result.Properties;
            properties.StoreTokens(authenticationToken);

            await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, result.Principal, properties);

            return true;
        }

        public async Task<bool> SignIn(SignInDTO signInDTO)
        {
            // 1) Discovery
            var disco = await _httpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
            {
                Address = _clientSettings.IdentityServerUrl, // appsettings'ten
                Policy = new DiscoveryPolicy
                {
                    RequireHttps = false // Dev ortamı için. Prod'da true olmalı.
                }
            });

            if (disco.IsError)
            {
                // disco.Error'ı logla
                return false;
            }

            // 2) Password Grant ile Token Al
            var token = await _httpClient.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = _clientSettings.MultiShopManagerClient.ClientID,
                ClientSecret = _clientSettings.MultiShopManagerClient.ClientSecret,
                UserName = signInDTO.UserName,
                Password = signInDTO.Password,
                // Access token ve UserInfo için openid/profile; refresh istiyorsan offline_access
                Scope = "openid profile offline_access roles CatalogFullPermission"
            });

            if (token.IsError || string.IsNullOrEmpty(token.AccessToken))
            {
                // debug için bakın
                var status = token.HttpStatusCode;      // 400
                var err = token.Error;               // invalid_scope / invalid_client / invalid_grant ...
                var raw = token.Raw;                 // ham cevap
                return false;
            }

            // 3) UserInfo (Token NULL gönderilmemesi için koruma)
            var userInfo = await _httpClient.GetUserInfoAsync(new UserInfoRequest
            {
                Address = disco.UserInfoEndpoint,
                Token = token.AccessToken
            });

            if (userInfo.IsError || userInfo.Claims is null)
            {
                // userInfo.Error loglanabilir
                return false;
            }

            // 4) Claims & Cookie SignIn
            var claimsIdentity = new ClaimsIdentity(
                userInfo.Claims,
                CookieAuthenticationDefaults.AuthenticationScheme,
                nameType: "name",
                roleType: "role");

            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            var authProps = new AuthenticationProperties
            {
                IsPersistent = true // kalıcı oturum
            };

            // Tokenları cookie'de sakla (expires_at kullan)
            authProps.StoreTokens(new List<AuthenticationToken>
            {
                new AuthenticationToken
                {
                    Name = OpenIdConnectParameterNames.AccessToken,
                    Value = token.AccessToken
                },
                new AuthenticationToken
                {
                    Name = OpenIdConnectParameterNames.RefreshToken,
                    Value = token.RefreshToken ?? "" // offline_access yoksa null olabilir
                },
                new AuthenticationToken
                {
                    Name = "expires_at",
                    Value = DateTimeOffset.UtcNow.AddSeconds(token.ExpiresIn).ToString("o")
                }
            });

            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext is null) return false; // HTTP pipeline dışından çağrıldıysa

            await httpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                claimsPrincipal,
                authProps);

            return true;
        }
    }
}