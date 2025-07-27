using MultiShop.Discount.Context;
using MultiShop.Discount.Services.DiscountService;

var builder = WebApplication.CreateBuilder(args);

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

app.UseAuthorization();

app.MapControllers();

app.Run();
