namespace MultiShop.IdentityServer.DTOs
{
    public class UserLoginResponseDTO
    {
        public string Token { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string ImageUrl { get; set; }
        public string Email { get; set; }
    }

}
