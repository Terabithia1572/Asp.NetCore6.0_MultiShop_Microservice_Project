using MultiShop.Discount.Context;
using MultiShop.Discount.Services.DiscountService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<DapperContext>();
// Her talepte (her Dependency Injection �a�r�s�nda) yeni bir DapperContext nesnesi olu�turur ve ba��ml�l�k olarak sa�lar.

builder.Services.AddTransient<IDiscountService, DiscountService>();
// Her talepte (her Dependency Injection �a�r�s�nda) yeni bir DiscountService nesnesi olu�turur ve bu nesneyi IDiscountService aray�z�ne ba��ml� olan yerlere sa�lar.


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
