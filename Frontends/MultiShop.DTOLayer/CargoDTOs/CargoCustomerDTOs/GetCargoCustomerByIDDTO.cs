using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.DTOLayer.CargoDTOs.CargoCustomerDTOs
{
    public class GetCargoCustomerByIDDTO
    {
        public string cargoCustomerName { get; set; } // Kargo Müşteri Adı
        public string cargoCustomerSurname { get; set; } // Kargo Müşteri Soyadı
        public string cargoCustomerEmail { get; set; } // Kargo Müşteri E-posta
        public string cargoCustomerPhone { get; set; } // Kargo Müşteri Telefon
        public string cargoCustomerCity { get; set; } // Kargo Müşteri Şehir
        public string cargoCustomerDistrict { get; set; } // Kargo Müşteri İlçe
        public string cargoCustomerAddress { get; set; } // Kargo Müşteri Adres
        public string userCustomerID { get; set; }  // Kullanıcı Müşteri ID


    }
}
