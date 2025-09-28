using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MultiShop.Message.DAL.Context;
using MultiShop.Message.DAL.Entites;
using MultiShop.Message.DTOs;

namespace MultiShop.Message.Services
{
    public class UserMessageService : IUserMessageService
    {
        private readonly MessageContext _messageContext;
        private readonly IMapper _mapper;

        public UserMessageService(MessageContext messageContext, IMapper mapper)
        {
            _messageContext = messageContext;
            _mapper = mapper;
        }

        public async Task CreateMessageAsync(CreateMessageDTO createmessageDTO)
        {
            var values = _mapper.Map<UserMessage>(createmessageDTO);
            await _messageContext.UserMessages.AddAsync(values);
        }

        public async Task DeleteMessageAsync(int messageID)
        {
           var values = await _messageContext.FindAsync<UserMessage>(messageID);
              if(values != null)
              {
                _messageContext.UserMessages.Remove(values);
                await _messageContext.SaveChangesAsync();
            }

        }

        public async Task<List<ResultMessageDTO>> GetAllMessageAsync()
        {
           var values=await _messageContext.UserMessages.ToListAsync();
            return _mapper.Map<List<ResultMessageDTO>>(values);

        }

        public Task<GetByIDMessageDTO> GetByIDMessageAsync(int messageID)
        {
            var values =  _messageContext.UserMessages.FindAsync(messageID);
            return _mapper.Map<Task<GetByIDMessageDTO>>(values);
        }

        public Task<List<ResultInboxMessageDTO>> GetInboxMessageAsync(string messageID)
        {
            var values =  _messageContext.UserMessages.Where(x => x.MessageReceiverID == messageID).ToListAsync();
            return _mapper.Map<Task<List<ResultInboxMessageDTO>>>(values);
        }

        public Task<List<ResultSendboxMessageDTO>> GetSendboxMessageAsync(string messageID)
        {
            var values =  _messageContext.UserMessages.Where(x => x.MessageSenderID == messageID).ToListAsync();
            return _mapper.Map<Task<List<ResultSendboxMessageDTO>>>(values);
        }

        public async Task UpdateMessageAsync(UpdateMessageDTO updatemessageDTO)
        {
            var values=await _messageContext.UserMessages.FindAsync(updatemessageDTO.UserMessageID);
             _messageContext.UserMessages.Update(values);
        }
    }
}
