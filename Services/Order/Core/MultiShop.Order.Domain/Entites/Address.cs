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
        public string Name { get; set; } // Kullanıcının Adı
        public string Surname { get; set; } // Kullanıcının Soyadı
        public string Email { get; set; } // Kullanıcının mail adresi
        public string PhoneNumber { get; set; } // Kullanıcının telefon numarası
        public string Country { get; set; } // Kullanıcının Ülke bilgisi
        public string AddressDistrict { get; set; } // Adresin bulunduğu ilçe
        public string AddressCity { get; set; } // Adresin bulunduğu şehir
        public string AddressDetail1 { get; set; } // Adresin detaylı açıklaması (sokak, apartman, daire numarası vb.)
        public string AddressDetail2 { get; set; } // Adresin detaylı açıklaması (sokak, apartman, daire numarası vb.)
        public string AddressDescription { get; set; } // Adresin açıklaması (örneğin, "Ev adresi", "İş adresi" vb.)
        public string AddressZipCode { get; set; } // Adresin kodu

    }
}
