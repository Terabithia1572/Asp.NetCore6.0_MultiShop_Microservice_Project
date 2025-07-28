using MediatR;
using MultiShop.Order.Application.Features.Mediator.Commands.OrderingCommands;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Order.Application.Features.Mediator.Handlers.OrderingHandlers
{
    public class CreateOrderingCommandHandler : IRequestHandler<CreateOrderingCommands>
    {
        private readonly IRepository<Ordering> _orderingRepository;
        // Ordering (sipariş) ile ilgili veritabanı işlemlerini yapmak için kullanılan generic repository nesnesi.
        // DI (Dependency Injection) ile dışarıdan alınır ve sınıf içinde kullanılır.
        public CreateOrderingCommandHandler(IRepository<Ordering> orderingRepository)
        {
            _orderingRepository = orderingRepository; // Constructor ile repository nesnesi dışarıdan alınır ve private değişkene atanır.
        }

       
        public async Task Handle(CreateOrderingCommands request, CancellationToken cancellationToken)
        {
            await _orderingRepository.CreateAsync(new Ordering
            {
                OrderingDate = request.OrderingDate,               // Siparişin verildiği tarih    
                OrderingTotalPrice = request.OrderingTotalPrice,   // Siparişin toplam tutarı
                OrderingUserID = request.OrderingUserID            // Siparişi veren kullanıcının kimliği (ID)
            }); // Yeni bir Ordering nesnesi oluşturulur ve repository aracılığıyla veritabanına eklenir.
        }
    }
}
