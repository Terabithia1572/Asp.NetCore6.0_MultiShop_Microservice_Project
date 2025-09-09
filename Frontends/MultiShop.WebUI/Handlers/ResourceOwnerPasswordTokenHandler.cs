using Microsoft.AspNetCore.Authentication; // Kimlik doğrulama işlemleri için gerekli namespace
using Microsoft.IdentityModel.Protocols.OpenIdConnect; // OpenID Connect protokolü ile ilgili sabitler ve yardımcı sınıflar
using MultiShop.WebUI.Services.Interfaces; // Projede tanımlı kimlik servis arayüzlerini kullanmak için
using System.Net; // HTTP durum kodlarını kullanmak için
using System.Net.Http.Headers; // HTTP isteklerine Authorization gibi header eklemek için

namespace MultiShop.WebUI.Handlers
{
    // DelegatingHandler: HTTP isteklerini göndermeden önce veya sonra özelleştirilmiş işlemler yapmaya imkân verir.
    public class ResourceOwnerPasswordTokenHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _httpContextAccessor; // HTTP context bilgilerine (örn: kullanıcı, token) erişmek için
        private readonly IIdentityService _identityService; // Kimlik servisi (token yenileme gibi işlemleri yapmak için)

        // Constructor: bağımlılıkların enjekte edilmesini sağlar.
        public ResourceOwnerPasswordTokenHandler(IHttpContextAccessor httpContextAccessor, IIdentityService identityService)
        {
            _httpContextAccessor = httpContextAccessor;
            _identityService = identityService;
        }

        // HTTP istekleri gönderilmeden önce burada işlenir.
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // Kullanıcının mevcut access_token bilgisini HttpContext üzerinden alır
            var accessToken = await _httpContextAccessor.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);

            // İstek header'ına Bearer token ekler (Authorization başlığı altında)
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            // İstek gönderilir ve cevap alınır
            var response = await base.SendAsync(request, cancellationToken);

            // Eğer gelen cevap "Unauthorized (401)" ise access_token süresi bitmiş olabilir
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                // Refresh token ile yeni access_token alınmaya çalışılır
                var isSuccess = await _identityService.GetRefreshToken();

                if (isSuccess)
                {
                    // Refresh token başarılı olursa, yeni access_token tekrar alınır
                    accessToken = await _httpContextAccessor.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);

                    // Yeni token tekrar Authorization header’a eklenir
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                    // İstek tekrar gönderilir
                    response = await base.SendAsync(request, cancellationToken);
                }
            }

            // Eğer hâlâ Unauthorized ise burada hata yönetimi yapılabilir
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                // Örn: loglama veya kullanıcıya mesaj döndürme
                // hata mesajı
            }

            // Son response döndürülür
            return response;
        }
    }
}
