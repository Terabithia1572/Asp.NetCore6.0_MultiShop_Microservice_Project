using Dapper;
using MultiShop.Discount.Context;
using MultiShop.Discount.DTOs;

namespace MultiShop.Discount.Services.DiscountService
{
    public class DiscountService : IDiscountService
    {
        private readonly DapperContext _context; // Veritabanı bağlantısını yöneten ve Dapper ile veri işlemlerinin yapılmasını sağlayan context nesnesidir.

        public DiscountService(DapperContext context)
        {
            _context = context;
        }

        public async Task CreateCouponAsync(CreateCouponDTO createCouponDTO)
        {
            string query = "insert into Coupons (CouponCode,CouponRate,CouponIsActive,CouponValidDate) values" +
   "(@couponCode,@couponRate,@couponIsActive,@couponValidDate)";
            // Kupon eklemek için gerekli olan SQL sorgusunu tanımlar.

            var parameters = new DynamicParameters();
            // Dapper için kullanılacak dinamik parametre nesnesi oluşturulur.

            parameters.Add("@couponCode", createCouponDTO.CouponCode);
            // Kupon kodunu parametrelere ekler.

            parameters.Add("@couponRate", createCouponDTO.CouponRate);
            // Kupon indirim oranını parametrelere ekler.

            parameters.Add("@couponIsActive", createCouponDTO.CouponIsActive);
            // Kuponun aktif olup olmadığını parametrelere ekler.

            parameters.Add("@couponValidDate", createCouponDTO.CouponValidDate);
            // Kuponun geçerlilik tarihini parametrelere ekler.

            using (var connection = _context.CreateConnection())
            // DapperContext üzerinden yeni bir veritabanı bağlantısı oluşturur.

            {
                await connection.ExecuteAsync(query, parameters);
                // Hazırlanan sorguyu ve parametreleri kullanarak veritabanına asenkron olarak yeni kupon kaydı ekler.
            }

        }

        public Task DeleteCouponAsync(int couponID)
        {
            throw new NotImplementedException();
        }

        public Task<List<ResultCouponDTO>> GetAllCouponAsync()
        {
            throw new NotImplementedException();
        }

        public Task<GetByIDCouponDTO> GetByIDCouponAsync(int couponID)
        {
            throw new NotImplementedException();
        }

        public Task UpdateCouponAsync(UpdateCouponDTO updateCouponDTO)
        {
            throw new NotImplementedException();
        }
    }
}
