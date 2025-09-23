using MultiShop.Order.Application.Features.CQRS.Handlers.AddressHandlers;
using MultiShop.Order.Application.Features.CQRS.Handlers.OrderDetailHandlers;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Application.Services;
using MultiShop.Order.Persistence.Context;
using MultiShop.Order.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

// ?? Swagger ayarlarý
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Order API",
        Version = "v1"
    });
});

// ?? DbContext ve servisler
builder.Services.AddDbContext<OrderContext>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddApplicationServices(builder.Configuration);

#region CQRS Handlers
builder.Services.AddScoped<GetAddressByIDQueryHandler>();
builder.Services.AddScoped<GetAddressQueryHandler>();
builder.Services.AddScoped<CreateAddressCommandHandler>();
builder.Services.AddScoped<UpdateAddressCommandHandler>();
builder.Services.AddScoped<RemoveAddressCommandHandler>();

builder.Services.AddScoped<GetOrderDetailQueryHandler>();
builder.Services.AddScoped<CreateOrderDetailCommandHandler>();
builder.Services.AddScoped<UpdateOrderDetailCommandHandler>();
builder.Services.AddScoped<RemoveOrderDetailCommandHandler>();
builder.Services.AddScoped<GetOrderDetailByIDQueryHandler>();
#endregion

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// ?? Middleware pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// ?? Authentication ve Authorization þimdilik kaldýrýldý
// app.UseAuthentication();
// app.UseAuthorization();

app.MapControllers();

app.Run();
