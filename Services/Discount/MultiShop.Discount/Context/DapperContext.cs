using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MultiShop.Discount.Entites;
using System.Data;

namespace MultiShop.Discount.Context
{
    public class DapperContext:DbContext
    {
        private readonly IConfiguration _configuration; // Uygulama yapılandırma ayarlarına (appsettings.json, environment variables vs.) erişim sağlar.
        private readonly string _connectionString; // Veritabanı bağlantı dizesini tutar.
        // DbContext sınıfı, Entity Framework Core'un temel sınıfıdır ve veritabanı işlemlerini yönetir.

        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection"); // "DefaultConnection" adlı bağlantı dizesini yapılandırmadan alır.
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           optionsBuilder.UseSqlServer("Server=.;initial Catalog=MultiShopDiscountDB; integrated Security=true");
        }
        // OnConfiguring metodu, DbContext'in yapılandırılmasını sağlar. Burada SQL Server kullanılarak bağlantı dizesi ayarlanır.
        public DbSet<Coupon> Coupons { get; set; } // Coupons tablosunu temsil eden DbSet özelliği. Bu, Coupon nesneleri üzerinde CRUD işlemleri yapmamızı sağlar.
        public IDbConnection CreateConnection() => new SqlConnection(_connectionString); // Veritabanı bağlantısı oluşturur ve SqlConnection nesnesi döner. Bu, Dapper gibi mikro ORM'ler ile kullanılabilir.


    }
}
