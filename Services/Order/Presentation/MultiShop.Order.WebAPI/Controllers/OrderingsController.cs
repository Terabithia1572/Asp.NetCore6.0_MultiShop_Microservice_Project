using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Order.Application.Features.Mediator.Commands.OrderingCommands;
using MultiShop.Order.Application.Features.Mediator.Queries.OrderingQueries;

namespace MultiShop.Order.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderingsController : ControllerBase
    {
        private readonly IMediator _mediator;
        // MediatR kütüphanesi ile, komut ve sorguların ilgili handler'lara yönlendirilmesini sağlayan arayüz.
        // Uygulama katmanları arasındaki iletişimi soyutlar, loosely-coupled (gevşek bağlı) yapı sağlar.

        public OrderingsController(IMediator mediator)
        {
            _mediator = mediator;
            // Constructor ile IMediator nesnesi dışarıdan alınır ve sınıf içinde kullanılır.
        }

        // GET: /Orderings
        [HttpGet]
        public async Task<IActionResult> OrderingList()
        {
            var values = await _mediator.Send(new GetOrderingQuery());
            // Tüm siparişleri (Ordering) listelemek için, MediatR aracılığıyla GetOrderingQuery sorgusunu ilgili handler'a gönderir.
            return Ok(values);
        }

        // GET: /Orderings/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderingByID(int id)
        {
            var values = await _mediator.Send(new GetOrderingByIDQuery(id));
            // Belirtilen ID'ye sahip siparişi getirmek için MediatR üzerinden sorgu gönderir.
            return Ok(values);
        }

        // POST: /Orderings
        [HttpPost]
        public async Task<IActionResult> CreateOrdering(CreateOrderingCommands createOrderingCommands)
        {
            await _mediator.Send(createOrderingCommands);
            // Yeni bir sipariş eklemek için, komutu MediatR üzerinden ilgili handler'a iletir.
            return Ok("Sipariş Başarıyla Eklendi..");
        }

        // DELETE: /Orderings/{id}
        [HttpDelete]
        public async Task<IActionResult> RemoveOrdering(int id)
        {
            await _mediator.Send(new RemoveOrderingCommands(id));
            // Belirtilen ID'ye sahip siparişi silmek için komutu MediatR ile ilgili handler'a gönderir.
            return Ok("Sipariş Başarıyla Silindi..");
        }

        // PUT: /Orderings
        [HttpPut]
        public async Task<IActionResult> UpdateOrdering(UpdateOrderingCommands updateOrderingCommands)
        {
            await _mediator.Send(updateOrderingCommands);
            // Var olan bir siparişi güncellemek için komutu MediatR aracılığıyla handler'a gönderir.
            return Ok("Sipariş Başarıyla Güncellendi..");
        }

    }
}
