
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

        public async Task<long> GetCategoryCount()
        {
            var response = await _httpClient.GetAsync("statistics/GetCategoryCount"); //HttpClient ile GET isteği gönderilir
            var values = await response.Content.ReadFromJsonAsync<long>(); //Gelen cevap JSON formatında okunur ve listeye dönüştürülür
            return values; //Liste döndürülür
        }

        public async Task<string> GetMaximumPriceProductName()
        {
            var response = await _httpClient.GetAsync("statistics/GetMaximumPriceProductName"); //HttpClient ile GET isteği gönderilir
            var values = await response.Content.ReadFromJsonAsync<string>(); //Gelen cevap JSON formatında okunur ve listeye dönüştürülür
            return values; //Liste döndürülür
        }

        public async Task<string> GetMinimumPriceProductName()
        {
            var response = await _httpClient.GetAsync("statistics/GetMinimumPriceProductName"); //HttpClient ile GET isteği gönderilir
            var values = await response.Content.ReadFromJsonAsync<string>(); //Gelen cevap JSON formatında okunur ve listeye dönüştürülür
            return values; //Liste döndürülür
        }

        public async Task<long> GetProduceCount()
        {
            var response = await _httpClient.GetAsync("statistics/GetProduceCount"); //HttpClient ile GET isteği gönderilir
            var values = await response.Content.ReadFromJsonAsync<long>(); //Gelen cevap JSON formatında okunur ve listeye dönüştürülür
            return values; //Liste döndürülür
        }

        public async Task<decimal> GetProductAvgPrice()
        {
            var response = await _httpClient.GetAsync("statistics/GetProductAvgPrice"); //HttpClient ile GET isteği gönderilir
            var values = await response.Content.ReadFromJsonAsync<decimal>(); //Gelen cevap JSON formatında okunur ve listeye dönüştürülür
            return values; //Liste döndürülür
        }
    }
}
