using MultiShop.Cargo.BusinessLayer.Abstract;
using MultiShop.Cargo.BusinessLayer.Concrete;
using MultiShop.Cargo.DataAccessLayer.Abstract;
using MultiShop.Cargo.DataAccessLayer.Concrete;
using MultiShop.Cargo.DataAccessLayer.EntityFramework;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<CargoContext>(); //Dbcontext'e Depency Injection ile ekleniyor
builder.Services.AddScoped<ICargoCompanyDal,EfCargoCompanyDal>(); // CargoCompany için EntityFramework implementasyonu ekleniyor
builder.Services.AddScoped<ICargoCompanyService, CargoCompanyManager>(); // CargoCompany için BusinessLayer implementasyonu ekleniyor
builder.Services.AddScoped<ICargoCustomerDal, EfCargoCustomerDal>(); // CargoCustomer için EntityFramework implementasyonu ekleniyor
builder.Services.AddScoped<ICargoCustomerService, CargoCustomerManager>(); // CargoCustomer için BusinessLayer implementasyonu ekleniyor
builder.Services.AddScoped<ICargoDetailDal, EfCargoDetailDal>(); // CargoDetail için EntityFramework implementasyonu ekleniyor
builder.Services.AddScoped<ICargoDetailService, CargoDetailManager>(); // CargoDetail için BusinessLayer implementasyonu ekleniyor
builder.Services.AddScoped<ICargoOperationDal, EfCargoOperationDal>(); // CargoOperation için EntityFramework implementasyonu ekleniyor
builder.Services.AddScoped<ICargoOperationService, CargoOperationManager>(); // CargoOperation için BusinessLayer implementasyonu ekleniyor

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
