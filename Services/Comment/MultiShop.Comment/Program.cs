using Microsoft.AspNetCore.Authentication.JwtBearer;
using MultiShop.Comment.Context;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme) // JWT Bearer kimlik do�rulamas�n� kullan�r.
    .AddJwtBearer(options =>
    {
        options.Authority = builder.Configuration["IdentityServerUrl"]; // IdentityServer URL'sini yap�land�rma dosyas�ndan al�r.
        options.Audience = "ResourceComment"; // Bu API'nin Audience'�n� tan�mlar.
        options.RequireHttpsMetadata = false; // HTTPS gereksinimini devre d��� b�rak�r (geli�tirme ortam�nda kullan�labilir).
    });


// Add services to the container.
builder.Services.AddDbContext<CommentContext>();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
