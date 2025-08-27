using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.DTOLayer.CatalogDTOs.ContactDTOs
{
    public class CreateContactDTO
    {
        public string ContactNameSurname { get; set; } //İletişim Adını Tuttuk.
        public string ContactEmail { get; set; } //İletişim Resim Görselini Tuttuk.
        public string ContactSubject { get; set; } //İletişim Konu Başlığını Tuttuk.
        public string ContactMessage { get; set; } //İletişim Mesajını Tuttuk.
        public DateTime ContactCreatedDate { get; set; } //İletişim Oluşturma Tarihini Tuttuk.
        public bool ContactIsRead { get; set; } //İletişim Durumunu Tuttuk.
    }
}
