using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.DTOLayer.CatalogDTOs.FeatureSliderDTOs
{
    public class UpdateFeatureSliderDTO
    {
        public string FeatureSliderID { get; set; }
        public string FeatureSliderTitle { get; set; } //Feature Adını Tuttuk.
        public string FeatureSliderDescription { get; set; } //Feature Açıklamasını Tuttuk.
        public string FeatureSliderImageURL { get; set; } //Feature Resim Url'sini Tuttuk.
        public bool FeatureSliderStatus { get; set; } // 
    }
}
