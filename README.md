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

![Ana Sayfa](https://i.ibb.co/pvGBTJJb/Ek-A-klama-2025-11-02-163744.png)
![Ürün Detay](https://i.ibb.co/pv70hnHG/Ek-A-klama-2025-11-02-163755.png)
![Sepet](https://i.ibb.co/Y4bWCR7k/Ek-A-klama-2025-11-02-163820.png)
![Favoriler](https://i.ibb.co/6Jrk2RnK/Ek-A-klama-2025-11-02-163830.png)
![Admin Panel](https://i.ibb.co/Q4QjjMH/Ek-A-klama-2025-11-02-163841.png)
![Bildirim Sistemi](https://i.ibb.co/Xxr1Mxpy/Ek-A-klama-2025-11-02-163854.png)
![Kargo Takip](https://i.ibb.co/Q7ztdYGk/Ek-A-klama-2025-11-02-164254.png)
![Kampanya Banner](https://i.ibb.co/Xf9xjqr7/Ek-A-klama-2025-11-02-164304.png)
![Responsive Görünüm](https://i.ibb.co/Q3NQLwLg/Ek-A-klama-2025-11-02-164315.png)
![Kullanıcı Dashboard](https://i.ibb.co/C3pv6ghV/Ek-A-klama-2025-11-02-164327.png)

> 📂 **Tüm ekran görüntülerinin tam listesi:**  
> [screenshots/ klasöründen görüntüle »](https://github.com/Terabithia1572/Asp.NetCore6.0_MultiShop_Microservice_Project/tree/main/screenshots)



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
