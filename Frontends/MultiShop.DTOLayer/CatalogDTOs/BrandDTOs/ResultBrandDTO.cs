using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.DTOLayer.CatalogDTOs.BrandDTOs
{
    public class ResultBrandDTO
    {
        public string BrandID { get; set; } // MongoDB'de benzersiz kimlik olarak kullanılacak
        public string BrandName { get; set; } //Marka Adını Tuttuk.
        public string BrandImageURL { get; set; } //Marka Resim Görselini Tuttuk.
    }
}
