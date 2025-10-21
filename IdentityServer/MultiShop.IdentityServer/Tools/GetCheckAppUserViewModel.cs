namespace MultiShop.IdentityServer.Tools
{
    public class GetCheckAppUserViewModel
    {
        public string ID { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
        public bool IsExist { get; set; }

        // 🔥 Kullanıcının profil fotoğrafı URL'si (token'a claim olarak eklenecek)
        public string ProfileImageUrl { get; set; }
    }
}
