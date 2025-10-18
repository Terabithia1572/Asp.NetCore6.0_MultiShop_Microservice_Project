
using MultiShop.SignalRRealTimeAPI.HUBs;
using MultiShop.SignalRRealTimeAPI.Services.SignalRCommentServices;
using MultiShop.SignalRRealTimeAPI.Services.SignalRMessageServices;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("MultiShopCors", policy =>
    {
        policy.AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials()
              .SetIsOriginAllowed(_ => true); // UI'dan gelen bağlantıları kabul eder
    });
});

builder.Services.AddHttpClient();

builder.Services.AddScoped<ISignalRMessageService,SignalRMessageService>();
builder.Services.AddScoped<ISignalRCommentService,SignalRCommentService>();

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
app.UseCors("MultiShopCors");  // 🔹 2. CORS mutlaka routing’ten sonra gelmeli
app.UseHttpsRedirection();     // 🔹 3. HTTPS yönlendirme
app.UseAuthorization();        // 🔹 4. Authorization middleware
app.MapControllers();          // 🔹 5. API Controller route’ları
app.MapHub<LiveChatHub>("/hubs/livechat");
// 🔹 6. SignalR endpoint

app.Run();
