using System;

namespace MultiShop.DTOLayer.OrderDTOs.OrderingDTO
{
    public class CreateOrderingDTO
    {
        public string OrderingUserID { get; set; }  // Kullanıcı ID
        public decimal OrderingTotalPrice { get; set; } // Toplam Tutar
        public DateTime OrderingDate { get; set; } // Sipariş Tarihi
    }
}
