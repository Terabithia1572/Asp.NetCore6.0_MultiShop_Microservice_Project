using MultiShop.DTOLayer.DiscountDTOs;

namespace MultiShop.WebUI.Services.DiscountServices
{
    public interface IDiscountService
    {
        Task<GetDiscountCodeDetailByCode> GetDiscountCode(string couponCode); //Kupon koduna göre kupon detaylarını getiren metot.
        Task<int> GetDiscountCouponCountRate(string couponCode); //Kupon koduna göre kupon oranını getiren metot.
    }
}
