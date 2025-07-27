using MultiShop.Order.Application.Features.CQRS.Results.OrderDetailResults;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Order.Application.Features.CQRS.Handlers.OrderDetailHandlers
{
    public class GetOrderDetailQueryHandler
    { 
        private readonly IRepository<OrderDetail> _orderDetailRepository; // Sipariş detaylarını yöneten repository

        public GetOrderDetailQueryHandler(IRepository<OrderDetail> orderDetailRepository) // Constructor, sipariş detaylarını yöneten repository'i alır
        {
            _orderDetailRepository = orderDetailRepository; // Sipariş detaylarını yöneten repository'i başlatır
        }
        public async Task<List<GetOrderDetailQueryResult>> Handle()
        {
            var values=await _orderDetailRepository.GetAllAsync(); // Tüm sipariş detaylarını alır
            return values.Select(x => new GetOrderDetailQueryResult
            {
                OrderDetailID = x.OrderDetailID, // Sipariş Detay ID'sini alır
                ProductID = x.ProductID, // Ürünün benzersiz kimliğini alır
                ProductName = x.ProductName, // Ürünün adını alır
                ProductPrice = x.ProductPrice, // Ürünün birim fiyatını alır
                ProductAmount = x.ProductAmount, // Sipariş edilen ürün miktarını alır
                ProductTotalPrice = x.ProductTotalPrice, // Ürünün toplam fiyatını alır
                OrderingID = x.OrderingID // Siparişin benzersiz kimliğini alır
            }).ToList(); // Sonuçları liste olarak döner
        }
    }
}
