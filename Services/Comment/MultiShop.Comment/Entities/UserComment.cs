
namespace MultiShop.Comment.Entities
{
    public class UserComment
    {
        public int UserCommentID { get; set; } //Kullanıcı Yorum ID
        public string UserCommentNameSurname { get; set; } //Kullanıcı Yorum Ad Soyad
        public string? UserCommentImageURL { get; set; } //Kullanıcı Yorum Resim URL
        public string UserCommentEmail { get; set; } //Kullanıcı Yorum Email
        public string UserCommentDetail { get; set; } //Kullanıcı Yorum Detay
        public int UserCommentRating { get; set; } //Kullanıcı Yorum Puan
        public DateTime UserCommentCreatedDate { get; set; } //Kullanıcı Yorum Oluşturma Tarihi
        public bool UserCommentStatus { get; set; } //Kullanıcı Yorum Durum
        public string ProductID { get; set; } //Ürün ID
    }
}
