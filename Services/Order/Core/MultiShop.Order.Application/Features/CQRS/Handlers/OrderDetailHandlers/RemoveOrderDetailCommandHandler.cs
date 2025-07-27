using MultiShop.Order.Application.Features.CQRS.Commands.OrderDetailCommands;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Order.Application.Features.CQRS.Handlers.OrderDetailHandlers
{
    public class RemoveOrderDetailCommandHandler
    {
        private readonly IRepository<OrderDetail> _orderDetailRepository;

        public RemoveOrderDetailCommandHandler(IRepository<OrderDetail> orderDetailRepository)
        {
            _orderDetailRepository = orderDetailRepository;
        }
        public async Task Handle(RemoveOrderDetailCommand removeOrderDetailCommand)
        {
            var values = await _orderDetailRepository.GetByIDAsync(removeOrderDetailCommand.OrderDetailID); //OrderDetailID'ye göre adresi al
            await _orderDetailRepository.DeleteAsync(values); // Alınan adresi sil
        }
    }
}
