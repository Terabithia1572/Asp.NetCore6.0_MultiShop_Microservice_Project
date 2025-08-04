using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Options;
using MultiShop.Basket.LoginServices;
using MultiShop.Basket.Services;
using MultiShop.Basket.Settings;

var builder = WebApplication.CreateBuilder(args);

var requireAuthorizationPolicy = new AuthorizationPolicyBuilder() // Yetkilendirme politikasý oluþturma
    .RequireAuthenticatedUser() // Kimlik doðrulamasý gerektirir
    .Build(); // Yetkilendirme politikasýný oluþturur

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
builder.Services.Configure<RedisService>(builder.Configuration.GetSection("RedisSettings")); // Redis ayarlarýný yapýlandýrma
builder.Services.AddSingleton<RedisService>(sp =>
{
    var redisSettings = sp.GetRequiredService<IOptions<RedisService>>().Value; // Redis ayarlarýný al
    var redis= new RedisService(redisSettings._host,redisSettings._port); // Redis servisini oluþtur
    redis.Connect(); // Redis'e baðlan
    return redis; // Redis servisini döner
}); // Redis servisini tekil olarak ekleme

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
