using Microsoft.AspNetCore.Authentication.JwtBearer;
using MultiShop.Order.Application.Features.CQRS.Handlers.AddressHandlers;
using MultiShop.Order.Application.Features.CQRS.Handlers.OrderDetailHandlers;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Application.Services;
using MultiShop.Order.Persistence.Context;
using MultiShop.Order.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme) // JWT Bearer kimlik doðrulamasýný kullanýr.
    .AddJwtBearer(options =>
    {
        options.Authority = builder.Configuration["IdentityServerUrl"]; // IdentityServer URL'sini yapýlandýrma dosyasýndan alýr.
        options.Audience = "ResourceOrder"; // Bu API'nin Audience'ýný tanýmlar.
        options.RequireHttpsMetadata = false; // HTTPS gereksinimini devre dýþý býrakýr (geliþtirme ortamýnda kullanýlabilir).
    });


builder.Services.AddDbContext<OrderContext>();

builder.Services.AddScoped(typeof(IRepository<>),typeof(Repository<>)); // IRepository arayüzünü uygulayan tüm sýnýflar için DI konteynerine ekler.
builder.Services.AddApplicationServices(builder.Configuration); // Uygulama katmanýndaki servislerin DI konteynerine eklenmesi için kullanýlýr. Bu, uygulama katmanýndaki tüm servislerin Dependency Injection ile kullanýlabilmesini saðlar.

#region

builder.Services.AddScoped<GetAddressByIDQueryHandler>();
// Her HTTP isteðinde yeni bir GetAddressByIDQueryHandler nesnesi oluþturur ve Dependency Injection ile saðlar.

builder.Services.AddScoped<GetAddressQueryHandler>();
// Her HTTP isteðinde yeni bir GetAddressQueryHandler nesnesi oluþturur ve Dependency Injection ile saðlar.

builder.Services.AddScoped<CreateAddressCommandHandler>();
// Her HTTP isteðinde yeni bir CreateAddressCommandHandler nesnesi oluþturur ve Dependency Injection ile saðlar.

builder.Services.AddScoped<UpdateAddressCommandHandler>();
// Her HTTP isteðinde yeni bir UpdateAddressCommandHandler nesnesi oluþturur ve Dependency Injection ile saðlar.

builder.Services.AddScoped<RemoveAddressCommandHandler>();
// Her HTTP isteðinde yeni bir RemoveAddressCommandHandler nesnesi oluþturur ve Dependency Injection ile saðlar.

builder.Services.AddScoped<GetOrderDetailQueryHandler>();
// Her HTTP isteðinde yeni bir GetOrderDetailQueryHandler nesnesi oluþturur ve Dependency Injection ile saðlar.

builder.Services.AddScoped<CreateOrderDetailCommandHandler>();
// Her HTTP isteðinde yeni bir CreateOrderDetailCommandHandler nesnesi oluþturur ve Dependency Injection ile saðlar.

builder.Services.AddScoped<UpdateOrderDetailCommandHandler>();
// Her HTTP isteðinde yeni bir UpdateOrderDetailCommandHandler nesnesi oluþturur ve Dependency Injection ile saðlar.

builder.Services.AddScoped<RemoveOrderDetailCommandHandler>();
// Her HTTP isteðinde yeni bir RemoveOrderDetailCommandHandler nesnesi oluþturur ve Dependency Injection ile saðlar.

builder.Services.AddScoped<GetOrderDetailByIDQueryHandler>();
// Her HTTP isteðinde yeni bir GetOrderDetailByIDQueryHandler nesnesi oluþturur ve Dependency Injection ile saðlar.

#endregion

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
app.UseAuthentication(); // Kimlik doðrulama middleware'ini ekler.

app.UseAuthorization();

app.MapControllers();

app.Run();
