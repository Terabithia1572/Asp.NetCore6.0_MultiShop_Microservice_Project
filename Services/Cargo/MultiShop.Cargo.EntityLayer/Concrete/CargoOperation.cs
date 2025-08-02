using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Cargo.EntityLayer.Concrete
{
    public class CargoOperation
    {
        public int CargoOperationID { get; set; } //Kargo Hareketleri ID
        public string Barcode { get; set; } //Kargo Barkod numarası
        public string CargoOperationDescription { get; set; } //Kargo Hareket Açıklaması
        public DateTime CargoOperationDate { get; set; } //Kargo Hareket Tarihi
    }
}
