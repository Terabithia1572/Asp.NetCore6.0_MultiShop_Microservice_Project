# 🏬 MultiShop Mikroservis E-Ticaret Projesi

🎉 ASP.NET Core 6.0 tabanlı **tam ölçekli bir mikroservis e-ticaret sistemi** geliştirdim.  
Bu proje, **Murat Yücedağ’ın** eğitim serisinden ilham alınarak oluşturulmuş, ancak **tema, arayüz, UI/UX ve tüm tasarım kodları sıfırdan tarafımdan yazılmıştır.**

---

## 🚀 Proje Genel Özellikleri

- 24 bağımsız mikroservis  
- Docker Compose ile izole konteyner ortamı  
- Ocelot Gateway ile merkezi yönlendirme  
- RabbitMQ ile asenkron haberleşme  
- IdentityServer4 + JWT kimlik doğrulama  
- Onion Architecture, CQRS, Mediator mimarisi  
- Polyglot Persistence (MongoDB, PostgreSQL, MSSQL, Redis)  
- SignalR ile gerçek zamanlı bildirim ve mesajlaşma  
- Globalization (çoklu dil desteği, TR/EN)  

---

## 🧩 Mikroservisler

| Servis | Veritabanı | Açıklama |
|--------|-------------|----------|
| 🛒 **Basket** | Redis | Token bazlı kullanıcı sepet yönetimi |
| 📦 **Catalog** | MongoDB | Ürün, kategori, marka, vitrin ve kampanya verileri |
| 💬 **Comment** | MSSQL | Kullanıcı yorumları ve puanlama sistemi |
| 💰 **Discount** | MSSQL (Dapper) | Kupon ve indirim motoru |
| 🧾 **Order** | PostgreSQL | Onion + CQRS + MediatR mimarisi |
| 🚚 **Cargo** | MSSQL | Kargo takip ve teslimat entegrasyonu |
| 💳 **Payment** | Soyutlama | Ödeme yönetimi |
| 🔐 **Identity** | MSSQL | Kullanıcı tabanı (IdentityServer4 + JWT) |
| ⚡ **SignalR Hub** | — | Gerçek zamanlı bildirim ve mesaj sistemi |

---

## 💻 Frontend (Tema Tarafı)

✨ **Tüm tema ve arayüz sıfırdan geliştirildi.**  
Modern, responsive ve premium bir tasarım anlayışıyla oluşturuldu.

### 🔹 Özellikler:
- Dinamik vitrin, öne çıkan ürünler ve kampanya banner’ları  
- Kategori/marka filtreleme ve istatistik alanları  
- Mini-sepet ve favoriler (AJAX tabanlı)  
- Yorum & puanlama entegrasyonu  
- İndirim rozetleri (% veya YENİ etiketi)  
- Globalization (TR/EN)  
- SEO optimizasyonu, temiz URL yapısı (slug)  
- Toastr bildirimler, animasyonlu geçişler, smooth UI deneyimi  

---

## 🛠️ Kullanılan Teknolojiler

**Backend & Araçlar:**  
ASP.NET Core 6 • Ocelot Gateway • RabbitMQ • SignalR • Swagger • Postman • Dapper  

**Veritabanları:**  
MongoDB • PostgreSQL • MSSQL • Redis  

**Mimariler:**  
Onion Architecture • CQRS • Mediator • Repository Pattern • JWT • Polyglot Persistence  

**DevOps:**  
Docker Compose • HealthChecks • Serilog  

**Frontend:**  
Bootstrap 5 + SASS • ES6 • Responsive UI • SEO/OG etiketleri  

---

## 📸 Ekran Görüntüleri

![Ana Sayfa](https://i.ibb.co/SXW4gRR/1.png)
![Ürün Detay](https://i.ibb.co/8DCKYXyT/2.png)
![Sepet](https://i.ibb.co/p6nP70vz/3.png)
![Favoriler](https://i.ibb.co/n86FTsgX/4.png)
![Kampanyalar](https://i.ibb.co/jSfZZDw/5.png)
![Kategori Yönetimi](https://i.ibb.co/5hgmwhvY/6.png)
![Siparişler](https://i.ibb.co/7x8L4rFQ/7.png)
![Yorumlar](https://i.ibb.co/ZR3zG0pB/8.png)
![Admin Panel](https://i.ibb.co/1tqQ1W1g/9.png)
![Bildirim Sistemi](https://i.ibb.co/DHPkYh5w/10.png)
![Kargo Takip](https://i.ibb.co/wrQ5SGhq/11.png)
![Kupon Ekleme](https://i.ibb.co/hxgmYfZy/12.png)
![İndirim Yönetimi](https://i.ibb.co/gbnFCxmP/13.png)
![Kullanıcı Profili](https://i.ibb.co/4nf9fVjy/14.png)
![Dashboard](https://i.ibb.co/BHFyb7Tj/15.png)
![Oturum Açma](https://i.ibb.co/dwS1fMfH/16.png)
![Kayıt Olma](https://i.ibb.co/KjH1cgX6/17.png)
![Yönetici Paneli](https://i.ibb.co/NdT3NPm0/18.png)
![E-Ticaret Ana Görünüm](https://i.ibb.co/dwzcf0tz/19.png)
![Ocelot Gateway Yapısı](https://i.ibb.co/rKn4xVyL/20.png)

---

## 🎥 YouTube Tanıtım Videosu

📺 **MultiShop Mikroservis E-Ticaret Projesi (Full Demo + Mimari Anlatım)**  
👉 [https://youtu.be/3rQURdOtYv8](https://youtu.be/3rQURdOtYv8)

---

## 🔗 Kaynak Kod

🔗 [GitHub Repository](https://github.com/Terabithia1572/Asp.NetCore6.0_MultiShop_Microservice_Project)

---

## 🏷️ Etiketler
#DotNet #AspNetCore #Microservices #CSharp #API #MongoDB #PostgreSQL #Redis #Docker #RabbitMQ #SignalR #CQRS #Mediator #OcelotGateway #ECommerce #FullStackDeveloper #JWT #SoftwareArchitecture #LearningByDoing #Udemy #TechCommunity
