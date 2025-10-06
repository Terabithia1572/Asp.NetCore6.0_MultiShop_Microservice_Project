var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews(); //  en önemli satýr bu



// Add services to the container.

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
//  Bu satýr olmazsa MVC controller'larýn View'larý bulunmaz
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Default}/{action=WeatherDetail}/{id?}");

app.MapControllers();

app.Run();
