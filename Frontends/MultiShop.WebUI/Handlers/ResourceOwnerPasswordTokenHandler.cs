using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using MultiShop.WebUI.Services.Interfaces;
using System.Net;
using System.Net.Http.Headers;

namespace MultiShop.WebUI.Handlers
{
    public class ResourceOwnerPasswordTokenHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IIdentityService _identityService;

        public ResourceOwnerPasswordTokenHandler(IHttpContextAccessor httpContextAccessor, IIdentityService identityService)
        {
            _httpContextAccessor = httpContextAccessor;
            _identityService = identityService;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // ❗ HttpContext null olabilir; koru
            var httpContext = _httpContextAccessor.HttpContext;

            // ❗ access_token yoksa header yazma
            var accessToken = httpContext != null
                ? await httpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken)
                : null;

            if (!string.IsNullOrEmpty(accessToken))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            }

            var response = await base.SendAsync(request, cancellationToken);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                // 🔄 Refresh dene (bu metot genelde cookie'deki tokenları günceller)
                var tokenResponse = await _identityService.GetRefreshToken();

                if (tokenResponse != null)
                {
                    // ✔️ Güncellenmiş access_token'ı tekrar oku
                    var renewedAccessToken = httpContext != null
                        ? await httpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken)
                        : null;

                    // önceki Authorization'ı temizle (güvenli taraf)
                    request.Headers.Authorization = null;

                    if (!string.IsNullOrEmpty(renewedAccessToken))
                    {
                        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", renewedAccessToken);
                        response = await base.SendAsync(request, cancellationToken);
                    }
                }
            }

            // (İstersen burada hâlâ 401 ise log/hata işleyişi)
            return response;
        }
    }
}
