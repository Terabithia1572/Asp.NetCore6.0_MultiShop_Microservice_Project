using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Cargo.DTOLayer.DTOs.CargoCustomerDTOs
{
    public class UpdateCargoCustomerDTO
    {
        public int CargoCustomerID { get; set; } //Kargo Müşteri ID'sini aldık.
        public string CargoCustomerName { get; set; } // Kargo Müşteri Adı
        public string CargoCustomerSurname { get; set; } // Kargo Müşteri Soyadı
        public string CargoCustomerEmail { get; set; } // Kargo Müşteri E-Posta Adresi

        public string CargoCustomerPhone { get; set; } // Kargo Müşteri Telefonu
        public string CargoCustomerCity { get; set; } // Kargo Müşteri Şehri
        public string CargoCustomerDistrict { get; set; } // Kargo Müşteri İlçesi
        public string CargoCustomerAddress { get; set; } // Kargo Müşteri Adresi
        public string UserCustomerID { get; set; } // Kargo Müşteri Kullanıcı ID'si
    }
}
