using MultiShop.Discount.DTOs.DiscountCouponDTOs;

namespace MultiShop.Discount.Services.DiscountService
{
    public interface IDiscountService
    {
        Task<List<ResultDiscountCouponDTO>> GetAllDiscountCouponAsync();
        Task CreateDiscountCouponAsync(CreateDiscountCouponDTO createCouponDTO);
        Task UpdateDiscountCouponAsync(UpdateDiscountCouponDTO updateCouponDTO);
        Task DeleteDiscountCouponAsync(int couponID);
        Task<GetByIDDiscountCouponDTO> GetByIDDiscountCouponAsync(int couponID);
        Task<ResultDiscountCouponDTO> GetCodeDetailByCodeAsync(string couponCode);
    }
}
