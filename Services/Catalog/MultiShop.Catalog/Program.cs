using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using MultiShop.Catalog.Services.BrandServices;
using MultiShop.Catalog.Services.CategoryServices;
using MultiShop.Catalog.Services.FeatureServices;
using MultiShop.Catalog.Services.FeatureSliderServices;
using MultiShop.Catalog.Services.OfferDiscountServices;
using MultiShop.Catalog.Services.ProductDetailServices;
using MultiShop.Catalog.Services.ProductImageServices;
using MultiShop.Catalog.Services.ProductServices;
using MultiShop.Catalog.Services.SpecialOfferServices;
using MultiShop.Catalog.Settings;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme) // JWT Bearer kimlik doðrulamasýný kullanýr.
    .AddJwtBearer(options =>
    {
        options.Authority = builder.Configuration["IdentityServerUrl"]; // IdentityServer URL'sini yapýlandýrma dosyasýndan alýr.
        options.Audience = "ResourceCatalog"; // Bu API'nin Audience'ýný tanýmlar.
        options.RequireHttpsMetadata = false; // HTTPS gereksinimini devre dýþý býrakýr (geliþtirme ortamýnda kullanýlabilir).
    });

builder.Services.AddScoped<ICategoryService, CategoryService>(); // Her istek için yeni bir CategoryService örneði oluþturur ve ICategoryService arayüzüne baðýmlýlýklarý bu sýnýfa yönlendirir (Dependency Injection).
builder.Services.AddScoped<IProductService, ProductService>(); // Her istek için yeni bir ProductService örneði oluþturur ve IProductService arayüzüne baðýmlýlýklarý bu sýnýfa yönlendirir.
builder.Services.AddScoped<IProductImageService, ProductImageService>(); // Her istek için yeni bir ProductImageService örneði oluþturur ve IProductImageService arayüzüne baðýmlýlýklarý bu sýnýfa yönlendirir.
builder.Services.AddScoped<IProductDetailService, ProductDetailService>(); // Her istek için yeni bir ProductDetailService örneði oluþturur ve IProductDetailService arayüzüne baðýmlýlýklarý bu sýnýfa yönlendirir.
builder.Services.AddScoped<IFeatureSliderService, FeatureSliderService>(); // Her istek için yeni bir FeatureSliderService örneði oluþturur ve IFeatureSliderService arayüzüne baðýmlýlýklarý bu sýnýfa yönlendirir.
builder.Services.AddScoped<ISpecialOfferService, SpecialOfferService>(); // Her istek için yeni bir SpecialOfferService örneði oluþturur ve ISpecialOfferService arayüzüne baðýmlýlýklarý bu sýnýfa yönlendirir.
builder.Services.AddScoped<IFeatureService, FeatureService>(); // Her istek için yeni bir FeatureService örneði oluþturur ve IFeatureService arayüzüne baðýmlýlýklarý bu sýnýfa yönlendirir.
builder.Services.AddScoped<IOfferDiscountService, OfferDiscountService>(); // Her istek için yeni bir OfferDiscountService örneði oluþturur ve IOfferDiscountService arayüzüne baðýmlýlýklarý bu sýnýfa yönlendirir.
builder.Services.AddScoped<IBrandService, BrandService>(); // Her istek için yeni bir BrandService örneði oluþturur ve IBrandService arayüzüne baðýmlýlýklarý bu sýnýfa yönlendirir.

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

app.UseAuthentication(); // Kimlik doðrulama middleware'ini kullanýr, bu middleware JWT Bearer token'larýný doðrular.

app.UseAuthorization();

app.MapControllers();

app.Run();
