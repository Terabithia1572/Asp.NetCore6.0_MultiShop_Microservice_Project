using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using MultiShop.Basket.LoginServices;
using MultiShop.Basket.Services;
using MultiShop.Basket.Settings;

var builder = WebApplication.CreateBuilder(args);

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
