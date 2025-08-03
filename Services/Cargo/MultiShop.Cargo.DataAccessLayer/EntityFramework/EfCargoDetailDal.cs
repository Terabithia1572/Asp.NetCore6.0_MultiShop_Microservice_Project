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
    public class EfCargoDetailDal : GenericRepository<CargoDetail>, ICargoDetailDal // EfCargoDetailDal sınıfı, ICargoDetailDal arayüzünü uygular ve CargoDetail varlıklarını (entity) yönetir
    {
        public EfCargoDetailDal(CargoContext cargoContext) : base(cargoContext) // EfCargoDetailDal sınıfı, GenericRepository sınıfından türetilir ve CargoContext nesnesini alır
        {
        }
    }
}
