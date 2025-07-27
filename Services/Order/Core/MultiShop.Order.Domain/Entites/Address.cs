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
        public string AddressDetail { get; set; } // Adresin detaylı açıklaması (sokak, apartman, daire numarası vb.)
    }
}
