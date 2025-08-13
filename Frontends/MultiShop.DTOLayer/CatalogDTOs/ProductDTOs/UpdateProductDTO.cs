using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.DTOLayer.CatalogDTOs.ProductDTOs
{
    public class UpdateProductDTO
    {
        public string ProductID { get; set; } // MongoDB'de benzersiz kimlik olarak kullanılacak
        public string ProductName { get; set; } // Ürün Adını Tuttuk.
        public decimal ProductPrice { get; set; } //Ürün Fiyatını Tuttuk.
        public string ProductDescription { get; set; } //Ürün Açıklamasını Tuttuk.
        public string ProductImageURL { get; set; } //Ürün Resmini Tuttuk.
        public string CategoryID { get; set; } // Ürünün ait olduğu kategori ID'sini tuttuk.
    }
}
