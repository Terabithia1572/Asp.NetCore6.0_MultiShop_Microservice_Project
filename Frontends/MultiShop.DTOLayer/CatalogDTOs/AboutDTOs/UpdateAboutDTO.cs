using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.DTOLayer.CatalogDTOs.AboutDTOs
{
    public class UpdateAboutDTO
    {
        public string AboutID { get; set; } // MongoDB'de benzersiz kimlik olarak kullanılacak
        public string AboutDescription { get; set; } //Hakkında Açıklama Bilgilerini burada tutacak. 
        public string AboutAddress { get; set; } //Hakkında Adres Bilgilerini burada tutacak.
        public string AboutEmail { get; set; } // Hakkında Mail Bilgilerini burada tutacak.
        public string AboutPhone { get; set; } // Hakkında Telefon Bilgilerini burada tutacak.
    }
}
