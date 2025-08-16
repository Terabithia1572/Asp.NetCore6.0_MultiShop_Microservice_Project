using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.DTOLayer.CatalogDTOs.SpecialOfferDTOs
{
    public class CreateSpecialOfferDTO
    {
        public string SpecialOfferTitle { get; set; } //Özel Teklif Başlığı
        public string SpecialOfferSubTitle { get; set; } //Özel Teklif alt Başlığı
        public string SpecialOfferImageUrl { get; set; } //Özel Teklif Resim URL
    }
}
