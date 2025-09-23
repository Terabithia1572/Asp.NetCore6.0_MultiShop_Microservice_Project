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
    public class UpdateAddressCommandHandler
    {
        private readonly IRepository<Address> _addressRepository;

        public UpdateAddressCommandHandler(IRepository<Address> addressRepository)
        {
            _addressRepository = addressRepository;
        }
        public async Task Handle(UpdateAddressCommand updateAddressCommand)
        {
            var address = await _addressRepository.GetByIDAsync(updateAddressCommand.AddressID); // Adres ID'sine göre adresi al

            if (address == null) // Eğer adres bulunamazsa
            {
                throw new Exception("Adres bulunamadı"); // Hata fırlat
            }

            address.AddressDetail1 = updateAddressCommand.AddressDetail; // Adres detayını güncelle
            address.AddressCity = updateAddressCommand.AddressCity; // Şehir bilgisini güncelle
            address.AddressDistrict = updateAddressCommand.AddressDistrict; // İlçe bilgisini güncelle
            address.AddressUserID = updateAddressCommand.AddressUserID; // Kullanıcı ID'sini güncelle
            await _addressRepository.UpdateAsync(address); // Güncellenmiş adresi veritabanında güncelle
        }
    }
}
