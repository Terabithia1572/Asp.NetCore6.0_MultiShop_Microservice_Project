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
    public class EfCargoOperationDal : GenericRepository<CargoOperation>, ICargoOperationDal // EfCargoOperationDal sınıfı, ICargoOperationDal arayüzünü uygular ve CargoOperation varlıklarını (entity) yönetir
    {
        public EfCargoOperationDal(CargoContext cargoContext) : base(cargoContext) // EfCargoOperationDal sınıfı, GenericRepository sınıfından türetilir ve CargoContext nesnesini alır
        {
        }
    }
}
