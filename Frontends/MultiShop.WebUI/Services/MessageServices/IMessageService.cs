using MultiShop.DTOLayer.MessageDTOs;

namespace MultiShop.WebUI.Services.MessageServices
{
    public interface IMessageService
    {
        Task<List<ResultInboxMessageDTO>> GetInboxMessageAsync(string messageID); //Gelen kutusundaki mesajları listeleyen metot
        Task<List<ResultSendboxMessageDTO>> GetSendboxMessageAsync(string messageID); //Giden kutusundaki mesajları listeleyen metot
       
    }
}
