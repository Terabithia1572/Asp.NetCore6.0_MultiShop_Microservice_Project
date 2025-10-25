using Microsoft.EntityFrameworkCore;
using MultiShop.Favorite.DataAccess;
using MultiShop.Favorite.Services;

var builder = WebApplication.CreateBuilder(args);

// === DbContext ===
builder.Services.AddDbContext<FavoriteContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
});

// === DI Servisleri ===
builder.Services.AddScoped<IFavoriteService, FavoriteService>();

// === Controller ve Swagger ===
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// === Pipeline ===
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
