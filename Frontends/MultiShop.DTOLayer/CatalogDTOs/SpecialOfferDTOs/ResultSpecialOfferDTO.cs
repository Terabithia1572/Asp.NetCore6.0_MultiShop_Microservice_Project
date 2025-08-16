using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.DTOLayer.CatalogDTOs.SpecialOfferDTOs
{
    public class ResultSpecialOfferDTO
    {
        public string SpecialOfferID { get; set; } //Özel Teklif ID
        public string SpecialOfferTitle { get; set; } //Özel Teklif Başlığı
        public string SpecialOfferSubTitle { get; set; } //Özel Teklif alt Başlığı
        public string SpecialOfferImageUrl { get; set; } //Özel Teklif Resim URL
    }
}
