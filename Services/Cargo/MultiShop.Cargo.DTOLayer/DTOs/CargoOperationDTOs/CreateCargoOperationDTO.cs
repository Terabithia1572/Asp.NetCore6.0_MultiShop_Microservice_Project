using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Cargo.DTOLayer.DTOs.CargoOperationDTOs
{
    public class CreateCargoOperationDTO
    {
        public string Barcode { get; set; } //Kargo Barkod numarası
        public string CargoOperationDescription { get; set; } //Kargo Hareket Açıklaması
        public DateTime CargoOperationDate { get; set; } //Kargo Hareket Tarihi
    }
}
