using MultiShop.Cargo.DataAccessLayer.Abstract;
using MultiShop.Cargo.DataAccessLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Cargo.DataAccessLayer.Repositories
{
    public class GenericRepository<T> : IGenericDal<T> where T : class // GenericRepository sınıfı, IGenericDal arayüzünü uygular ve T türünde varlıkları (entity) yönetir
    {
        private readonly CargoContext _context; // CargoContext, Entity Framework Core kullanarak veritabanı işlemlerini gerçekleştiren bir DbContext sınıfıdır

        public GenericRepository(CargoContext context)
        {
            _context = context; // GenericRepository sınıfı, CargoContext nesnesini alır ve bu nesne üzerinden veritabanı işlemlerini gerçekleştirir
        }

        public void Delete(int id)
        {
            var values= _context.Set<T>().Find(id);// Verilen ID'ye sahip varlığı (entity) bulur
            _context.Set<T>().Remove(values);   // Bulunan varlığı (entity) veritabanından siler
            _context.SaveChanges(); // Değişiklikleri kaydeder
        }

        public List<T> GetAll()
        {
            var values = _context.Set<T>().ToList(); // Tüm varlıkları (entity) veritabanından getirir
            return values; // Liste olarak döner
        }

        public T GetByID(int id)
        {
            var values = _context.Set<T>().Find(id); // Verilen ID'ye sahip varlığı (entity) bulur
            return _context.Set<T>().FirstOrDefault(x => x.Equals(values)); // İlk eşleşen varlığı döner, eğer yoksa null döner
        }

        public void Insert(T entity)
        {
            _context.Set<T>().Add(entity); // Yeni varlığı (entity) ekler
            _context.SaveChanges(); // Değişiklikleri kaydeder
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity); // Var olan varlığı (entity) günceller
            _context.SaveChanges(); // Değişiklikleri kaydeder
        }
    }
}
