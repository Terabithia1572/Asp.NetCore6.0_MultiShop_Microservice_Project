using Microsoft.Extensions.Options;
using MultiShop.Catalog.Services.CategoryServices;
using MultiShop.Catalog.Services.ProductImageServices;
using MultiShop.Catalog.Services.ProductServices;
using MultiShop.Catalog.Settings;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ICategoryService, CategoryService>(); // Her istek için yeni bir CategoryService örneði oluþturur ve ICategoryService arayüzüne baðýmlýlýklarý bu sýnýfa yönlendirir (Dependency Injection).
builder.Services.AddScoped<IProductService, ProductService>(); // Her istek için yeni bir ProductService örneði oluþturur ve IProductService arayüzüne baðýmlýlýklarý bu sýnýfa yönlendirir.
builder.Services.AddScoped<IProductImageService, ProductImageService>(); // Her istek için yeni bir ProductImageService örneði oluþturur ve IProductImageService arayüzüne baðýmlýlýklarý bu sýnýfa yönlendirir.

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly()); // Uygulamadaki tüm AutoMapper profillerini yükler ve otomatik eþleme (mapping) iþlemlerinin kullanýlmasýný saðlar.

builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("DatabaseSettings")); // appsettings.json'daki DatabaseSettings bölümünü, DatabaseSettings sýnýfýna baðlar ve yapýlandýrýr.

builder.Services.AddScoped<IDatabaseSettings>(sp =>
{
    return sp.GetRequiredService<IOptions<DatabaseSettings>>().Value;
}); // DatabaseSettings yapýlandýrmasýný IDatabaseSettings arayüzüyle Dependency Injection olarak saðlar.


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
