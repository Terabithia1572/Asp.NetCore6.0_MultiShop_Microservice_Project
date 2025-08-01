using Microsoft.AspNetCore.Authentication.JwtBearer;
using MultiShop.Discount.Context;
using MultiShop.Discount.Services.DiscountService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme) // JWT Bearer kimlik doðrulamasýný kullanýr.
    .AddJwtBearer(options =>
    {
        options.Authority = builder.Configuration["IdentityServerUrl"]; // IdentityServer URL'sini yapýlandýrma dosyasýndan alýr.
        options.Audience = "ResourceDiscount"; // Bu API'nin Audience'ýný tanýmlar.
        options.RequireHttpsMetadata = false; // HTTPS gereksinimini devre dýþý býrakýr (geliþtirme ortamýnda kullanýlabilir).
    });


// Add services to the container.
builder.Services.AddTransient<DapperContext>();
// Her talepte (her Dependency Injection çaðrýsýnda) yeni bir DapperContext nesnesi oluþturur ve baðýmlýlýk olarak saðlar.

builder.Services.AddTransient<IDiscountService, DiscountService>();
// Her talepte (her Dependency Injection çaðrýsýnda) yeni bir DiscountService nesnesi oluþturur ve bu nesneyi IDiscountService arayüzüne baðýmlý olan yerlere saðlar.


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
