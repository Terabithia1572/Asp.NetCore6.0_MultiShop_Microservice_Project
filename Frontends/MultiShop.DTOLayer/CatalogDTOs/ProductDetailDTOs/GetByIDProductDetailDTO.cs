using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.DTOLayer.CatalogDTOs.ProductDetailDTOs
{
    public class GetByIDProductDetailDTO
    {
        public string ProductDetailID { get; set; } // MongoDB'de benzersiz kimlik olarak kullanılacak
        public string ProductDetailDescription { get; set; } // Ürün Detay Açıklamasını Tuttuk.
        public string ProductDetailInfo { get; set; } //Ürün Detay Bilgilerini Tuttuk.
        public string ProductID { get; set; } //Ürün ID
    }
}
