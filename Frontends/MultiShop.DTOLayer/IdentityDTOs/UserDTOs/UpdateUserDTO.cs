namespace MultiShop.DTOLayer.IdentityDTOs.UserDTOs
{
    public class UpdateUserDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string City { get; set; }
        public string Gender { get; set; }
        public string About { get; set; }
        public string? NewPassword { get; set; }
        public string? ProfileImageUrl { get; set; } // ⬅️ önemli
    }
}
