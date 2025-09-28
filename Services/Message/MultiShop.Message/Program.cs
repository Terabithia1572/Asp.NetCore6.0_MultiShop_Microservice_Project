using Microsoft.EntityFrameworkCore;
using MultiShop.Message.DAL.Context;
using MultiShop.Message.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddEntityFrameworkNpgsql()
    .AddDbContext<MessageContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))); // Baðlantý stringi appsettings.json dosyasýndan alýnýyor
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly()); // Uygulamadaki tüm AutoMapper profillerini yükler ve otomatik eþleme (mapping) iþlemlerinin kullanýlmasýný saðlar.
builder.Services.AddScoped<IUserMessageService, UserMessageService>(); // IUserMessageService arayüzünün UserMessageService sýnýfý ile eþlenmesini saðlar. Bu sayede baðýmlýlýk enjeksiyonu (dependency injection) ile IUserMessageService türünde bir nesne talep edildiðinde, UserMessageService örneði saðlanýr. Scoped yaþam süresi, her HTTP isteði için tek bir örnek oluþturulmasýný garanti eder.

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
