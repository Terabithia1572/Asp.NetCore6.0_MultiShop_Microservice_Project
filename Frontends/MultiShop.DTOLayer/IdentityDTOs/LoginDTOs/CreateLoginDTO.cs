using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.DTOLayer.IdentityDTOs.LoginDTOs
{
    public class CreateLoginDTO
    {
        public string UserName { get; set; } // Kullanıcı adı
        public string Password { get; set; } // Şifre
    }
}
