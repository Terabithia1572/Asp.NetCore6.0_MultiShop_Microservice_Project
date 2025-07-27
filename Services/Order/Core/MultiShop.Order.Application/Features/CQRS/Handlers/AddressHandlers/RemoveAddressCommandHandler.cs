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
    public class RemoveAddressCommandHandler
    {
        private readonly IRepository<Address> _addressRepository;

        public RemoveAddressCommandHandler(IRepository<Address> addressRepository)
        {
            _addressRepository = addressRepository;
        }
        public async Task Handle(RemoveAddressCommand removeAddressCommand)
        {
            var values=await _addressRepository.GetByIDAsync(removeAddressCommand.AddressID); //AddressID'ye göre adresi al
            await _addressRepository.DeleteAsync(values); // Alınan adresi sil
        }
    }

}
