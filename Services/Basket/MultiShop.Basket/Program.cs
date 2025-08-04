using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Options;
using MultiShop.Basket.LoginServices;
using MultiShop.Basket.Services;
using MultiShop.Basket.Settings;
using System.IdentityModel.Tokens.Jwt;

var builder = WebApplication.CreateBuilder(args);

var requireAuthorizationPolicy = new AuthorizationPolicyBuilder() // Yetkilendirme politikasý oluþturma
    .RequireAuthenticatedUser() // Kimlik doðrulamasý gerektirir
    .Build(); // Yetkilendirme politikasýný oluþturur

JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub"); // JWT'den gelen "sub" (subject) claim'ini varsayýlan olarak haritalamaktan kaldýrýr

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = builder.Configuration["IdentityServerUrl"]; // IdentityServer URL'si
        options.Audience="ResourceBasket"; // API Audience
        options.RequireHttpsMetadata = false; // HTTPS gereksinimi
    });

builder.Services.AddHttpContextAccessor(); // HttpContext eriþimi için gerekli
builder.Services.AddScoped<ILoginService, LoginService>(); // Kullanýcý kimliði yönetimi için servis
builder.Services.AddScoped<IBasketService, BasketService>(); // Sepet iþlemleri için servis
builder.Services.Configure<RedisSettings>(builder.Configuration.GetSection("RedisSettings")); // Redis ayarlarýný yapýlandýrma

builder.Services.AddSingleton<RedisService>(sp => // RedisService'i DI konteynerine ekleme
{
    var redisSettings = sp.GetRequiredService<IOptions<RedisSettings>>().Value; // Redis ayarlarýný al
    var redis = new RedisService(redisSettings.Host, redisSettings.Port); // RedisService nesnesini oluþtur
    redis.Connect(); // Redis'e baðlantý kur
    return redis; // RedisService nesnesini döner
});

builder.Services.AddControllers(opt =>
{
    opt.Filters.Add(new AuthorizeFilter(requireAuthorizationPolicy)); // Tüm denetleyicilere yetkilendirme politikasý uygula
});
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

app.UseHttpsRedirection();
app.UseAuthentication(); // Kimlik doðrulama middleware'ini ekle

app.UseAuthorization();

app.MapControllers();

app.Run();
