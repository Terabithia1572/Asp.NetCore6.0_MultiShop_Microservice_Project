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
    public class CargoCompanyManager : ICargoCompanyService //Karo şirketleri için servis sınıfı
    {
        private readonly ICargoCompanyDal _cargoCompanyDal; // Kargo şirketleri veri erişim katmanı

        public CargoCompanyManager(ICargoCompanyDal cargoCompanyDal)
        {
            _cargoCompanyDal = cargoCompanyDal;
        }

        public void TDelete(int id)
        {
            _cargoCompanyDal.Delete(id); // Verilen ID'ye sahip kargo şirketini siler
        }

        public List<CargoCompany> TGetAll()
        {
            return _cargoCompanyDal.GetAll(); // Tüm kargo şirketlerini getirir
        }

        public CargoCompany TGetByID(int id)
        {
            return _cargoCompanyDal.GetByID(id); // Verilen ID'ye sahip kargo şirketini getirir
        }

        public void TInsert(CargoCompany entity)
        {
            _cargoCompanyDal.Insert(entity); // Yeni kargo şirketini ekler
        }

        public void TUpdate(CargoCompany entity)
        {
            _cargoCompanyDal.Update(entity); // Var olan kargo şirketini günceller
        }
    }
}
