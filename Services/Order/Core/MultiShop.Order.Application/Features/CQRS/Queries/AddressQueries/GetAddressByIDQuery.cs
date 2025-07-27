using MultiShop.Order.Application.Features.CQRS.Results.AddressResults;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Order.Application.Features.CQRS.Queries.AddressQueries
{
    public class GetAddressByIDQuery
    {
        public int AddressID { get; set; } // Sorgulanacak adresin benzersiz kimliği (ID'si)

        public GetAddressByIDQuery(int addressID)
        {
            AddressID = addressID; // Constructor üzerinden, sorgu oluşturulurken AddressID bilgisinin zorunlu olarak atanmasını sağlar.
        }
    }
}

//1.Neden Constructor Geçtin?
//GetAddressByIDQuery sınıfı, CQRS (Command Query Responsibility Segregation) deseninde bir "query request" objesidir.

//Buradaki amaç: Bir adresin ID’siyle sorgulanmasını istemek.

//Constructor’da parametreyle alman, objenin yaratılırken “ID” bilgisini zorunlu olarak almasını sağlar.
//Bu şekilde, yanlış veya eksik veriyle query nesnesi oluşturulmasını önlersin.

//Yani, adres ID’si olmadan sorgu başlatılamaz. Bu da daha güvenli ve hatasız kod anlamına gelir.

//2. Neden Sonuna "Result" Eklenmedi?
//Query sınıfı (GetAddressByIDQuery), bir "istek" (request) modelidir.
//Yani “bana şu ID ile adresi getir” anlamında sorgu isteği gönderiyorsun.

//Result ise genellikle “bu sorgunun döndürdüğü veri” için kullanılır.
//Örnek: GetAddressByIDQueryResult

//Senin yazdığın request nesnesi, sadece parametreleri içerir (gelen veri modelini değil).

//Cevap modeli (dönen data), genelde ayrı bir sınıf olarak (örneğin GetAddressByIDQueryResult) tanımlanır.