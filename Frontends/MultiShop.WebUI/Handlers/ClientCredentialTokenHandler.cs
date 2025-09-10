
using MultiShop.WebUI.Services.Interfaces;
using System.Net;
using System.Net.Http.Headers;

namespace MultiShop.WebUI.Handlers
{
    public class ClientCredentialTokenHandler:DelegatingHandler
    {
        private readonly IClientCredentialTokenService _clientCredentialTokenService; //Client Credential Token'ı alacak service

        public ClientCredentialTokenHandler(IClientCredentialTokenService clientCredentialTokenService) //Dependency Injection
        {
            _clientCredentialTokenService = clientCredentialTokenService; //Client Credential Token'ı alacak service
        } 

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer",await _clientCredentialTokenService.GetToken()); //Client Credential Token'ı request'in header'ına ekle
            var response= await base.SendAsync(request, cancellationToken);
            if(response.StatusCode==HttpStatusCode.Unauthorized) //Eğer response 401 dönerse
            {
                //hata yönetimi yapılabilir
            }
            return response; //Response'u döndür
        }
    }
}
