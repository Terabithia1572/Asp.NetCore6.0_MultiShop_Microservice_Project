using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MultiShop.Message.DAL.Context;
using MultiShop.Message.DAL.Entites;
using MultiShop.Message.DTOs;

namespace MultiShop.Message.Services
{
    public class UserMessageService : IUserMessageService //Mesaj işlemlerini gerçekleştiren servis sınıfı
    {
        private readonly MessageContext _messageContext; //Veritabanı işlemleri için kullanılan DbContext
        private readonly IMapper _mapper; //DTO ve Entity dönüşümleri için kullanılan AutoMapper arayüzü

        public UserMessageService(MessageContext messageContext, IMapper mapper) //Dependency Injection ile MessageContext ve IMapper örneklerini alır
        {
            _messageContext = messageContext; //DbContext örneğini sınıf değişkenine atar
            _mapper = mapper; //AutoMapper örneğini sınıf değişkenine atar
        }

        public async Task CreateMessageAsync(CreateMessageDTO createmessageDTO) //Yeni mesaj ekleme metodu
        {
            var values = _mapper.Map<UserMessage>(createmessageDTO); //CreateMessageDTO nesnesini UserMessage entity'sine dönüştürür 
            await _messageContext.UserMessages.AddAsync(values); //Dönüştürülen entity'yi DbSet'e ekler
            await _messageContext.SaveChangesAsync(); //Değişiklikleri veritabanına kaydeder (eksikti, eklendi)
        }

        public async Task DeleteMessageAsync(int messageID) //Mesaj silme metodu
        {
            var values = await _messageContext.FindAsync<UserMessage>(messageID); //Verilen ID'ye sahip mesajı veritabanında arar
            if (values != null) //Eğer mesaj bulunursa
            {
                _messageContext.UserMessages.Remove(values); //Mesajı DbSet'ten kaldırır
                await _messageContext.SaveChangesAsync(); //Değişiklikleri veritabanına kaydeder
            }
        }

        public async Task<List<ResultMessageDTO>> GetAllMessageAsync() //Tüm mesajları listeleme metodu
        {
            var values = await _messageContext.UserMessages.AsNoTracking().ToListAsync(); //DbSet'teki tüm mesajları liste olarak alır (AsNoTracking performans için)
            return _mapper.Map<List<ResultMessageDTO>>(values); //Alınan mesaj listesini ResultMessageDTO tipine dönüştürüp döner
        }

        public async Task<GetByIDMessageDTO> GetByIDMessageAsync(int messageID) //ID'ye göre mesaj getirme metodu
        {
            var values = await _messageContext.UserMessages.FindAsync(messageID); //Verilen ID'ye sahip mesajı veritabanında arar
            return _mapper.Map<GetByIDMessageDTO>(values); //Bulunan mesajı GetByIDMessageDTO tipine dönüştürüp döner (Task yerine async-await kullanıldı)
        }

        public async Task<List<ResultInboxMessageDTO>> GetInboxMessageAsync(string messageID) //Gelen kutusu mesajlarını listeleme metodu
        {
            var values = await _messageContext.UserMessages
                .Where(x => x.MessageReceiverID == messageID) //Alıcısı belirtilen kullanıcı olan mesajları filtreler
                .ToListAsync(); //Sonuçları listeye dönüştürür

            return _mapper.Map<List<ResultInboxMessageDTO>>(values); //Alınan mesaj listesini ResultInboxMessageDTO tipine dönüştürüp döner
        }

        public async Task<List<ResultSendboxMessageDTO>> GetSendboxMessageAsync(string messageID) //Giden kutusu mesajlarını listeleme metodu
        {
            var values = await _messageContext.UserMessages
                .Where(x => x.MessageSenderID == messageID) //Göndericisi belirtilen kullanıcı olan mesajları filtreler
                .ToListAsync(); //Sonuçları listeye dönüştürür

            return _mapper.Map<List<ResultSendboxMessageDTO>>(values); //Alınan mesaj listesini ResultSendboxMessageDTO tipine dönüştürüp döner
        }

        public async Task<int> GetTotalMessageCount()
        {
            int values = await _messageContext.UserMessages.CountAsync();
            return values;
        }

        public async Task<int> GetTotalMessageCountByReceiverID(string id)
        {
            var values =await _messageContext.UserMessages.Where(x => x.MessageReceiverID == id).CountAsync(); // 
            return values;
        }

        public async Task UpdateMessageAsync(UpdateMessageDTO updatemessageDTO) //Mevcut mesajı güncelleme metodu
        {
            var values = await _messageContext.UserMessages.FindAsync(updatemessageDTO.UserMessageID); //Verilen ID'ye sahip mesajı veritabanında arar
            if (values != null) //Eğer mesaj bulunursa
            {
                _mapper.Map(updatemessageDTO, values); //DTO içindeki güncel bilgileri entity üzerine uygular
                _messageContext.UserMessages.Update(values); //Bulunan mesajı DbSet'te günceller
                await _messageContext.SaveChangesAsync(); //Değişiklikleri veritabanına kaydeder (eksikti, eklendi)
            }
        }
    }
}
