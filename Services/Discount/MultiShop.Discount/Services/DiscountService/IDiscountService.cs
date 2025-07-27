using MultiShop.Discount.DTOs;

namespace MultiShop.Discount.Services.DiscountService
{
    public interface IDiscountService
    {
        Task<List<ResultCouponDTO>> GetAllCouponAsync();
        Task CreateCouponAsync(CreateCouponDTO createCouponDTO);
        Task UpdateCouponAsync(UpdateCouponDTO updateCouponDTO);
        Task DeleteCouponAsync(int couponID);
        Task<GetByIDCouponDTO> GetByIDCouponAsync(int couponID);
    }
}
