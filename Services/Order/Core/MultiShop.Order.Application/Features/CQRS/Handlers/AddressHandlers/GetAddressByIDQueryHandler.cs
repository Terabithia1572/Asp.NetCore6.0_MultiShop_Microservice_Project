using MultiShop.Order.Application.Features.CQRS.Queries.AddressQueries;
using MultiShop.Order.Application.Features.CQRS.Results.AddressResults;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Order.Application.Features.CQRS.Handlers.AddressHandlers
{
    public class GetAddressByIDQueryHandler
    {
        private readonly IRepository<Address> _addressRepository;

        public GetAddressByIDQueryHandler(IRepository<Address> addressRepository)
        {
            _addressRepository = addressRepository;
        }
        public async Task<GetAddressByIDQueryResult> Handle(GetAddressByIDQuery getAddressByIDQuery)
        {
            var values = await _addressRepository.GetByIDAsync(getAddressByIDQuery.AddressID); //AddressID sorgulanıyor
            return new GetAddressByIDQueryResult // Sonuç döndürülüyor
            {
                AddressID = values.AddressID, // AddressID bilgisi
                AddressCity = values.AddressCity, // AddressCity bilgisi
                AddressDetail = values.AddressDetail, // AddressDetail bilgisi   
                AddressDistrict = values.AddressDistrict, // AddressDistrict bilgisi
                AddressUserID = values.AddressUserID // AddressUserID bilgisi
            };
        }
    }
}
