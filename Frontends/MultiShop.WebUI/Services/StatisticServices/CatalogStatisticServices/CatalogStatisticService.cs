
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
            var response = await _httpClient.GetAsync("statistics/GetBrandCount" ); //HttpClient ile GET isteği gönderilir
            var values = await response.Content.ReadFromJsonAsync<long>(); //Gelen cevap JSON formatında okunur ve listeye dönüştürülür
            return values; //Liste döndürülür
        }

        public Task<long> GetCategoryCount()
        {
            throw new NotImplementedException();
        }

        public Task<string> GetMaximumPriceProductName()
        {
            throw new NotImplementedException();
        }

        public Task<string> GetMinimumPriceProductName()
        {
            throw new NotImplementedException();
        }

        public Task<long> GetProduceCount()
        {
            throw new NotImplementedException();
        }

        public Task<decimal> GetProductAvgPrice()
        {
            throw new NotImplementedException();
        }
    }
}
