using MultiShop.Order.Application.Features.CQRS.Commands.AddressCommands;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Order.Application.Features.CQRS.Handlers.AddressHandlers
{
    public class CreateAddressCommandHandler
    {
        private readonly IRepository<Address> _addressRepository;
        // Address nesneleriyle ilgili veri tabanı işlemlerini gerçekleştirmek için kullanılan generic repository (depo) arayüzüdür.
        // Dependency Injection ile dışarıdan alınır ve sınıf içinde kullanılır.

        public CreateAddressCommandHandler(IRepository<Address> addressRepository)
        {
            _addressRepository = addressRepository;
            // Constructor ile repository nesnesi dışarıdan alınır ve private alana atanır.
        }

        public async Task Handle(CreateAddressCommand createAddressCommand)
        {
            // CreateAddressCommand ile gelen adres bilgilerine göre yeni bir Address nesnesi oluşturur ve veri tabanına ekler.
            await _addressRepository.CreateAsync(new Address
            {
                AddressUserID = createAddressCommand.AddressUserID,       // Kullanıcı ID'si atanır.
                AddressDistrict = createAddressCommand.AddressDistrict,   // İlçe bilgisi atanır.
                AddressCity = createAddressCommand.AddressCity,           // Şehir bilgisi atanır.
                AddressDetail = createAddressCommand.AddressDetail        // Adres detayı atanır.
            });
            // Oluşturulan yeni Address nesnesi asenkron olarak veri tabanına kaydedilir.
        }

    }
}
