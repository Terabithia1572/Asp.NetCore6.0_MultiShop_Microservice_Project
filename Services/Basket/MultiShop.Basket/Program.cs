using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Options;
using MultiShop.Basket.LoginServices;
using MultiShop.Basket.Services;
using MultiShop.Basket.Settings;

var builder = WebApplication.CreateBuilder(args);

var requireAuthorizationPolicy = new AuthorizationPolicyBuilder() // Yetkilendirme politikas� olu�turma
    .RequireAuthenticatedUser() // Kimlik do�rulamas� gerektirir
    .Build(); // Yetkilendirme politikas�n� olu�turur

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = builder.Configuration["IdentityServerUrl"]; // IdentityServer URL'si
        options.Audience="ResourceBasket"; // API Audience
        options.RequireHttpsMetadata = false; // HTTPS gereksinimi
    });

builder.Services.AddHttpContextAccessor(); // HttpContext eri�imi i�in gerekli
builder.Services.AddScoped<ILoginService, LoginService>(); // Kullan�c� kimli�i y�netimi i�in servis
builder.Services.AddScoped<IBasketService, BasketService>(); // Sepet i�lemleri i�in servis
builder.Services.Configure<RedisService>(builder.Configuration.GetSection("RedisSettings")); // Redis ayarlar�n� yap�land�rma
builder.Services.AddSingleton<RedisService>(sp =>
{
    var redisSettings = sp.GetRequiredService<IOptions<RedisService>>().Value; // Redis ayarlar�n� al
    var redis= new RedisService(redisSettings._host,redisSettings._port); // Redis servisini olu�tur
    redis.Connect(); // Redis'e ba�lan
    return redis; // Redis servisini d�ner
}); // Redis servisini tekil olarak ekleme

builder.Services.AddControllers(opt =>
{
    opt.Filters.Add(new AuthorizeFilter(requireAuthorizationPolicy)); // T�m denetleyicilere yetkilendirme politikas� uygula
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
app.UseAuthentication(); // Kimlik do�rulama middleware'ini ekle

app.UseAuthorization();

app.MapControllers();

app.Run();
