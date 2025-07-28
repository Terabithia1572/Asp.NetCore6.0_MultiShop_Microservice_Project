using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Order.Application.Features.Mediator.Commands.OrderingCommands
{
    public class RemoveOrderingRequest
    {
        public int OrderingID { get; set; } //Unique olarak her sipariş için bir ID

        public RemoveOrderingRequest(int orderingID) 
        {
            OrderingID = orderingID; // Siparişin ID'si
        }
    }
}
