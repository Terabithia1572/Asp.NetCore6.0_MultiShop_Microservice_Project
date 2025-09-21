using System.Security.Claims;
using MultiShop.WebUI.Services.Interfaces;

namespace MultiShop.WebUI.Services.Concrete
{
    public class LoginService : IloginService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public LoginService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }
        public string GetUserID => _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
    }
}