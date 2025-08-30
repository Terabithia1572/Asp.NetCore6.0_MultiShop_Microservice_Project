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
    public class IdentityService : IIdentityService // IIdentityService arayüzünü implemente ettik.
    {
        private readonly HttpClient _httpClient; // HTTP istekleri yapmak için HttpClient kullanıyoruz.
        private readonly IHttpContextAccessor _httpContextAccessor; // HTTP context'e erişim için kullanılır.
        private readonly ClientSettings _clientSettings; // Uygulama ayarlarını tutan ClientSettings nesnesi. 
        public IdentityService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor, IOptions<ClientSettings> clientSettings) // Constructor ile bağımlılıkları alıyoruz.
        {
            _httpClient = httpClient; // HttpClient bağımlılığı.
            _httpContextAccessor = httpContextAccessor; // IHttpContextAccessor bağımlılığı.
            _clientSettings = clientSettings.Value; // IOptions<ClientSettings> ile gelen ayarları alıyoruz.
        }

        public async Task<bool> SignIn(SignUpDTO signUpDTO) // SignIn metodu, kullanıcı girişi yapmak için kullanılır.
        {
            var discoveryEndpoint = await _httpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest // Kimlik sağlayıcının keşif belgesini alıyoruz.
            {
                Address = "https://localhost:5001", // Kimlik sağlayıcının adresi.
                Policy = new DiscoveryPolicy { RequireHttps = false } // HTTPS gereksinimini devre dışı bırakıyoruz (geliştirme ortamı için).
            });

            var passwordTokenRequest = new PasswordTokenRequest // Parola tabanlı token isteği oluşturuyoruz.
            {
                ClientId = _clientSettings.MultiShopManagerID.ClientID, // Yönetici istemcisinin kimlik bilgileri
                ClientSecret = _clientSettings.MultiShopManagerID.ClientSecret, // Yönetici istemcisinin gizli anahtarı
                UserName = signUpDTO.UserName, // Kullanıcı adı
                Password = signUpDTO.Password, // Şifre
                Address = discoveryEndpoint.TokenEndpoint, // Token endpoint adresi
            };
            var token=await _httpClient.RequestPasswordTokenAsync(passwordTokenRequest); // Token isteğini gönderiyoruz.
            var userInfoRequest = new UserInfoRequest // Kullanıcı bilgisi isteği oluşturuyoruz.
            {
                Token = token.AccessToken, // Erişim token'ı
                Address = discoveryEndpoint.UserInfoEndpoint // Kullanıcı bilgi endpoint adresi
            };
            var userInfo = await _httpClient.GetUserInfoAsync(userInfoRequest); // Kullanıcı bilgisi isteğini gönderiyoruz.

            if (token.IsError || userInfo.IsError) // Eğer token veya kullanıcı bilgisi isteğinde hata varsa
            {
                return false; // Giriş başarısız
            }
            ClaimsIdentity claimsIdentity=new ClaimsIdentity(userInfo.Claims,CookieAuthenticationDefaults.AuthenticationScheme,"name", "role"); // ClaimsIdentity oluşturuyoruz.
            ClaimsPrincipal claimsPrincipal=new ClaimsPrincipal(claimsIdentity); // ClaimsPrincipal oluşturuyoruz.
            var authenticationProperties = new AuthenticationProperties(); // Kimlik doğrulama özelliklerini ayarlıyoruz.
            authenticationProperties.StoreTokens(new List<AuthenticationToken> // Token bilgilerini saklıyoruz.
            {
                new AuthenticationToken
                {
                    Name=OpenIdConnectParameterNames.AccessToken, // Erişim token'ı
                    Value=token.AccessToken // Token değeri
                },
                new AuthenticationToken
                {
                    Name=OpenIdConnectParameterNames.RefreshToken, // Yenileme token'ı
                    Value=token.RefreshToken // Token değeri
                },
                new AuthenticationToken
                {
                    Name=OpenIdConnectParameterNames.ExpiresIn, // Token'ın geçerlilik süresi
                    Value=DateTime.Now.AddSeconds(token.ExpiresIn).ToString() // Geçerlilik süresi
                }
            });
            authenticationProperties.IsPersistent = true; // Oturumun kalıcı olmasını sağlıyoruz.
            await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, authenticationProperties); // Kullanıcıyı oturum açtırıyoruz.
            return true; // Giriş başarılı


        }
    }
}
