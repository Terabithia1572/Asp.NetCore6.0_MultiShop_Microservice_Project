using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.DTOLayer.CatalogDTOs.ProductImageDTOs
{
    public class CreateProductImageDTO
    {
        public string ProductImage1 { get; set; } //Birden fazla ürünün resmi varsa yolunu birden fazla tabloda tutacağız.
        public string ProductImage2 { get; set; } //İkinci resim için
        public string ProductImage3 { get; set; } //Üçüncü resim için
        public string ProductImage4 { get; set; } //Dördüncü resim için
        public string ProductID { get; set; } //Ürün ile ilişkili hale getirmek için ProductID ekledik.
                                              //   public Product Product { get; set; } //Product tablosuyla ilişki ekledik.
    }
}
