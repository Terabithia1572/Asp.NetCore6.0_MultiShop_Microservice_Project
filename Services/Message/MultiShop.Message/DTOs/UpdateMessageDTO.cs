namespace MultiShop.Message.DTOs
{
    public class UpdateMessageDTO
    {
        public int UserMessageID { get; set; } //Mesaj ID'si Primary Key olarak tanımlanır
        public string MessageSenderID { get; set; } //Mesaj Gönderen Kullanıcı ID'si
        public string MessageReceiverID { get; set; } //Mesaj Alan Kullanıcı ID'si
        public string MessageSubject { get; set; } //Mesaj Konusu
        public string MessageDetail { get; set; } //Mesaj Detayı 
        public DateTime MessageDate { get; set; } //Mesajın Gönderildiği Tarih
        public bool MessageStatus { get; set; } //Mesajın Okunma Durumu (Okundu/Okunmadı)
    }
}
