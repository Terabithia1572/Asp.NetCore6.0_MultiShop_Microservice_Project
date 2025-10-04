
namespace MultiShop.SignalRRealTimeAPI.Services.SignalRCommentServices
{
    public class SignalRCommentService : ISignalRCommentService
    {
        private readonly HttpClient _httpClient;

        public SignalRCommentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<int> GetTotalCommentCount()
        {
            var response = await _httpClient.GetAsync("comments/GetTotalCommentCount");
            var values = await response.Content.ReadFromJsonAsync<int>();
            return values;
        }

    }
}
