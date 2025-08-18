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

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme) // JWT Bearer kimlik do�rulamas�n� kullan�r.
    .AddJwtBearer(options =>
    {
        options.Authority = builder.Configuration["IdentityServerUrl"]; // IdentityServer URL'sini yap�land�rma dosyas�ndan al�r.
        options.Audience = "ResourceCatalog"; // Bu API'nin Audience'�n� tan�mlar.
        options.RequireHttpsMetadata = false; // HTTPS gereksinimini devre d��� b�rak�r (geli�tirme ortam�nda kullan�labilir).
    });

builder.Services.AddScoped<ICategoryService, CategoryService>(); // Her istek i�in yeni bir CategoryService �rne�i olu�turur ve ICategoryService aray�z�ne ba��ml�l�klar� bu s�n�fa y�nlendirir (Dependency Injection).
builder.Services.AddScoped<IProductService, ProductService>(); // Her istek i�in yeni bir ProductService �rne�i olu�turur ve IProductService aray�z�ne ba��ml�l�klar� bu s�n�fa y�nlendirir.
builder.Services.AddScoped<IProductImageService, ProductImageService>(); // Her istek i�in yeni bir ProductImageService �rne�i olu�turur ve IProductImageService aray�z�ne ba��ml�l�klar� bu s�n�fa y�nlendirir.
builder.Services.AddScoped<IProductDetailService, ProductDetailService>(); // Her istek i�in yeni bir ProductDetailService �rne�i olu�turur ve IProductDetailService aray�z�ne ba��ml�l�klar� bu s�n�fa y�nlendirir.
builder.Services.AddScoped<IFeatureSliderService, FeatureSliderService>(); // Her istek i�in yeni bir FeatureSliderService �rne�i olu�turur ve IFeatureSliderService aray�z�ne ba��ml�l�klar� bu s�n�fa y�nlendirir.
builder.Services.AddScoped<ISpecialOfferService, SpecialOfferService>(); // Her istek i�in yeni bir SpecialOfferService �rne�i olu�turur ve ISpecialOfferService aray�z�ne ba��ml�l�klar� bu s�n�fa y�nlendirir.
builder.Services.AddScoped<IFeatureService, FeatureService>(); // Her istek i�in yeni bir FeatureService �rne�i olu�turur ve IFeatureService aray�z�ne ba��ml�l�klar� bu s�n�fa y�nlendirir.
builder.Services.AddScoped<IOfferDiscountService, OfferDiscountService>(); // Her istek i�in yeni bir OfferDiscountService �rne�i olu�turur ve IOfferDiscountService aray�z�ne ba��ml�l�klar� bu s�n�fa y�nlendirir.
builder.Services.AddScoped<IBrandService, BrandService>(); // Her istek i�in yeni bir BrandService �rne�i olu�turur ve IBrandService aray�z�ne ba��ml�l�klar� bu s�n�fa y�nlendirir.

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

app.UseAuthentication(); // Kimlik do�rulama middleware'ini kullan�r, bu middleware JWT Bearer token'lar�n� do�rular.

app.UseAuthorization();

app.MapControllers();

app.Run();
