using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Order.Application.Features.CQRS.Commands.OrderDetailCommands;
using MultiShop.Order.Application.Features.CQRS.Handlers.OrderDetailHandlers;
using MultiShop.Order.Application.Features.CQRS.Queries.OrderDetailQueries;

namespace MultiShop.Order.WebAPI.Controllers
{
    [Authorize] // Bu controller'a erişim için kimlik doğrulama gereklidir.
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailsController : ControllerBase
    {
        private readonly GetOrderDetailByIDQueryHandler _getOrderDetailByIDQueryHandler; // Sipariş detaylarını ID'ye göre getiren sorgu işleyici
        private readonly GetOrderDetailQueryHandler _getOrderDetailQueryHandler; // Tüm sipariş detaylarını getiren sorgu işleyici
        private readonly CreateOrderDetailCommandHandler _createOrderDetailCommandHandler; // Yeni sipariş detayı ekleyen komut işleyici
        private readonly UpdateOrderDetailCommandHandler _updateOrderDetailCommandHandler; // Var olan sipariş detayını güncelleyen komut işleyici
        private readonly RemoveOrderDetailCommandHandler _removeOrderDetailCommandHandler; // Sipariş detayını silen komut işleyici

        public OrderDetailsController(GetOrderDetailByIDQueryHandler getOrderDetailByIDQueryHandler, GetOrderDetailQueryHandler getOrderDetailQueryHandler, CreateOrderDetailCommandHandler createOrderDetailCommandHandler, UpdateOrderDetailCommandHandler updateOrderDetailCommandHandler, RemoveOrderDetailCommandHandler removeOrderDetailCommandHandler)
        {
            _getOrderDetailByIDQueryHandler = getOrderDetailByIDQueryHandler;
            _getOrderDetailQueryHandler = getOrderDetailQueryHandler;
            _createOrderDetailCommandHandler = createOrderDetailCommandHandler;
            _updateOrderDetailCommandHandler = updateOrderDetailCommandHandler;
            _removeOrderDetailCommandHandler = removeOrderDetailCommandHandler;
        }

        [HttpGet]
        public async Task<IActionResult> OrderDetailList()
        {
            var values = await _getOrderDetailQueryHandler.Handle();
            // Tüm sipariş detaylarının listesini asenkron olarak getirir.
            return Ok(values);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderDetailByID(int id)
        {
            var values = await _getOrderDetailByIDQueryHandler.Handle(new GetOrderDetailByIDQuery(id));
            // Verilen ID'ye sahip sipariş detay bilgisini asenkron olarak getirir.
            return Ok(values);
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrderDetail(CreateOrderDetailCommand createOrderDetailCommand)
        {
            await _createOrderDetailCommandHandler.Handle(createOrderDetailCommand);
            // Yeni sipariş detayı ekleme işlemini asenkron olarak gerçekleştirir.
            return Ok("Sipariş Detayı başarıyla eklendi..");
        }
        [HttpPut]
        public async Task<IActionResult> UpdateOrderDetail(UpdateOrderDetailCommand updateOrderDetailCommand)
        {
            await _updateOrderDetailCommandHandler.Handle(updateOrderDetailCommand);
            // Var olan sipariş detayını güncelleme işlemini asenkron olarak gerçekleştirir.
            return Ok("Sipariş Detayı başarıyla güncellendi..");
        }
        [HttpDelete]
        public async Task<IActionResult> RemoveOrderDetail(int id)
        {
            await _removeOrderDetailCommandHandler.Handle(new RemoveOrderDetailCommand(id));
            // Verilen ID'ye sahip sipariş detayını silme işlemini asenkron olarak gerçekleştirir.
            return Ok("Sipariş Detayı başarıyla silindi..");

        }
    }
}
