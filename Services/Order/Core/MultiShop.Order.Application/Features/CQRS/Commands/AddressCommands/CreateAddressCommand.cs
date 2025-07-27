using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Order.Application.Features.CQRS.Commands.AddressCommands
{
    public class CreateAddressCommand
    {
        public string AddressUserID { get; set; } // Adresi kullanan kullanıcının benzersiz kimliği
        public string AddressDistrict { get; set; } // Adresin bulunduğu ilçe
        public string AddressCity { get; set; } // Adresin bulunduğu şehir
        public string AddressDetail { get; set; } // Adresin detaylı açıklaması (sokak, apartman, daire numarası vb.)
    }
}
