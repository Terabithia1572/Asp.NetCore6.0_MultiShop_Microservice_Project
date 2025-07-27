using MultiShop.Order.Application.Features.CQRS.Queries.OrderDetailQueries;
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
    public class GetOrderDetailByIDQueryHandler
    {
        private readonly IRepository<OrderDetail> _orderDetailRepository;

        public GetOrderDetailByIDQueryHandler(IRepository<OrderDetail> orderDetailRepository)
        {
            _orderDetailRepository = orderDetailRepository;
        }
        public async Task<GetOrderDetailByIDQueryResult> Handle(GetOrderDetailByIDQuery getOrderDetailByIDQuery)
        {
            var values = await _orderDetailRepository.GetByIDAsync(getOrderDetailByIDQuery.OrderDetailID); //OrderDetailID sorgulanıyor
            return new GetOrderDetailByIDQueryResult // Sonuç döndürülüyor
            {
                OrderDetailID = values.OrderDetailID, // Sipariş Detay ID'si
                ProductID = values.ProductID, // Ürünün benzersiz kimliği
                ProductName = values.ProductName, // Ürünün adı
                ProductPrice = values.ProductPrice, // Ürünün birim fiyatı
                ProductAmount = values.ProductAmount, // Sipariş edilen ürün miktarı
                ProductTotalPrice = values.ProductTotalPrice, // Ürünün toplam fiyatı
                OrderingID = values.OrderingID // Siparişin benzersiz kimliği
            };
        }
    }
}
