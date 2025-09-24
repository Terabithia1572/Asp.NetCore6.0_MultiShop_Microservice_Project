using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Order.Application.Features.CQRS.Commands.AddressCommands;
using MultiShop.Order.Application.Features.CQRS.Handlers.AddressHandlers;
using MultiShop.Order.Application.Features.CQRS.Queries.AddressQueries;

namespace MultiShop.Order.WebAPI.Controllers
{
    [Authorize] // Bu controller'a erişim için kimlik doğrulama gereklidir.
    [Route("api/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private readonly GetAddressQueryHandler _getAddressQueryHandler;
        // Tüm adres kayıtlarını listeleyen sorgu (query) işlemlerini yöneten handler nesnesi.

        private readonly GetAddressByIDQueryHandler _getAddressByIDQueryHandler;
        // Belirli bir ID ile adres bilgisini getiren sorgu işlemlerini yöneten handler nesnesi.

        private readonly CreateAddressCommandHandler _createAddressCommandHandler;
        // Yeni adres ekleme (create) işlemlerini yöneten handler nesnesi.

        private readonly UpdateAddressCommandHandler _updateAddressCommandHandler;
        // Var olan adresi güncelleme işlemlerini yöneten handler nesnesi.

        private readonly RemoveAddressCommandHandler _removeAddressCommandHandler;
        // Adres silme (remove) işlemlerini yöneten handler nesnesi.

        public AddressesController(
            GetAddressQueryHandler getAddressQueryHandler,
            GetAddressByIDQueryHandler getAddressByIDQueryHandler,
            CreateAddressCommandHandler createAddressCommandHandler,
            UpdateAddressCommandHandler updateAddressCommandHandler,
            RemoveAddressCommandHandler removeAddressCommandHandler)
        {
            _getAddressQueryHandler = getAddressQueryHandler;
            _getAddressByIDQueryHandler = getAddressByIDQueryHandler;
            _createAddressCommandHandler = createAddressCommandHandler;
            _updateAddressCommandHandler = updateAddressCommandHandler;
            _removeAddressCommandHandler = removeAddressCommandHandler;
            // Handler nesneleri Dependency Injection ile dışarıdan alınır ve private alanlara atanır.
        }

        [HttpGet]
        public async Task<IActionResult> AddressList()
        {
            var values = await _getAddressQueryHandler.Handle();
            // Tüm adreslerin listesini asenkron olarak getirir.
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAddressByID(int id)
        {
            var values = await _getAddressByIDQueryHandler.Handle(new GetAddressByIDQuery(id));
            // Verilen ID'ye sahip adres bilgisini asenkron olarak getirir.
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAddres(CreateAddressCommand createAddressCommand)
        {
            await _createAddressCommandHandler.Handle(createAddressCommand);
            // Gönderilen komut ile yeni adres kaydı oluşturur ve veritabanına ekler.
            return Ok("Adres Bilgisi Başarıyla Eklendi.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAddres(UpdateAddressCommand updateAddressCommand)
        {
            await _updateAddressCommandHandler.Handle(updateAddressCommand);
            // Gönderilen komut ile mevcut adres kaydını günceller.
            return Ok("Adres Bilgisi Başarıyla Güncellendi.");
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveAddres(int id)
        {
            await _removeAddressCommandHandler.Handle(new RemoveAddressCommand(id));
            // Verilen ID'ye sahip adres kaydını siler.
            return Ok("Adres Bilgisi Başarıyla Silindi.");
        }

    }
}
