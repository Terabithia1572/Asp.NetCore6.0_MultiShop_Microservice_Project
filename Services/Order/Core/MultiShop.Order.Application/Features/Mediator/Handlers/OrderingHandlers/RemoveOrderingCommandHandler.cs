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
    public class RemoveOrderingCommandHandler : IRequestHandler<RemoveOrderingCommands> // MediatR kütüphanesinin IRequestHandler arayüzünü uygular
    {
        private readonly IRepository<Ordering> _orderingRepository; // Ordering (sipariş) ile ilgili veritabanı işlemlerini yapmak için kullanılan generic repository nesnesi.

        public RemoveOrderingCommandHandler(IRepository<Ordering> orderingRepository)
        {
            _orderingRepository = orderingRepository; // Constructor ile repository nesnesi dışarıdan alınır ve private değişkene atanır.
        }

        public async Task Handle(RemoveOrderingCommands request, CancellationToken cancellationToken)
        {
            var values=await _orderingRepository.GetByIDAsync(request.OrderingID);   // Siparişin ID'sine göre veritabanından sipariş bilgisi alınır.
            await _orderingRepository.DeleteAsync(values); // Siparişin ID'sine göre veritabanından silme işlemi yapılır.
        }
    }
}
