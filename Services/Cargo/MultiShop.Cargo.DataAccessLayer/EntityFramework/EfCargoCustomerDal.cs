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
        private readonly CargoContext _context; // CargoContext nesnesi, veritabanı işlemleri için kullanılır
        public EfCargoCustomerDal(CargoContext cargoContext, CargoContext context) : base(cargoContext) // EfCargoCustomerDal sınıfı, GenericRepository sınıfından türetilir ve CargoContext nesnesini alır
        {
            _context = context;
        }

        public CargoCustomer GetCargoCustomerByID(string id)
        {
           var values=_context.CargoCustomers.Where(x=>x.UserCustomerID==id).FirstOrDefault(); // Verilen kullanıcı ID'sine sahip kargo müşterisini bulur
            return values; // Bulunan kargo müşterisini döner
        }
    }

}

