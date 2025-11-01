using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.DTOLayer.IdentityDTOs.UserDTOs
{
    public class ResultUserDTO
    {

        // ⚠️ Kimlik
        public string id { get; set; }
        public string userName { get; set; }

        // 👤 Profil
        public string name { get; set; }
        public string surname { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }
        public string city { get; set; }
        public string gender { get; set; }
        public string about { get; set; }

        // 📷 Görsel (okuma için)
        public string profileImageUrl { get; set; }

        // 🔑 Şifre (sadece gönderimde opsiyonel)
        public string NewPassword { get; set; }

        // Aşağıdakiler Identity’nin kendi alanları; istersen tutabilirsin
        public string normalizedUserName { get; set; }
        public string normalizedEmail { get; set; }
        public bool emailConfirmed { get; set; }
        public string passwordHash { get; set; }
        public string securityStamp { get; set; }
        public string concurrencyStamp { get; set; }
        public bool phoneNumberConfirmed { get; set; }
        public bool twoFactorEnabled { get; set; }
        public object lockoutEnd { get; set; }
        public bool lockoutEnabled { get; set; }
        public int accessFailedCount { get; set; }

    }
}

