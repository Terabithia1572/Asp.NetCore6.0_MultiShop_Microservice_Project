using MultiShop.DTOLayer.CargoDTOs.CargoCompanyDTOs;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Services.CargoServices.CargoCompanyServices
{
    public class CargoCompanyService : ICargoCompanyService
    {
        private readonly HttpClient _httpClient; //HttpClient nesnesi
        public CargoCompanyService(HttpClient httpClient) //Dependency Injection ile HttpClient nesnesi alınır
        {
            _httpClient = httpClient; //HttpClient nesnesi atanır
        }

        public async Task CreateCargoCompanyAsync(CreateCargoCompanyDTO createCargoCompanyDTO)
        {
           await _httpClient.PostAsJsonAsync<CreateCargoCompanyDTO>("cargocompanies", createCargoCompanyDTO);
        }

        public async Task DeleteCargoCompanyAsync(int id)
        {
            await _httpClient.DeleteAsync("cargocompanies?id=" + id);
        }

        public async Task<List<ResultCargoCompanyDTO>> GetAllCargoCompanyAsync()
        {
            var response = await _httpClient.GetAsync("cargocompanies"); //HttpClient ile GET isteği gönderilir
            var jsonData = await response.Content.ReadAsStringAsync(); // JSON verisi okunur.
                                                                       //var values = await response.Content.ReadFromJsonAsync<List<ResultAboutDTO>>(); //Gelen cevap JSON formatında okunur ve listeye dönüştürülür
            var values = JsonConvert.DeserializeObject<List<ResultCargoCompanyDTO>>(jsonData); // JSON verisi DTO nesnesine dönüştürülür.
            return values; //Liste döndürülür

        }

        public async Task<UpdateCargoCompanyDTO> GetByIDCargoCompanyAsync(int id)
        {
            var response = await _httpClient.GetAsync("cargocompanies/" + id); //HttpClient ile GET isteği gönderilir
            var values = await response.Content.ReadFromJsonAsync<UpdateCargoCompanyDTO>(); //Gelen cevap JSON formatında okunur ve listeye dönüştürülür
            return values; //Liste döndürülür
        }

        public async Task UpdateCargoCompanyAsync(UpdateCargoCompanyDTO updateCargoCompanyDTO)
        {
            await _httpClient.PutAsJsonAsync<UpdateCargoCompanyDTO>("cargocompanies", updateCargoCompanyDTO);
        }
    }
}
