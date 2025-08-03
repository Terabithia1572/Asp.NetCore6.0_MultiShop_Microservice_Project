using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Cargo.DataAccessLayer.Abstract
{
    public interface IGenericDal<T> where T : class
    {
        void Insert(T entity); // Yeni bir varlığı (entity) veritabanına ekler.

        void Update(T entity); // Var olan bir varlığı (entity) veritabanında günceller.

        void Delete(int id); // Verilen ID'ye sahip varlığı (entity) veritabanından siler.

        T GetByID(int id); // Verilen ID'ye sahip varlığı (entity) veritabanından getirir.

        List<T> GetAll(); // Tüm varlıkları (entity) veritabanından getirir.
    }

}
