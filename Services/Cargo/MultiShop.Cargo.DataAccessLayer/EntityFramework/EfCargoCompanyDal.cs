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
    public class EfCargoCompanyDal:GenericRepository<CargoCompany>, ICargoCompanyDal // EfCargoCompanyDal sınıfı, ICargoCompanyDal arayüzünü uygular ve CargoCompany varlıklarını (entity) yönetir
    {
        public EfCargoCompanyDal(CargoContext cargoContext):base(cargoContext) // EfCargoCompanyDal sınıfı, GenericRepository sınıfından türetilir ve CargoContext nesnesini alır
        {
            
        }
    }
}
