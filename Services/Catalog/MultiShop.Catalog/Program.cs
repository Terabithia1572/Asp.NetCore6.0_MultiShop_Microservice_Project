using Microsoft.Extensions.Options;
using MultiShop.Catalog.Services.CategoryServices;
using MultiShop.Catalog.Services.ProductImageServices;
using MultiShop.Catalog.Services.ProductServices;
using MultiShop.Catalog.Settings;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ICategoryService, CategoryService>(); // Her istek i�in yeni bir CategoryService �rne�i olu�turur ve ICategoryService aray�z�ne ba��ml�l�klar� bu s�n�fa y�nlendirir (Dependency Injection).
builder.Services.AddScoped<IProductService, ProductService>(); // Her istek i�in yeni bir ProductService �rne�i olu�turur ve IProductService aray�z�ne ba��ml�l�klar� bu s�n�fa y�nlendirir.
builder.Services.AddScoped<IProductImageService, ProductImageService>(); // Her istek i�in yeni bir ProductImageService �rne�i olu�turur ve IProductImageService aray�z�ne ba��ml�l�klar� bu s�n�fa y�nlendirir.

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly()); // Uygulamadaki t�m AutoMapper profillerini y�kler ve otomatik e�leme (mapping) i�lemlerinin kullan�lmas�n� sa�lar.

builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("DatabaseSettings")); // appsettings.json'daki DatabaseSettings b�l�m�n�, DatabaseSettings s�n�f�na ba�lar ve yap�land�r�r.

builder.Services.AddScoped<IDatabaseSettings>(sp =>
{
    return sp.GetRequiredService<IOptions<DatabaseSettings>>().Value;
}); // DatabaseSettings yap�land�rmas�n� IDatabaseSettings aray�z�yle Dependency Injection olarak sa�lar.


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
