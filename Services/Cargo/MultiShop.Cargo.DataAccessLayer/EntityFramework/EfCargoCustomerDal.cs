using MultiShop.Cargo.DataAccessLayer.Abstract;
using MultiShop.Cargo.DataAccessLayer.Concrete;
using MultiShop.Cargo.DataAccessLayer.Repositories;
using MultiShop.Cargo.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Cargo.DataAccessLayer.EntityFramework
{
    public class EfCargoCustomerDal : GenericRepository<CargoCustomer>, ICargoCustomerDal // EfCargoCustomerDal sınıfı, ICargoCustomerDal arayüzünü uygular ve CargoCustomer varlıklarını (entity) yönetir
    {
        public EfCargoCustomerDal(CargoContext cargoContext) : base(cargoContext) // EfCargoCustomerDal sınıfı, GenericRepository sınıfından türetilir ve CargoContext nesnesini alır
        {

        }
    }

}

