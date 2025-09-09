using MultiShop.WebUI.Services.Interfaces;
using System.Security.Claims;

namespace MultiShop.WebUI.Services.Concrete
{
    public class LoginService : IloginService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoginService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetUserID =>_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
    }
}
