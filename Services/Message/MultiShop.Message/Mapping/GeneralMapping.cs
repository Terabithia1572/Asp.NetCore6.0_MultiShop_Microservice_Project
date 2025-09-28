using AutoMapper;
using MultiShop.Message.DAL.Entites;
using MultiShop.Message.DTOs;

namespace MultiShop.Message.Mapping
{
    public class GeneralMapping:Profile
    {
        public GeneralMapping()
        {
            CreateMap<UserMessage,ResultMessageDTO>().ReverseMap(); //UserMessage ile ResultMessageDTO arasında çift yönlü eşleme
            CreateMap<UserMessage,CreateMessageDTO>().ReverseMap(); //UserMessage ile CreateMessageDTO arasında çift yönlü eşleme
            CreateMap<UserMessage,UpdateMessageDTO>().ReverseMap(); //UserMessage ile UpdateMessageDTO arasında çift yönlü eşleme
            CreateMap<UserMessage,GetByIDMessageDTO>().ReverseMap(); //UserMessage ile GetByIDMessageDTO arasında çift yönlü eşleme
            CreateMap<UserMessage,ResultInboxMessageDTO>().ReverseMap(); //UserMessage ile ResultInboxMessageDTO arasında çift yönlü eşleme
            CreateMap<UserMessage,ResultSendboxMessageDTO>().ReverseMap(); //UserMessage ile ResultSendboxMessageDTO arasında çift yönlü eşleme

        }
    }
}
