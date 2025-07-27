using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Order.Application.Features.CQRS.Queries.OrderDetailQueries
{
    public class GetOrderDetailByIDQuery
    {
        public int OrderDetailID { get; set; } // Sipariş Detay ID'si, her sipariş detayı için benzersiz bir kimlik

        public GetOrderDetailByIDQuery(int orderDetailID) //Constructor geçerli sipariş detayını almak için kullanılır
        {
            OrderDetailID = orderDetailID; // Sipariş Detay ID'sini alır
        }
    }
}
