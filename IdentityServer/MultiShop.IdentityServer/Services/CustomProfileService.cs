using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using MultiShop.IdentityServer.Models;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MultiShop.IdentityServer.Services
{
    // 🔹 Kullanıcı bilgilerini (profil, isim, e-posta, resim) token'a dahil eden özel servis
    public class CustomProfileService : IProfileService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public CustomProfileService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        // 🔹 Token oluşturulurken hangi claim'lerin ekleneceğini belirliyoruz
        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var user = await _userManager.GetUserAsync(context.Subject);
            if (user == null) return;

            var claims = new List<Claim>
            {
                new Claim("name", user.Name ?? ""),
                new Claim("surname", user.Surname ?? ""),
                new Claim("email", user.Email ?? ""),
                new Claim("profileImage", user.ProfileImageUrl ?? "")
            };

            context.IssuedClaims.AddRange(claims);
        }

        // 🔹 Kullanıcının aktif olup olmadığını kontrol ediyoruz
        public async Task IsActiveAsync(IsActiveContext context)
        {
            var user = await _userManager.GetUserAsync(context.Subject);
            context.IsActive = user != null;
        }
    }
}
