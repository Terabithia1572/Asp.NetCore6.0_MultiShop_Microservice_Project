using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.DTOLayer.CommentDTOs
{
    public class CreateCommentDTO
    {
     
        public string UserCommentNameSurname { get; set; } //Kullanıcı Yorum Ad Soyad
        public string? UserCommentImageURL { get; set; } //Kullanıcı Yorum Resim URL
        public string UserCommentEmail { get; set; } //Kullanıcı Yorum Email
        public string UserCommentDetail { get; set; } //Kullanıcı Yorum Detay
        public int UserCommentRating { get; set; } //Kullanıcı Yorum Puan
        public DateTime UserCommentCreatedDate { get; set; } //Kullanıcı Yorum Oluşturma Tarihi
        public bool UserCommentStatus { get; set; } //Kullanıcı Yorum Durum
        public string ProductID { get; set; } //Ürün ID
        public string? UserId { get; set; } // Yorumu yapan kullanıcının ID'si

    }
}
