using Microsoft.AspNetCore.Authentication.JwtBearer;
using MultiShop.Order.Application.Features.CQRS.Handlers.AddressHandlers;
using MultiShop.Order.Application.Features.CQRS.Handlers.OrderDetailHandlers;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Application.Services;
using MultiShop.Order.Persistence.Context;
using MultiShop.Order.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme) // JWT Bearer kimlik do?rulamas?n? kullan?r.
    .AddJwtBearer(options =>
    {
        options.Authority = builder.Configuration["IdentityServerUrl"]; // IdentityServer URL'sini yap?land?rma dosyas?ndan al?r.
        options.Audience = "ResourceOrder"; // Bu API'nin Audience'?n? tan?mlar.
        options.RequireHttpsMetadata = false; // HTTPS gereksinimini devre d??? b?rak?r (geli?tirme ortam?nda kullan?labilir).
    });
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Order API",
        Version = "v1"
    });

    // ?? JWT Authentication için ayar
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "JWT Authorization header. 'Bearer {token}' þeklinde giriniz."
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});


builder.Services.AddDbContext<OrderContext>();

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>)); // IRepository arayüzünü uygulayan tüm s?n?flar için DI konteynerine ekler.
builder.Services.AddApplicationServices(builder.Configuration); // Uygulama katman?ndaki servislerin DI konteynerine eklenmesi için kullan?l?r. Bu, uygulama katman?ndaki tüm servislerin Dependency Injection ile kullan?labilmesini sa?lar.

#region

builder.Services.AddScoped<GetAddressByIDQueryHandler>();
// Her HTTP iste?inde yeni bir GetAddressByIDQueryHandler nesnesi olu?turur ve Dependency Injection ile sa?lar.

builder.Services.AddScoped<GetAddressQueryHandler>();
// Her HTTP iste?inde yeni bir GetAddressQueryHandler nesnesi olu?turur ve Dependency Injection ile sa?lar.

builder.Services.AddScoped<CreateAddressCommandHandler>();
// Her HTTP iste?inde yeni bir CreateAddressCommandHandler nesnesi olu?turur ve Dependency Injection ile sa?lar.

builder.Services.AddScoped<UpdateAddressCommandHandler>();
// Her HTTP iste?inde yeni bir UpdateAddressCommandHandler nesnesi olu?turur ve Dependency Injection ile sa?lar.

builder.Services.AddScoped<RemoveAddressCommandHandler>();
// Her HTTP iste?inde yeni bir RemoveAddressCommandHandler nesnesi olu?turur ve Dependency Injection ile sa?lar.

builder.Services.AddScoped<GetOrderDetailQueryHandler>();
// Her HTTP iste?inde yeni bir GetOrderDetailQueryHandler nesnesi olu?turur ve Dependency Injection ile sa?lar.

builder.Services.AddScoped<CreateOrderDetailCommandHandler>();
// Her HTTP iste?inde yeni bir CreateOrderDetailCommandHandler nesnesi olu?turur ve Dependency Injection ile sa?lar.

builder.Services.AddScoped<UpdateOrderDetailCommandHandler>();
// Her HTTP iste?inde yeni bir UpdateOrderDetailCommandHandler nesnesi olu?turur ve Dependency Injection ile sa?lar.

builder.Services.AddScoped<RemoveOrderDetailCommandHandler>();
// Her HTTP iste?inde yeni bir RemoveOrderDetailCommandHandler nesnesi olu?turur ve Dependency Injection ile sa?lar.

builder.Services.AddScoped<GetOrderDetailByIDQueryHandler>();
// Her HTTP iste?inde yeni bir GetOrderDetailByIDQueryHandler nesnesi olu?turur ve Dependency Injection ile sa?lar.

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
app.UseAuthentication(); // Kimlik do?rulama middleware'ini ekler.

app.UseAuthorization();

app.MapControllers();

app.Run();
