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
    public class GetAddressQueryHandler
    {
        private readonly IRepository<Address> _addressRepository; // Repository arayüzü kullanılarak Address nesneleri için veri erişimi sağlanır

        public GetAddressQueryHandler(IRepository<Address> addressRepository)
        {
            _addressRepository = addressRepository;
        }
        public async Task<List<GetAddressQueryResult>> Handle()
        {
            var values = await _addressRepository.GetAllAsync(); // Tüm adresler asenkron olarak alınır
            return values.Select(x => new GetAddressQueryResult // Her bir adres için sonuç nesnesi oluşturulur
            {
                AddressID = x.AddressID, // Adres ID'si
                AddressCity = x.AddressCity, // Şehir bilgisi
                AddressDetail = x.AddressDetail1, // Detay bilgisi
                AddressDistrict = x.AddressDistrict, // İlçe bilgisi
                AddressUserID = x.AddressUserID // Kullanıcı ID'si
            }).ToList(); // Sonuçlar listeye dönüştürülür
        }
    }
}
