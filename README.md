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

<p align="center">
  <img src="https://i.ibb.co/pvGBTJJb/Ek-A-klama-2025-11-02-163744.png" width="900"/>
  <img src="https://i.ibb.co/pv70hnHG/Ek-A-klama-2025-11-02-163755.png" width="900"/>
  <img src="https://i.ibb.co/Y4bWCR7k/Ek-A-klama-2025-11-02-163820.png" width="900"/>
  <img src="https://i.ibb.co/6Jrk2RnK/Ek-A-klama-2025-11-02-163830.png" width="900"/>
  <img src="https://i.ibb.co/Q4QjjMH/Ek-A-klama-2025-11-02-163841.png" width="900"/>
  <img src="https://i.ibb.co/Xxr1Mxpy/Ek-A-klama-2025-11-02-163854.png" width="900"/>
  <img src="https://i.ibb.co/Q7ztdYGk/Ek-A-klama-2025-11-02-164254.png" width="900"/>
  <img src="https://i.ibb.co/Xf9xjqr7/Ek-A-klama-2025-11-02-164304.png" width="900"/>
  <img src="https://i.ibb.co/Q3NQLwLg/Ek-A-klama-2025-11-02-164315.png" width="900"/>
  <img src="https://i.ibb.co/C3pv6ghV/Ek-A-klama-2025-11-02-164327.png" width="900"/>
  <img src="https://i.ibb.co/zH2qX9Vg/Ek-A-klama-2025-11-02-164351.png" width="900"/>
  <img src="https://i.ibb.co/mrRcN8XG/Ek-A-klama-2025-11-02-164406.png" width="900"/>
  <img src="https://i.ibb.co/bjfRnkQ2/Ek-A-klama-2025-11-02-164422.png" width="900"/>
  <img src="https://i.ibb.co/hJMhMmK5/Ek-A-klama-2025-11-02-164435.png" width="900"/>
  <img src="https://i.ibb.co/qLVNK4m9/Ek-A-klama-2025-11-02-164459.png" width="900"/>
  <img src="https://i.ibb.co/TxZpw2wN/Ek-A-klama-2025-11-02-164609.png" width="900"/>
  <img src="https://i.ibb.co/bgnfjVP1/Ek-A-klama-2025-11-02-164618.png" width="900"/>
  <img src="https://i.ibb.co/0RsX9SF0/Ek-A-klama-2025-11-02-164644.png" width="900"/>
  <img src="https://i.ibb.co/ycbRNnpb/Ek-A-klama-2025-11-02-164654.png" width="900"/>
  <img src="https://i.ibb.co/spLyKZ3D/Ek-A-klama-2025-11-02-164704.png" width="900"/>
  <img src="https://i.ibb.co/bM6L40Q7/Ek-A-klama-2025-11-02-164715.png" width="900"/>
  <img src="https://i.ibb.co/HTPjhHhC/Ek-A-klama-2025-11-02-164733.png" width="900"/>
  <img src="https://i.ibb.co/9kjqKKfx/Ek-A-klama-2025-11-02-164755.png" width="900"/>
  <img src="https://i.ibb.co/DgmLLNVg/Ek-A-klama-2025-11-02-164804.png" width="900"/>
  <img src="https://i.ibb.co/mrNC1Y9X/Ek-A-klama-2025-11-02-164819.png" width="900"/>
  <img src="https://i.ibb.co/yF9hrRHZ/Ek-A-klama-2025-11-02-164831.png" width="900"/>
  <img src="https://i.ibb.co/hFwSzNvX/Ek-A-klama-2025-11-02-164840.png" width="900"/>
  <img src="https://i.ibb.co/cSX0vdG1/Ek-A-klama-2025-11-02-164849.png" width="900"/>
  <img src="https://i.ibb.co/fPH96N3/Ek-A-klama-2025-11-02-164858.png" width="900"/>
  <img src="https://i.ibb.co/cBD7LWD/Ek-A-klama-2025-11-02-164917.png" width="900"/>
  <img src="https://i.ibb.co/Xk2X9MSD/Ek-A-klama-2025-11-02-164927.png" width="900"/>
  <img src="https://i.ibb.co/Df5NthsQ/Ek-A-klama-2025-11-02-164939.png" width="900"/>
  <img src="https://i.ibb.co/gMYCx5qv/Ek-A-klama-2025-11-02-164947.png" width="900"/>
  <img src="https://i.ibb.co/zV5WrzQG/Ek-A-klama-2025-11-02-164955.png" width="900"/>
  <img src="https://i.ibb.co/hxbbFDhp/Ek-A-klama-2025-11-02-165009.png" width="900"/>
  <img src="https://i.ibb.co/wNBWKhFq/Ek-A-klama-2025-11-02-165017.png" width="900"/>
  <img src="https://i.ibb.co/tp55kB2Y/Ek-A-klama-2025-11-02-165028.png" width="900"/>
  <img src="https://i.ibb.co/KjGg5G5k/Ek-A-klama-2025-11-02-165036.png" width="900"/>
  <img src="https://i.ibb.co/1Y3tTPBX/Ek-A-klama-2025-11-02-165044.png" width="900"/>
  <img src="https://i.ibb.co/cckXR6k7/Ek-A-klama-2025-11-02-165052.png" width="900"/>
  <img src="https://i.ibb.co/W4p7X3WW/Ek-A-klama-2025-11-02-165101.png" width="900"/>
  <img src="https://i.ibb.co/mrv7qy3d/Ek-A-klama-2025-11-02-165109.png" width="900"/>
  <img src="https://i.ibb.co/dswsNHtt/Ek-A-klama-2025-11-02-165120.png" width="900"/>
  <img src="https://i.ibb.co/mrhDh0yy/Ek-A-klama-2025-11-02-165132.png" width="900"/>
  <img src="https://i.ibb.co/hxvfkgCP/Ek-A-klama-2025-11-02-165143.png" width="900"/>
  <img src="https://i.ibb.co/BbKrgb9/Ek-A-klama-2025-11-02-165152.png" width="900"/>
  <img src="https://i.ibb.co/KcHFZwLL/Ek-A-klama-2025-11-02-165203.png" width="900"/>
  <img src="https://i.ibb.co/4RBTPBf7/Ek-A-klama-2025-11-02-165215.png" width="900"/>
  <img src="https://i.ibb.co/MkzHtMWR/Ek-A-klama-2025-11-02-165246.png" width="900"/>
  <img src="https://i.ibb.co/Qjtf8zmz/Ek-A-klama-2025-11-02-165301.png" width="900"/>
  <img src="https://i.ibb.co/7xjzDt9M/Ek-A-klama-2025-11-02-165310.png" width="900"/>
  <img src="https://i.ibb.co/gb5mcp6t/Ek-A-klama-2025-11-02-165329.png" width="900"/>
  <img src="https://i.ibb.co/qLK6n4tt/Ek-A-klama-2025-11-02-165337.png" width="900"/>
  <img src="https://i.ibb.co/vfM6SVS/Ek-A-klama-2025-11-02-165345.png" width="900"/>
  <img src="https://i.ibb.co/8nGDtnrS/Ek-A-klama-2025-11-02-165405.png" width="900"/>
</p>


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
