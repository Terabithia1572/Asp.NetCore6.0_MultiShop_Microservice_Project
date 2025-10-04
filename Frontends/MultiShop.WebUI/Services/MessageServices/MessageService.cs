using MultiShop.DTOLayer.MessageDTOs;

namespace MultiShop.WebUI.Services.MessageServices
{
    public class MessageService : IMessageService
    {
        private readonly HttpClient _httpClient;

        public MessageService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ResultInboxMessageDTO>> GetInboxMessageAsync(string messageID)
        {
            var responseMessage = await _httpClient.GetAsync("http://localhost:5000/services/message/usermessages/inbox/" + messageID);
            var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultInboxMessageDTO>>();
            return values;
        }

        public async Task<List<ResultSendboxMessageDTO>> GetSendboxMessageAsync(string messageID)
        {
            var responseMessage = await _httpClient.GetAsync("http://localhost:5000/services/message/usermessages/sendbox/" + messageID);
            var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultSendboxMessageDTO>>();
            return values;
        }

        public async Task<int> GetTotalMessageCountByReceiverID(string receiverID)
        {
            var responseMessage = await _httpClient.GetAsync("UserMessages/GetTotalMessageCountByReceiverID?id=" + receiverID);
            var values = await responseMessage.Content.ReadFromJsonAsync<int>();
            return values;
        }
    }
}
