using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.DTOLayer.IdentityDTOs
{
    public class SimpleUserDTO
    {
        public string Id { get; set; }
        public string FullName => $"{Name} {Surname}";
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
    }
}
