using Microsoft.EntityFrameworkCore;
using MultiShop.Message.DAL.Context;
using MultiShop.Message.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddEntityFrameworkNpgsql()
    .AddDbContext<MessageContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))); // Ba�lant� stringi appsettings.json dosyas�ndan al�n�yor
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly()); // Uygulamadaki t�m AutoMapper profillerini y�kler ve otomatik e�leme (mapping) i�lemlerinin kullan�lmas�n� sa�lar.
builder.Services.AddScoped<IUserMessageService, UserMessageService>(); // IUserMessageService aray�z�n�n UserMessageService s�n�f� ile e�lenmesini sa�lar. Bu sayede ba��ml�l�k enjeksiyonu (dependency injection) ile IUserMessageService t�r�nde bir nesne talep edildi�inde, UserMessageService �rne�i sa�lan�r. Scoped ya�am s�resi, her HTTP iste�i i�in tek bir �rnek olu�turulmas�n� garanti eder.

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
