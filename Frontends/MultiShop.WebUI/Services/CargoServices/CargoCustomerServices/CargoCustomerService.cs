using MultiShop.DTOLayer.CargoDTOs.CargoCustomerDTOs;

namespace MultiShop.WebUI.Services.CargoServices.CargoCustomerServices
{
    public class CargoCustomerService : ICargoCustomerService
    {
        private readonly HttpClient _httpClient;

        public CargoCustomerService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<GetCargoCustomerByIDDTO> GetByIDCargoCustomerInfoAsync(string id)
        {
            var response = await _httpClient.GetAsync("CargoCustomers/GetCargoCustomerByUserID/" + id);

            if (!response.IsSuccessStatusCode)
                return null; // 404 vs. gelirse null dön

            return await response.Content.ReadFromJsonAsync<GetCargoCustomerByIDDTO>();
        }


    }
}
