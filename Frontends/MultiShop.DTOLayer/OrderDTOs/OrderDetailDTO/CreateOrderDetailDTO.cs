using System;

namespace MultiShop.DTOLayer.OrderDTOs.OrderDetailDTO
{
    public class CreateOrderDetailDTO
    {
        public string ProductID { get; set; } // Ürün ID'si
        public string ProductName { get; set; } // Ürün Adı
        public decimal ProductPrice { get; set; } // Ürün Fiyatı
        public int ProductQuantity { get; set; } // Ürün Adedi
        public decimal ProductTotalPrice { get; set; } // Ürün Toplam Tutarı
        public int OrderingID { get; set; } // Ordering tablosuna foreign key
    }
}
