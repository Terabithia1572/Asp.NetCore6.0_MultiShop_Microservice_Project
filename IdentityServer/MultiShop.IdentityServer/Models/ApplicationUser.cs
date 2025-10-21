using Microsoft.AspNetCore.Identity;
using System;

namespace MultiShop.IdentityServer.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string About { get; set; }
        public string City { get; set; }
        public string Gender { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public string ProfileImageUrl { get; set; }
        public DateTime RegisterDate { get; set; }
    }
}
