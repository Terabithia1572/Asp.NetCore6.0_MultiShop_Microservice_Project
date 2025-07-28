using MediatR;
using MultiShop.Order.Application.Features.Mediator.Results.OrderingResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Order.Application.Features.Mediator.Queries.OrderingQueries
{
    public class GetOrderingByIDQuery:IRequest<GetOrderingByIDQueryResult>
    {
        public int OrderingID { get; set; } // Unique olarak her sipariş için bir ID

        public GetOrderingByIDQuery(int orderingID)
        {
            OrderingID = orderingID; // Siparişin ID'si
        } 
    }
}
