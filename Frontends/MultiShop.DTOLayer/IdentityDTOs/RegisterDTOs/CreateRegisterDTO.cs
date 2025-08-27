using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.DTOLayer.IdentityDTOs.RegisterDTOs
{
    public class CreateRegisterDTO
    {
        public string Username { get; set; } // Kullanıcı adı
        public string Password { get; set; } // Şifre
        public string ConfirmPassword { get; set; } // Şifre
        public string Email { get; set; } // E-posta adresi
        public string Name { get; set; } // Ad
        public string Surname { get; set; } // Soyad
    }
}
