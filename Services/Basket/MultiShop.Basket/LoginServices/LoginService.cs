namespace MultiShop.Basket.LoginServices
{
    public class LoginService : ILoginService
    {
        private readonly IHttpContextAccessor _httpContextAccessor; // IHttpContextAccessor, HTTP isteklerine erişim sağlar

        public LoginService(IHttpContextAccessor httpContextAccessor) // IHttpContextAccessor, HTTP isteklerine erişim sağlar
        {
            _httpContextAccessor = httpContextAccessor; // IHttpContextAccessor, HTTP isteklerine erişim sağlar
        }

        public string GetUserID => _httpContextAccessor.HttpContext.User.FindFirst("sub").Value; // Kullanıcının ID'sini JWT'den alır
    }
}