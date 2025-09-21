using Dapper;
using MultiShop.Discount.Context;
using MultiShop.Discount.DTOs.DiscountCouponDTOs;

namespace MultiShop.Discount.Services.DiscountService
{
    public class DiscountService : IDiscountService
    {
        private readonly DapperContext _context; // Veritabanı bağlantısını yöneten ve Dapper ile veri işlemlerinin yapılmasını sağlayan context nesnesidir.

        public DiscountService(DapperContext context)
        {
            _context = context;
        }

        public async Task CreateDiscountCouponAsync(CreateDiscountCouponDTO createCouponDTO)
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

        public async Task DeleteDiscountCouponAsync(int couponID)
        {
            string query = "delete from Coupons where CouponID=@couponID";
            // Kuponu silmek için gerekli olan SQL sorgusunu tanımlar.
            var parameters = new DynamicParameters();
            // Dapper için kullanılacak dinamik parametre nesnesi oluşturulur.
            parameters.Add("@couponID", couponID);
            // Silinecek kuponun ID'sini parametrelere ekler.
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
                // Hazırlanan sorguyu ve parametreleri kullanarak veritabanından asenkron olarak kupon kaydını siler.
            }
        }

        public async Task<List<ResultDiscountCouponDTO>> GetAllDiscountCouponAsync()
        {
            string query = "select * from Coupons";
            // Tüm kuponları almak için gerekli olan SQL sorgusunu tanımlar.
            using (var connection = _context.CreateConnection())
                {
                // DapperContext üzerinden yeni bir veritabanı bağlantısı oluşturur.
                var result = await connection.QueryAsync<ResultDiscountCouponDTO>(query);
                // Sorguyu çalıştırarak ResultCouponDTO tipinde sonuçları alır.
                return result.ToList(); // Sonuçları listeye çevirir ve geri döner.
            }
        }

        public async Task<GetByIDDiscountCouponDTO> GetByIDDiscountCouponAsync(int couponID)
        {
            string query = "select * from Coupons where CouponID=@couponID";
            // Belirli bir kuponu ID'sine göre almak için gerekli olan SQL sorgusunu tanımlar.
            var parameters = new DynamicParameters();
            // Dapper için kullanılacak dinamik parametre nesnesi oluşturulur.
            parameters.Add("@couponID", couponID);
            // Alınacak kuponun ID'sini parametrelere ekler.
            using (var connection = _context.CreateConnection())
            {
                // DapperContext üzerinden yeni bir veritabanı bağlantısı oluşturur.
                var result = await connection.QueryFirstOrDefaultAsync<GetByIDDiscountCouponDTO>(query, parameters);
                // Sorguyu çalıştırarak GetByIDCouponDTO tipinde sonucu alır.
                return result; // Sonucu geri döner. Eğer kupon bulunamazsa null dönecektir.
            }
        }

        public async Task<ResultDiscountCouponDTO> GetCodeDetailByCodeAsync(string couponCode)
        {
            string query = "select * from Coupons where CouponCode=@couponCode"; // Kupon koduna göre kupon detaylarını almak için gerekli olan SQL sorgusunu tanımlar.
            var parameters = new DynamicParameters();
            parameters.Add("@couponCode", couponCode); // Alınacak kupon kodunu parametrelere ekler.
            using (var connection = _context.CreateConnection())
            {
                // DapperContext üzerinden yeni bir veritabanı bağlantısı oluşturur.
                var values = await connection.QueryFirstOrDefaultAsync<ResultDiscountCouponDTO>(query, parameters);
                // Sorguyu çalıştırarak ResultDiscountCouponDTO tipinde sonucu alır.
                return values; // Sonucu geri döner. Eğer kupon bulunamazsa null dönecektir.
            }
        }

        public async Task UpdateDiscountCouponAsync(UpdateDiscountCouponDTO updateCouponDTO)
        {
            string query="update Coupons set CouponCode=@couponCode,CouponRate=@couponRate," +
                "CouponIsActive=@couponIsActive,CouponValidDate=@couponValidDate where CouponID=@couponID";
            // Kuponu güncellemek için gerekli olan SQL sorgusunu tanımlar.
            var parameters = new DynamicParameters();
            parameters.Add("@couponCode", updateCouponDTO.CouponCode);
            // Güncellenecek kupon kodunu parametrelere ekler.
            parameters.Add("@couponRate", updateCouponDTO.CouponRate);
            // Güncellenecek kupon indirim oranını parametrelere ekler.
            parameters.Add("@couponIsActive", updateCouponDTO.CouponIsActive);
            // Güncellenecek kuponun aktif olup olmadığını parametrelere ekler.
            parameters.Add("@couponValidDate", updateCouponDTO.CouponValidDate);
            // Güncellenecek kuponun geçerlilik tarihini parametrelere ekler.
            parameters.Add("@couponID", updateCouponDTO.CouponID);
            // Güncellenecek kuponun ID'sini parametrelere ekler.
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
                // Hazırlanan sorguyu ve parametreleri kullanarak veritabanında asenkron olarak kupon kaydını günceller.
            }
        }
    }
}
