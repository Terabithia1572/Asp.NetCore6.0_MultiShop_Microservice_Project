using Microsoft.AspNetCore.Authentication.JwtBearer;
using MultiShop.Cargo.BusinessLayer.Abstract;
using MultiShop.Cargo.BusinessLayer.Concrete;
using MultiShop.Cargo.DataAccessLayer.Abstract;
using MultiShop.Cargo.DataAccessLayer.Concrete;
using MultiShop.Cargo.DataAccessLayer.EntityFramework;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme) // JWT Bearer kimlik do�rulamas�n� kullan�r.
    .AddJwtBearer(options =>
    {
        options.Authority = builder.Configuration["IdentityServerUrl"]; // IdentityServer URL'sini yap�land�rma dosyas�ndan al�r.
        options.Audience = "ResourceCargo"; // Bu API'nin Audience'�n� tan�mlar.
        options.RequireHttpsMetadata = false; // HTTPS gereksinimini devre d��� b�rak�r (geli�tirme ortam�nda kullan�labilir).
    });

builder.Services.AddDbContext<CargoContext>(); //Dbcontext'e Depency Injection ile ekleniyor
builder.Services.AddScoped<ICargoCompanyDal,EfCargoCompanyDal>(); // CargoCompany i�in EntityFramework implementasyonu ekleniyor
builder.Services.AddScoped<ICargoCompanyService, CargoCompanyManager>(); // CargoCompany i�in BusinessLayer implementasyonu ekleniyor
builder.Services.AddScoped<ICargoCustomerDal, EfCargoCustomerDal>(); // CargoCustomer i�in EntityFramework implementasyonu ekleniyor
builder.Services.AddScoped<ICargoCustomerService, CargoCustomerManager>(); // CargoCustomer i�in BusinessLayer implementasyonu ekleniyor
builder.Services.AddScoped<ICargoDetailDal, EfCargoDetailDal>(); // CargoDetail i�in EntityFramework implementasyonu ekleniyor
builder.Services.AddScoped<ICargoDetailService, CargoDetailManager>(); // CargoDetail i�in BusinessLayer implementasyonu ekleniyor
builder.Services.AddScoped<ICargoOperationDal, EfCargoOperationDal>(); // CargoOperation i�in EntityFramework implementasyonu ekleniyor
builder.Services.AddScoped<ICargoOperationService, CargoOperationManager>(); // CargoOperation i�in BusinessLayer implementasyonu ekleniyor

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
app.UseAuthentication(); // Kimlik do�rulama middleware'ini kullan�r.

app.UseAuthorization();

app.MapControllers();

app.Run();
