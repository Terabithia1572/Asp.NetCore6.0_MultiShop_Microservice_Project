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
    public class CreateOrderDetailCommandHandler
    {
        private readonly IRepository<OrderDetail> _orderDetailRepository; // Sipariş detaylarını yöneten repository

        public CreateOrderDetailCommandHandler(IRepository<OrderDetail> orderDetailRepository) // Constructor, sipariş detaylarını yöneten repository'i alır
        {
            _orderDetailRepository = orderDetailRepository; // Sipariş detaylarını yöneten repository'i başlatır
        }
        public async Task Handle(CreateOrderDetailCommand createOrderDetailCommand) // CreateOrderDetailCommand nesnesini alır
        { 
            await _orderDetailRepository.CreateAsync(new OrderDetail // Yeni bir OrderDetail nesnesi oluşturur
            {
                ProductID = createOrderDetailCommand.ProductID, // Ürünün benzersiz kimliğini alır
                ProductName = createOrderDetailCommand.ProductName, // Ürünün adını alır
                ProductPrice = createOrderDetailCommand.ProductPrice, // Ürünün birim fiyatını alır
                ProductAmount = createOrderDetailCommand.ProductAmount, // Sipariş edilen ürün miktarını alır
                ProductTotalPrice = createOrderDetailCommand.ProductTotalPrice, // Ürünün toplam fiyatını alır
                OrderingID = createOrderDetailCommand.OrderingID // Siparişin benzersiz kimliğini alır
            });
        }
    }
}
