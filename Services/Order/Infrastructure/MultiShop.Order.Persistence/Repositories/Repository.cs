using Microsoft.EntityFrameworkCore;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Order.Persistence.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly OrderContext _orderContext; // Veritabanı işlemlerini gerçekleştirmek için kullanılan Entity Framework Core context nesnesi.

        public Repository(OrderContext orderContext)
        {
            _orderContext = orderContext; // DI (Dependency Injection) ile gelen OrderContext, sınıfın içindeki private değişkene atanır.
        }

        public async Task CreateAsync(T entity)
        {
            _orderContext.Set<T>().Add(entity); // Yeni bir entity (veri) ekler.
            await _orderContext.SaveChangesAsync(); // Eklenen değişiklikleri veritabanına asenkron olarak kaydeder.
        }

        public async Task DeleteAsync(T entity)
        {
            _orderContext.Set<T>().Remove(entity); // Verilen entity'yi siler.
            await _orderContext.SaveChangesAsync(); // Silme işlemini veritabanına asenkron olarak kaydeder.
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _orderContext.Set<T>().ToListAsync(); // İlgili entity tipindeki tüm kayıtları asenkron olarak getirir ve bir liste olarak döner.
        }

        public async Task<T> GetByFilterAsync(Expression<Func<T, bool>> filter)
        {
            return await _orderContext.Set<T>().SingleOrDefaultAsync(filter); // Belirtilen filtreye (şarta) uyan ilk veya tek kaydı asenkron olarak getirir.
        }

        public async Task<T> GetByIDAsync(int id)
        {
            return await _orderContext.Set<T>().FindAsync(id); // Verilen id'ye sahip kaydı asenkron olarak bulur ve döner.
        }

        public async Task UpdateAsync(T entity)
        {
            _orderContext.Set<T>().Update(entity); // Var olan bir entity'yi günceller.
            await _orderContext.SaveChangesAsync(); // Güncelleme işlemini veritabanına asenkron olarak kaydeder.
        }
    }
}
