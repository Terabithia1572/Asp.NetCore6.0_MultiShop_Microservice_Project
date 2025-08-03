using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Cargo.DTOLayer.DTOs.CargoDetailDTOs
{
    public class CreateCargoDetailDTO
    {
        public string SenderCustomer { get; set; } //Gönderen Müşteri 
        public string ReceiverCustomer { get; set; } // Alıcı Müşteri 
        public int Barcode { get; set; } //Barkod Numarası
        public int CargoCompanyID { get; set; } //Kargo Şirket ID
    }
}
