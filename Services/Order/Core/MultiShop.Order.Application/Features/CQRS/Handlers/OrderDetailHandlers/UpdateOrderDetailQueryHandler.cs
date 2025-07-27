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
    public class UpdateOrderDetailQueryHandler
    {
        private readonly IRepository<OrderDetail> _orderDetailRepository;

        public UpdateOrderDetailQueryHandler(IRepository<OrderDetail> orderDetailRepository)
        {
            _orderDetailRepository = orderDetailRepository;
        }
        public async Task Handle(UpdateOrderDetailCommand updateOrderDetailCommand)
        {
            var orderDetail = await _orderDetailRepository.GetByIDAsync(updateOrderDetailCommand.OrderDetailID); // Adres ID'sine göre adresi al

            if (orderDetail == null) // Eğer adres bulunamazsa
            {
                throw new Exception("Adres bulunamadı"); // Hata fırlat
            }

            orderDetail.OrderingID = updateOrderDetailCommand.OrderingID; // OrderID'yi güncelle
            orderDetail.ProductID = updateOrderDetailCommand.ProductID; // ProductID'yi güncelle         
            orderDetail.ProductPrice = updateOrderDetailCommand.ProductPrice; // Price'ı güncelle
            orderDetail.ProductName = updateOrderDetailCommand.ProductName; // ProductName'i güncelle
            orderDetail.ProductTotalPrice = updateOrderDetailCommand.ProductTotalPrice; // ProductTotalPrice'ı güncelle
            orderDetail.ProductAmount = updateOrderDetailCommand.ProductAmount; // ProductAmount'u güncelle
            await _orderDetailRepository.UpdateAsync(orderDetail); // Güncellenmiş adresi veritabanında güncelle
        }
    }
}
