namespace MultiShop.IdentityServer.Tools
{
    public class JwtTokenDefaults
    {
        public const string ValidAudience = "http:/localhost"; //Token'ın geçerli olduğu kitle
        public const string ValidIssuer = "http:/localhost"; //Token'ı veren
        public const string Key="MultiShop..0102030405Asp.NetCore6.0.3.6.5IdentityServer"; //Token'ı imzalamak için kullanılan anahtar
        public const int Expire=60; //dakika

    }
}
