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
    public class CargoOperationManager : ICargoOperationService // Kargo işlemleri için servis sınıfı
    {
        private readonly ICargoOperationDal _cargoOperationDal; // Kargo işlemleri veri erişim katmanı

        public CargoOperationManager(ICargoOperationDal cargoOperationDal)
        {
            _cargoOperationDal = cargoOperationDal;
        }

        public void TDelete(int id)
        {
            _cargoOperationDal.Delete(id); // Verilen ID'ye sahip kargo işlemini siler
        }

        public List<CargoOperation> TGetAll()
        {
            return _cargoOperationDal.GetAll(); // Tüm kargo işlemlerini getirir
        }

        public CargoOperation TGetByID(int id)
        {
            return _cargoOperationDal.GetByID(id); // Verilen ID'ye sahip kargo işlemini getirir    
        }

        public void TInsert(CargoOperation entity)
        {
           _cargoOperationDal.Insert(entity); // Yeni kargo işlemini ekler
        }

        public void TUpdate(CargoOperation entity)
        {
           _cargoOperationDal.Update(entity); // Var olan kargo işlemini günceller
        }
    }
}
