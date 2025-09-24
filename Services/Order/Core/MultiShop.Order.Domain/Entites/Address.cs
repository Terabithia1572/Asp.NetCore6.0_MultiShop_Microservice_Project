using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Order.Domain.Entites
{
    public class Address
    {
        public int AddressID { get; set; } // Adresin benzersiz kimliği
        public string AddressUserID { get; set; } // Adresi kullanan kullanıcının benzersiz kimliği
        public string AddressDistrict { get; set; } // Adresin bulunduğu ilçe
        public string AddressCity { get; set; } // Adresin bulunduğu şehir
        public string AddressDetail1 { get; set; } // Adresin detaylı açıklaması (sokak, apartman, daire numarası vb.)
        public string AddressDetail2 { get; set; } // Adresin ek detay açıklaması (varsa)
        public string AddressDescription { get; set; } // 
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
        public string AddressZipCode { get; set; }
    }
}
