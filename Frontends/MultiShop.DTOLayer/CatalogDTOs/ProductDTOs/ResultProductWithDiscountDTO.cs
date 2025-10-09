using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.DTOLayer.CatalogDTOs.ProductDTOs
{
    public class ResultProductWithDiscountDTO
    {
        public string ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public string ProductImageURL { get; set; }
        public decimal? DiscountRate { get; set; }

        public decimal DiscountedPrice =>
            DiscountRate.HasValue
                ? ProductPrice - (ProductPrice * DiscountRate.Value / 100)
                : ProductPrice;
    }
}
    


