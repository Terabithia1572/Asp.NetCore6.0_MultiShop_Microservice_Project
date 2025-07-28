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
    public class GetOrderingByIDQueryHandler : IRequestHandler<GetOrderingByIDQuery, GetOrderingByIDQueryResult> 
        // GetOrderingByIDQueryHandler sınıfı, MediatR kütüphanesinin IRequestHandler arayüzünü uygular.
    {
        private readonly IRepository<Ordering> _orderingRepository;
        // Ordering (sipariş) ile ilgili veritabanı işlemlerini yapmak için kullanılan generic repository nesnesi.
        // DI (Dependency Injection) ile dışarıdan alınır ve sınıf içinde kullanılır.
        public GetOrderingByIDQueryHandler(IRepository<Ordering> orderingRepository)
        {
            _orderingRepository = orderingRepository; // Constructor ile repository nesnesi dışarıdan alınır ve private değişkene atanır.
        }

        public async Task<GetOrderingByIDQueryResult> Handle(GetOrderingByIDQuery request, CancellationToken cancellationToken)
        {
            var values=await _orderingRepository.GetByIDAsync(request.OrderingID);
            return new GetOrderingByIDQueryResult
            {
                OrderingDate = values.OrderingDate,               // Siparişin verildiği tarih
                OrderingID = values.OrderingID,                   // Siparişin benzersiz kimliği (ID)
                OrderingTotalPrice = values.OrderingTotalPrice,   // Siparişin toplam tutarı
                OrderingUserID = values.OrderingUserID            // Siparişi veren kullanıcının kimliği (ID)
            };
        }
    }
}
