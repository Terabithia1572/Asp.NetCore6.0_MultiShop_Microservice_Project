using MultiShop.DTOLayer.DiscountDTOs;

namespace MultiShop.WebUI.Services.DiscountServices
{
    public class DiscountService : IDiscountService
    {
        private readonly HttpClient _httpClient;

        public DiscountService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<GetDiscountCodeDetailByCode> GetDiscountCode(string couponCode)
        {
           var responseMessage=await _httpClient.GetAsync("http://localhost:1003/api/Discounts/GetCodeDetailByCodeAsync?couponCode="+couponCode); //Kupon koduna göre kupon detaylarını aldık.
            var values=await responseMessage.Content.ReadFromJsonAsync<GetDiscountCodeDetailByCode>(); //Aldığımız kupon detaylarını GetDiscountCodeDetailByCode DTO'suna map ettik.
            return values;
        }
    }
}
