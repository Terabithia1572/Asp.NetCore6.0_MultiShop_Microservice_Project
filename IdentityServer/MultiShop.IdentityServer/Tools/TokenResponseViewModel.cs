using System;

namespace MultiShop.IdentityServer.Tools
{
    public class TokenResponseViewModel
    {
        public TokenResponseViewModel(string token, DateTime expireDate)
        {
            Token = token;
            ExpireDate = expireDate;
        }

        public string Token { get; set; } // Token değeri
        public DateTime ExpireDate { get; set; } // Token'ın geçerlilik süresi
    }
}
