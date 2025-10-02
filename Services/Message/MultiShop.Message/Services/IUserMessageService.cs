using MultiShop.Message.DTOs;

namespace MultiShop.Message.Services
{
    public interface IUserMessageService
    {
        Task<List<ResultMessageDTO>> GetAllMessageAsync(); //Veritabanındaki tüm mesajları listeleyen metot
        Task<List<ResultInboxMessageDTO>> GetInboxMessageAsync(string messageID); //Gelen kutusundaki mesajları listeleyen metot
        Task<List<ResultSendboxMessageDTO>> GetSendboxMessageAsync(string messageID); //Giden kutusundaki mesajları listeleyen metot
        Task CreateMessageAsync(CreateMessageDTO createmessageDTO); //Yeni mesaj ekleyen metot
        Task UpdateMessageAsync(UpdateMessageDTO updatemessageDTO); //Mevcut mesajı güncelleyen metot
        Task DeleteMessageAsync(int messageID); //Mesajı silen metot
        Task<GetByIDMessageDTO> GetByIDMessageAsync(int messageID); //ID'ye göre mesajı getiren metot
        Task<int> GetTotalMessageCount();

    }
}
