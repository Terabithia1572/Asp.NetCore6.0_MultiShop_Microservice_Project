using MultiShop.Cargo.BusinessLayer.Abstract;
using MultiShop.Cargo.DataAccessLayer.Abstract;
using MultiShop.Cargo.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Cargo.BusinessLayer.Concrete
{
    public class CargoCustomerManager : ICargoCustomerService // Kargo müşteri işlemleri için servis sınıfı
    {
        private readonly ICargoCustomerDal _cargoCustomerDal; // Kargo müşteri veri erişim katmanı

        public CargoCustomerManager(ICargoCustomerDal cargoCustomerDal)
        {
            _cargoCustomerDal = cargoCustomerDal;
        }

        public void TDelete(int id)
        {
          _cargoCustomerDal.Delete(id); // Verilen ID'ye sahip kargo müşterisini siler
        }

        public List<CargoCustomer> TGetAll()
        {
            return _cargoCustomerDal.GetAll(); // Tüm kargo müşterilerini getirir
        }

        public CargoCustomer TGetByID(int id)
        {
            return _cargoCustomerDal.GetByID(id); // Verilen ID'ye sahip kargo müşterisini getirir
        }

        public void TInsert(CargoCustomer entity)
        {
            _cargoCustomerDal.Insert(entity); // Yeni kargo müşterisini ekler
        }

        public void TUpdate(CargoCustomer entity)
        {
           _cargoCustomerDal.Update(entity); // Var olan kargo müşterisini günceller
        }
    }
}
