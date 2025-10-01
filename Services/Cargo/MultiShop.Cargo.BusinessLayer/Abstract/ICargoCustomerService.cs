using MultiShop.Cargo.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Cargo.BusinessLayer.Abstract
{
    public interface ICargoCustomerService:IGenericService<CargoCustomer> // Kargo müşteri işlemleri için generic servis arayüzü
    {
        CargoCustomer TGetCargoCustomerByID(string id); // Kullanıcı ID'sine göre kargo müşterisi getirme metodu
    }
}
