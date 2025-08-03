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
    public class CargoDetailManager : ICargoDetailService // Kargo detayları için servis sınıfı
    {
        private readonly ICargoDetailDal _cargoDetailDal; // Kargo detayları veri erişim katmanı

        public CargoDetailManager(ICargoDetailDal cargoDetailDal)
        {
            _cargoDetailDal = cargoDetailDal;
        }

        public void TDelete(int id)
        {
            _cargoDetailDal.Delete(id); // Verilen ID'ye sahip kargo detayını siler
        }

        public List<CargoDetail> TGetAll()
        {
            return _cargoDetailDal.GetAll(); // Tüm kargo detaylarını getirir
        }

        public CargoDetail TGetByID(int id)
        {
           return _cargoDetailDal.GetByID(id); // Verilen ID'ye sahip kargo detayını getirir
        }

        public void TInsert(CargoDetail entity)
        {
            _cargoDetailDal.Insert(entity); // Yeni kargo detayını ekler
        }

        public void TUpdate(CargoDetail entity)
        {
            _cargoDetailDal.Update(entity); // Var olan kargo detayını günceller
        }
    }
}
