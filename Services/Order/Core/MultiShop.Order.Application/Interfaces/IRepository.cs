using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Order.Application.Interfaces
{
    public interface IRepository<T> where T : class // T sınıfı veya türevleri için geçerli olacak bir arayüz
    {
        Task<List<T>> GetAllAsync(); // T türünden tüm kayıtları asenkron olarak getirir
        Task<T> GetByIDAsync(int id); // T türünden bir kaydı ID'sine göre asenkron olarak getirir
        Task<T> CreateAsync(T entity); // T türünden yeni bir kayıt oluşturur ve asenkron olarak döner
        Task<T> UpdateAsync(T entity); // T türünden mevcut bir kaydı günceller ve asenkron olarak döner
        Task<T> DeleteAsync(T entity); // T türünden mevcut bir kaydı siler ve asenkron olarak döner
        Task<T> GetByFilterAsync(Expression<Func<T, bool>> filter); // T türünden bir kaydı belirli bir filtreye göre asenkron olarak getirir. T bizim giriş olacak bool bizim çıkış olacak filtreleme işlemi için kullanılır

    }
}
