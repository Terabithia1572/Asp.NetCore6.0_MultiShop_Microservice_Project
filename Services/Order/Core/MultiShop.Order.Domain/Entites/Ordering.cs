using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Order.Domain.Entites
{
    public class Ordering
    {
        public int OrderingID { get; set; } //Unique olarak her sipariş için bir ID
        public string OrderingUserID { get; set; } // Siparişi veren kullanıcının ID'si
        public  decimal OrderingTotalPrice { get; set; } // Siparişin toplam fiyatı
        public DateTime OrderingDate { get; set; } // Siparişin verildiği tarih
        public List<OrderDetail> OrderDetails { get; set; } // Siparişin detayları (ürünler, miktarlar, fiyatlar vb.)
    }
}
