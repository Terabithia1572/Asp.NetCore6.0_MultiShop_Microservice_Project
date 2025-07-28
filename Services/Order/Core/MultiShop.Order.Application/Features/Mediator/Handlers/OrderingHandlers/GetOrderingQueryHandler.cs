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
    public class GetOrderingQueryHandler : IRequestHandler<GetOrderingQuery, List<GetOrderingQueryResult>>
    // GetOrderingQueryHandler sınıfı, MediatR kütüphanesinin IRequestHandler arayüzünü uygular.
    // Bu sınıf, GetOrderingQuery sorgusunu (query) işleyip, sonucunda List<GetOrderingQueryResult> tipinde bir liste döndürmekle görevlidir.
    {
        private readonly IRepository<Ordering> _orderingRepository;
        // Ordering (sipariş) ile ilgili veritabanı işlemlerini yapmak için kullanılan generic repository nesnesi.
        // DI (Dependency Injection) ile dışarıdan alınır ve sınıf içinde kullanılır.

        public GetOrderingQueryHandler(IRepository<Ordering> orderingRepository)
        {
            _orderingRepository = orderingRepository;
            // Constructor ile repository nesnesi dışarıdan alınır ve private değişkene atanır.
        }

        public async Task<List<GetOrderingQueryResult>> Handle(GetOrderingQuery request, CancellationToken cancellationToken)
        {
            var values = await _orderingRepository.GetAllAsync();
            // Tüm Ordering (sipariş) kayıtlarını asenkron olarak veritabanından getirir.

            return values.Select(x => new GetOrderingQueryResult
            {
                OrderingDate = x.OrderingDate,               // Siparişin verildiği tarih
                OrderingID = x.OrderingID,                   // Siparişin benzersiz kimliği (ID)
                OrderingTotalPrice = x.OrderingTotalPrice,   // Siparişin toplam tutarı
                OrderingUserID = x.OrderingUserID            // Siparişi veren kullanıcının kimliği (ID)
            }).ToList();
            // Her siparişi GetOrderingQueryResult tipine dönüştürür ve bir liste olarak döner.
        }

    }
}
