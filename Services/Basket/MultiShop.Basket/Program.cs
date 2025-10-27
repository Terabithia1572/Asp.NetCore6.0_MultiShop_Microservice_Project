using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Options;
using MultiShop.Basket.LoginServices;
using MultiShop.Basket.Services;
using MultiShop.Basket.Settings;
using System.IdentityModel.Tokens.Jwt;

var builder = WebApplication.CreateBuilder(args);

var requireAuthorizationPolicy = new AuthorizationPolicyBuilder() // Yetkilendirme politikas? olu?turma
    .RequireAuthenticatedUser() // Kimlik do?rulamas? gerektirir
    .Build(); // Yetkilendirme politikas?n? olu?turur

JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub"); // JWT'den gelen "sub" (subject) claim'ini varsay?lan olarak haritalamaktan kald?r?r

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = builder.Configuration["IdentityServerUrl"]; // IdentityServer URL'si
        options.Audience = "ResourceBasket"; // API Audience
        options.RequireHttpsMetadata = false; // HTTPS gereksinimi
    });

builder.Services.AddHttpContextAccessor(); // HttpContext eri?imi için gerekli
builder.Services.AddScoped<ILoginService, LoginService>(); // Kullan?c? kimli?i yönetimi için servis
builder.Services.AddScoped<IBasketService, BasketService>(); // Sepet i?lemleri için servis
builder.Services.Configure<RedisSettings>(builder.Configuration.GetSection("RedisSettings")); // Redis ayarlar?n? yap?land?rma

builder.Services.AddSingleton<RedisService>(sp => // RedisService'i DI konteynerine ekleme
{
    var redisSettings = sp.GetRequiredService<IOptions<RedisSettings>>().Value; // Redis ayarlar?n? al
    var redis = new RedisService(redisSettings.Host, redisSettings.Port); // RedisService nesnesini olu?tur
    redis.Connect(); // Redis'e ba?lant? kur
    return redis; // RedisService nesnesini döner
});

builder.Services.AddControllers(opt =>
{
    opt.Filters.Add(new AuthorizeFilter(requireAuthorizationPolicy)); // Tüm denetleyicilere yetkilendirme politikas? uygula
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
app.UseAuthentication(); // Kimlik do?rulama middleware'ini ekle

app.UseAuthorization();

app.MapControllers();

app.Run();