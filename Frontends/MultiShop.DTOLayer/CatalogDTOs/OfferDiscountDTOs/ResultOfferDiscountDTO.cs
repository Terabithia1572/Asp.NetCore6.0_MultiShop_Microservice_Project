using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.DTOLayer.CatalogDTOs.OfferDiscountDTOs
{
    public class ResultOfferDiscountDTO
    {
        public string OfferDiscountID { get; set; } //Özel Teklif ID
        public string OfferDiscountTitle { get; set; } //Özel Teklif Başlığı
        public string OfferDiscountSubTitle { get; set; } //Özel Teklif alt Başlığı
        public string OfferDiscountImageUrl { get; set; } //Özel Teklif Resim URL
        public string OfferDiscountButtonTitle { get; set; } // Özel Teklif Buton Başlığı
    }
}
