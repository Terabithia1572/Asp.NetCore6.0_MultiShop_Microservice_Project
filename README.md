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

## 📸 Öne Çıkan Ekran Görüntüleri

<p align="center">
  <img src="screenshots/Ek Açıklama 2025-11-02 163744.png" style="max-width:100%; height:auto;" alt="Ana Sayfa"/>
  <img src="screenshots/Ek Açıklama 2025-11-02 163820.png" style="max-width:100%; height:auto;" alt="Ürün Detay"/>
  <img src="screenshots/Ek Açıklama 2025-11-02 163841.png" style="max-width:100%; height:auto;" alt="Sepet Görünümü"/>
  <img src="screenshots/Ek Açıklama 2025-11-02 164254.png" style="max-width:100%; height:auto;" alt="Kampanyalar ve İndirimler"/>
  <img src="screenshots/Ek Açıklama 2025-11-02 164422.png" style="max-width:100%; height:auto;" alt="Yorum & Puanlama"/>
  <img src="screenshots/Ek Açıklama 2025-11-02 164609.png" style="max-width:100%; height:auto;" alt="Kullanıcı Dashboard"/>
  <img src="screenshots/Ek Açıklama 2025-11-02 164755.png" style="max-width:100%; height:auto;" alt="Bildirim Sistemi (SignalR)"/>
  <img src="screenshots/Ek Açıklama 2025-11-02 165044.png" style="max-width:100%; height:auto;" alt="Admin Panel / Yönetici Arayüzü"/>
  <img src="screenshots/Ek Açıklama 2025-11-02 165109.png" style="max-width:100%; height:auto;" alt="Responsive Görünüm"/>
  <img src="screenshots/Ek Açıklama 2025-11-02 165405.png" style="max-width:100%; height:auto;" alt="MultiShop Premium Tema"/>
</p>

> 📂 **Tüm ekran görüntüleri için:**  
> [screenshots klasörüne git →](https://github.com/Terabithia1572/Asp.NetCore6.0_MultiShop_Microservice_Project/tree/master/screenshots)

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

---

<p align="center">
  <sub>🚀 Developed by <strong>Yunus İNAN</strong> • 
  <a href="https://github.com/Terabithia1572">GitHub</a> • 
  <a href="https://youtu.be/3rQURdOtYv8">YouTube Demo</a>
  </sub>
</p>
