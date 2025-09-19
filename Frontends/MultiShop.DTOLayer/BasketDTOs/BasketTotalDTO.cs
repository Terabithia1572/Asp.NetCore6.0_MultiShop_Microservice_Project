using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.DTOLayer.BasketDTOs
{
    public class BasketTotalDTO
    {
        public string UserID { get; set; } //Kullanıcı ID
        public string DiscountCode { get; set; } //İndirim Kodu
        public int? DiscountRate { get; set; }  // İndirim Oranı
        public List<BasketItemDTO> BasketItems { get; set; } // Sepetteki Ürünler
        public decimal TotalPrice { get => BasketItems.Sum(x => x.ProductPrice * x.ProductQuantity); }
    }
}
