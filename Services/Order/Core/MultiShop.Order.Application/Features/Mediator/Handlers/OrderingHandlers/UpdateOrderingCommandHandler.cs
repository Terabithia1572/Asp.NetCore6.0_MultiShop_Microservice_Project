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
    public class UpdateOrderingCommandHandler : IRequestHandler<UpdateOrderingCommands> // MediatR kütüphanesinin IRequestHandler arayüzünü uygular
    {
        private readonly IRepository<Ordering> _orderingRepository; // Ordering (sipariş) ile ilgili veritabanı işlemlerini yapmak için kullanılan generic repository nesnesi.

        public UpdateOrderingCommandHandler(IRepository<Ordering> orderingRepository)
        {
            _orderingRepository = orderingRepository; // Constructor ile repository nesnesi dışarıdan alınır ve private değişkene atanır.
        }

        public async Task Handle(UpdateOrderingCommands request, CancellationToken cancellationToken)
        {
            var values=await _orderingRepository.GetByIDAsync(request.OrderingID); // Siparişin ID'sine göre veritabanından sipariş bilgisi alınır.
            values.OrderingDate = request.OrderingDate;               // Siparişin verildiği tarih güncellenir
            // values.OrderingID = request.OrderingID;                   // Siparişin benzersiz kimliği (ID) güncellenir
            values.OrderingTotalPrice = request.OrderingTotalPrice;   // Siparişin toplam tutarı güncellenir
            values.OrderingUserID = request.OrderingUserID;         // Siparişi veren kullanıcının kimliği (ID) güncellenir
            await _orderingRepository.UpdateAsync(values); // Güncellenen sipariş bilgisi veritabanında güncellenir.
        }
    }
}
