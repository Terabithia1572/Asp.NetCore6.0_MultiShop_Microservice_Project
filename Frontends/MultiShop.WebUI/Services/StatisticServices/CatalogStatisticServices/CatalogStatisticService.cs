
namespace MultiShop.WebUI.Services.StatisticServices.CatalogStatisticServices
{
    public class CatalogStatisticService : ICatalogStatisticService
    {
        private readonly HttpClient _httpClient;

        public CatalogStatisticService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<long> GetBrandCount()
        {
            //http://localhost:5000/services/catalog/statistics/GetBrandCount
            var responseMessage = await _httpClient.GetAsync("statistics/GetBrandCount");
            var values = await responseMessage.Content.ReadFromJsonAsync<long>();
            return values;
        }

        public async Task<long> GetCategoryCount()
        {
            var responseMessage = await _httpClient.GetAsync("statistics/GetCategoryCount");
            var values = await responseMessage.Content.ReadFromJsonAsync<long>();
            return values;
        }

        public async Task<string> GetMaximumPriceProductName()
        {
            var responseMessage = await _httpClient.GetAsync("statistics/GetMaximumPriceProductName");
            var values = await responseMessage.Content.ReadAsStringAsync();
            return values;
        }

        public async Task<string> GetMinimumPriceProductName()
        {
            var responseMessage = await _httpClient.GetAsync("statistics/GetMinimumPriceProductName");
            var values = await responseMessage.Content.ReadAsStringAsync();
            return values;
        }

        public async Task<long> GetProductCount()
        {
            var response = await _httpClient.GetAsync("statistics/GetProductCount"); //HttpClient ile GET isteği gönderilir
            var values = await response.Content.ReadFromJsonAsync<long>(); //Gelen cevap JSON formatında okunur ve listeye dönüştürülür
            return values; //Liste döndürülür
        }

        public async Task<decimal> GetProductAvgPrice()
        {
            var responseMessage = await _httpClient.GetAsync("statistics/GetProductAvgPrice");
            var values = await responseMessage.Content.ReadFromJsonAsync<decimal>();
            return values;
        }
    }
}
