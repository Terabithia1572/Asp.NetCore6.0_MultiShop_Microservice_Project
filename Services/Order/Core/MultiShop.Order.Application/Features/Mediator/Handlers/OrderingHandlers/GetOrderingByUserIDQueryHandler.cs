using MediatR;
using MultiShop.Order.Application.Features.Mediator.Queries.OrderingQueries;
using MultiShop.Order.Application.Features.Mediator.Results.OrderingResults;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Order.Application.Features.Mediator.Handlers.OrderingHandlers
{
    public class GetOrderingByUserIDQueryHandler : IRequestHandler<GetOrderingByUserIDQuery, List<GetOrderingByUserIDQueryResult>>
    {
        //private readonly IRepository<Ordering> _orderingRepository;

        //public GetOrderingByUserIDQueryHandler(IRepository<Ordering> orderingRepository)
        //{
        //    _orderingRepository = orderingRepository;
        //}

        public async Task<List<GetOrderingByUserIDQueryResult>> Handle(GetOrderingByUserIDQuery request, CancellationToken cancellationToken)
        {
            //var values =await _orderingRepository.GetByIDAsync(request.);
            //return new GetOrderingByUserIDQueryResult
            //{
            //    OrderingDate = values.OrderingDate,
            //    OrderingID = values.OrderingID,
            //    OrderingTotalPrice = values.OrderingTotalPrice,
            //    OrderingUserID = values.OrderingUserID
            //};
        }
    }
}

