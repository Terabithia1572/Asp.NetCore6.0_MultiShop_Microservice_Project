using MultiShop.SignalRRealTimeAPI.HUBs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("CorsPolicy", builder =>
    {
        builder
            .AllowAnyHeader()
            .AllowAnyMethod()
            .SetIsOriginAllowed(_ => true)
            .AllowCredentials();
    });
});

builder.Services.AddSignalR();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 🔥 En kritik kısım burası:
app.UseRouting();              // 🔹 1. Routing aktif edilmeli
app.UseCors("CorsPolicy");     // 🔹 2. CORS mutlaka routing’ten sonra gelmeli
app.UseHttpsRedirection();     // 🔹 3. HTTPS yönlendirme
app.UseAuthorization();        // 🔹 4. Authorization middleware
app.MapControllers();          // 🔹 5. API Controller route’ları
app.MapHub<SignalRHub>("/signalrhub"); // 🔹 6. SignalR endpoint

app.Run();
