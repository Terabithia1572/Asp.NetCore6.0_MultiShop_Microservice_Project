using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Order.Application.Features.CQRS.Commands.OrderDetailCommands
{
    public class CreateOrderDetailCommand
    {
        public string ProductID { get; set; } // Ürünün benzersiz kimliği
        public string ProductName { get; set; } // Ürünün adı
        public decimal ProductPrice { get; set; } // Ürünün birim fiyatı
        public int ProductAmount { get; set; } // Sipariş edilen ürün miktarı
        public decimal ProductTotalPrice { get; set; } // Ürünün toplam fiyatı (ProductPrice * ProductAmount)
        public int OrderingID { get; set; } // Siparişin benzersiz kimliği, bu sipariş detayının hangi siparişe ait olduğunu belirtir
    }
}
