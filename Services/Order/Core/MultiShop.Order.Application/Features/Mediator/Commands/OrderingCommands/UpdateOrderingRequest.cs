using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Order.Application.Features.Mediator.Commands.OrderingCommands
{
    public class UpdateOrderingRequest:IRequest
    {
        public int OrderingID { get; set; } //Unique olarak her sipariş için bir ID
        public string OrderingUserID { get; set; } // Siparişi veren kullanıcının ID'si
        public decimal OrderingTotalPrice { get; set; } // Siparişin toplam fiyatı
        public DateTime OrderingDate { get; set; } // Siparişin verildiği tarih
    }
}
