using MediatR;
using MultiShop.Order.Application.Features.Mediator.Results.OrderingResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Order.Application.Features.Mediator.Queries.OrderingQueries
{
    public class GetOrderingByUserIDQuery : IRequest<List<GetOrderingByUserIDQueryResult>>
    {
        public string UserID { get; set; }

        public GetOrderingByUserIDQuery(string userID)
        {
            UserID = userID;
        }
    }
}
