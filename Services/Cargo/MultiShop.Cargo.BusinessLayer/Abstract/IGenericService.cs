using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Cargo.BusinessLayer.Abstract
{
    public interface IGenericService<T> where T : class
    {
        void TInsert(T entity); // Yeni bir varlığı (entity) veritabanına ekler.

        void TUpdate(T entity); // Var olan bir varlığı (entity) veritabanında günceller.

        void TDelete(int id); // Verilen ID'ye sahip varlığı (entity) veritabanından siler.

        T TGetByID(int id); // Verilen ID'ye sahip varlığı (entity) veritabanından getirir.

        List<T> TGetAll(); // Tüm varlıkları (entity) veritabanından getirir.

        // bu methodların başına neden t harfi ekledik?
        //başında T olan methodlar bizim Business katmanımızda kullanacağımız generic methodlar
        //Başında t olmayan methodlar ise DataAccess katmanında kullanacağımız methodlar
    }
}
