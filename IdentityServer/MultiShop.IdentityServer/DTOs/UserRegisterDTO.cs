namespace MultiShop.IdentityServer.DTOs
{
    public class UserRegisterDTO //Kullanıcı Kayıt DTO'su
    {
        public string Username { get; set; } // Kullanıcı adı
        public string Password { get; set; } // Şifre
        public string Email { get; set; } // E-posta adresi
        public string Name { get; set; } // Ad
        public string Surname { get; set; } // Soyad
    }
}
