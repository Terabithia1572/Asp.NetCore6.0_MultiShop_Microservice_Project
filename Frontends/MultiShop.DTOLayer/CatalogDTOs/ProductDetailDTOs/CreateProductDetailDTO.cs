using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.DTOLayer.CatalogDTOs.ProductDetailDTOs
{
    public class CreateProductDetailDTO
    {
        public string ProductDetailDescription { get; set; } // Ürün Detay Açıklamasını Tuttuk.
        public string ProductDetailInfo { get; set; } //Ürün Detay Bilgilerini Tuttuk.
        public string ProductID { get; set; } //Ürün ID
    }
}
