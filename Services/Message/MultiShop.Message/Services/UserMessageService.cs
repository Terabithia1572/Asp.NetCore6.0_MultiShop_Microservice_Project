using AutoMapper;
using MultiShop.Message.DAL.Context;
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
            await _messageContext.AddAsync(createmessageDTO);
        }

        public async Task DeleteMessageAsync(int messageID)
        {
            var value = await _messageContext.FindAsync<GetByIDMessageDTO>(messageID);
            if (value != null)
            {
                 _messageContext.Remove(value);
            }
        }

        public Task<List<ResultMessageDTO>> GetAllMessageAsync()
        {
            throw new NotImplementedException();
        }

        public Task<GetByIDMessageDTO> GetByIDMessageAsync(int messageID)
        {
            throw new NotImplementedException();
        }

        public Task<List<ResultInboxMessageDTO>> GetInboxMessageAsync(string messageID)
        {
            throw new NotImplementedException();
        }

        public Task<List<ResultSendboxMessageDTO>> GetSendboxMessageAsync(string messageID)
        {
            throw new NotImplementedException();
        }

        public Task UpdateMessageAsync(UpdateMessageDTO updatemessageDTO)
        {
            throw new NotImplementedException();
        }
    }
}
