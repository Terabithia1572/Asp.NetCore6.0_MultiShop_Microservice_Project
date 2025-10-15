using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.DTOLayer.CatalogDTOs.ProductDiscountDTOs
{
    public class ResultProductDiscountDTO
    {
        public string ProductDiscountID { get; set; }
        public string ProductID { get; set; }
        public decimal DiscountRate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
        public string? ProductName { get; set; } // 🆕 Ürün adı (UI'de göstermek için)
        public string? ProductImageURL { get; set; } // 🆕 Ürün adı (UI'de göstermek için)

    }
}
