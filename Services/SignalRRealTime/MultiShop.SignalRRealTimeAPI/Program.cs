var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// CORS (Cross-Origin Resource Sharing) ayarlarını ekliyoruz.
builder.Services.AddCors(opt =>
{
    // "CorsPolicy" adında bir CORS politikası oluşturuyoruz.
    opt.AddPolicy("CorsPolicy", builder =>
    {
        // Herhangi bir header (başlık) kullanımına izin veriyoruz.
        builder.AllowAnyHeader()

               // Herhangi bir HTTP metoduna (GET, POST, PUT, DELETE vs.) izin veriyoruz.
               .AllowAnyMethod()

               // İstek yapılabilecek kaynak (origin) kısıtlamasını kaldırıyoruz.
               // Burada (host) => true demek, "tüm domainlerden gelen isteklere izin ver" anlamına gelir.
               .SetIsOriginAllowed((host) => true)

               // Kimlik doğrulama bilgileri (cookie, token vs.) içeren isteklerin kabul edilmesine izin veriyoruz.
               .AllowCredentials();
    });
});

// NOTLARIM 🧠
// 🔹 AddCors -> Uygulama genelinde CORS yapılandırması ekler.
// 🔹 AddPolicy -> Farklı senaryolar için isimlendirilmiş CORS politikaları tanımlamayı sağlar.
// 🔹 AllowAnyHeader -> Her türlü başlığa izin verir.
// 🔹 AllowAnyMethod -> Her türlü HTTP metoduna izin verir.
// 🔹 SetIsOriginAllowed((host) => true) -> Tüm domainlerden gelen isteklere izin verir (geliştirme ortamı için uygundur).
// 🔹 AllowCredentials -> Çerez, kimlik doğrulama gibi bilgilerin paylaşılmasına izin verir.
// ⚠️ Production ortamında “(host) => true” yerine belirli domain(ler) tanımlanmalıdır!

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("CorsPolicy"); // CorsPolicy'i kullan demek
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
