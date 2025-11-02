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

![Ana Sayfa](https://i.ibb.co/SXW4gRR5)
![Ürün Detay](https://i.ibb.co/8DCKYXyT)
![Sepet](https://i.ibb.co/p6nP70vz)
![Favoriler](https://i.ibb.co/n86FTsgX)
![Kampanyalar](https://i.ibb.co/jSfZZDw)
![Kategori Yönetimi](https://i.ibb.co/5hgmwhvY)
![Siparişler](https://i.ibb.co/7x8L4rFQ)
![Yorumlar](https://i.ibb.co/ZR3zG0pB)
![Admin Panel](https://i.ibb.co/1tqQ1W1g)
![Bildirim Sistemi](https://i.ibb.co/DHPkYh5w)
![Kargo Takip](https://i.ibb.co/wrQ5SGhq)
![Kupon Ekleme](https://i.ibb.co/hxgmYfZy)
![İndirim Yönetimi](https://i.ibb.co/gbnFCxmP)
![Kullanıcı Profili](https://i.ibb.co/4nf9fVjy)
![Dashboard](https://i.ibb.co/BHFyb7Tj)
![Oturum Açma](https://i.ibb.co/dwS1fMfH)
![Kayıt Olma](https://i.ibb.co/KjH1cgX6)
![Yönetici Paneli](https://i.ibb.co/NdT3NPm0)
![E-Ticaret Ana Görünüm](https://i.ibb.co/dwzcf0tz)
![Ocelot Gateway Yapısı](https://i.ibb.co/rKn4xVyL)
![SignalR Bildirim](https://i.ibb.co/5g1vQ0G2)
![RapidAPI Entegrasyonu](https://i.ibb.co/8gxHY7Yb)
![Sepet Güncelleme](https://i.ibb.co/1tx9yypC)
![Sipariş Özeti](https://i.ibb.co/TqJrrZvq)
![Tema Tasarımı](https://i.ibb.co/qLJMX4NW)
![Mobil Görünüm](https://i.ibb.co/60MvkZjT)
![Kargo Modülü](https://i.ibb.co/chVjn9db)
![Mesajlaşma Sistemi](https://i.ibb.co/4ngzfx67)
![Blog Yönetimi](https://i.ibb.co/8S9bvxy)
![Kampanya Sayfası](https://i.ibb.co/07hdt0h)
![Kullanıcı Dashboard](https://i.ibb.co/84KMJt6b)
![Admin Dashboard](https://i.ibb.co/PGwRNK3g)
![Ürün Yönetimi](https://i.ibb.co/WWC1Lb8D)
![Kategori Listesi](https://i.ibb.co/ZzJRV0GW)
![Sipariş Geçmişi](https://i.ibb.co/RGffTzZL)
![Hızlı Ekleme](https://i.ibb.co/xSXjMqtb)
![Kargo Şirketleri](https://i.ibb.co/gZCCHyWt)
![Yeni Ürün Ekleme](https://i.ibb.co/ccv9yvyH)
![Ürün Kartları](https://i.ibb.co/xKyt6Bwg)
![Kampanya Banner](https://i.ibb.co/RpY4t3YV)
![Kullanıcı Girişi](https://i.ibb.co/DHgmFbDD)
![Admin Ayarları](https://i.ibb.co/XfyTs5q6)
![Blog Detay](https://i.ibb.co/4wnwb022)
![İstatistikler](https://i.ibb.co/twYmYz22)
![Yönetici Menü](https://i.ibb.co/bjnJx67S)
![Ürün Listesi](https://i.ibb.co/hb1ZKbS)
![Renk Teması](https://i.ibb.co/LDfnw5JJ)
![Responsive Görünüm](https://i.ibb.co/Ld4tn4P8)
![Bildirim Detay](https://i.ibb.co/d4NvVG1Q)
![Dashboard Cards](https://i.ibb.co/Pz0NT3C3)
![Admin Sidebar](https://i.ibb.co/hxLMNFqQ)
![E-Ticaret Dashboard](https://i.ibb.co/V07JkZ3M)
![Sepet Özeti](https://i.ibb.co/SDbSx1hh)
![Login Modal](https://i.ibb.co/ZRv67RBk)
![Mobil Menü](https://i.ibb.co/YJVFxPx)


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
