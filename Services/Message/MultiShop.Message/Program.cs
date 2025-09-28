using Microsoft.EntityFrameworkCore;
using MultiShop.Message.DAL.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddEntityFrameworkNpgsql()
    .AddDbContext<MessageContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))); // Ba�lant� stringi appsettings.json dosyas�ndan al�n�yor

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
