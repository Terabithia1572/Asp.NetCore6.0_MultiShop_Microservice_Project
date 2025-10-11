using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.DTOLayer.CatalogDTOs.ProductDTOs
{
    public class ResultProductWithDiscountDTO
    {
        public string ProductID { get; set; }              // Ürünün benzersiz ID'si (Mongo ObjectId string)
        public string ProductName { get; set; }            // Ürün adı
        public decimal ProductPrice { get; set; }          // Orijinal fiyat
        public string ProductImageURL { get; set; }        // Görsel URL

        public decimal? DiscountRate { get; set; }         // İndirim oranı (örn: 25 => %25), yoksa null

        // ⬇️ ÖNEMLİ: set; eklendi ki JSON’dan deserialize edilebilsin ve
        // fallback path’te (indirim yoksa) ProductPrice’ı buraya atayabilelim.
        public decimal DiscountedPrice { get; set; }       // İndirim uygulanmış fiyat (yoksa = ProductPrice)
    }
}


