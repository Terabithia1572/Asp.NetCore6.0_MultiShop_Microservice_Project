using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using MultiShop.WebUI.Services.Concrete;
using MultiShop.WebUI.Services.Interfaces;
using MultiShop.WebUI.Settings;

var builder = WebApplication.CreateBuilder(args);

// 1) Options binding (ClientSettings)
builder.Services.Configure<ClientSettings>(builder.Configuration.GetSection("ClientSettings"));
builder.Services.Configure<ServiceApiSettings>(builder.Configuration.GetSection("ServiceApiSettings"));

// 2) Authentication – Default: Cookie (UI senaryosu)
builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, opt =>
    {
        opt.LoginPath = "/Login/Index/";
        opt.LogoutPath = "/Login/Logout/";
        opt.AccessDeniedPath = "/Login/AccessDenied/";
        opt.Cookie.HttpOnly = true;
        opt.Cookie.SameSite = SameSiteMode.Lax;
        opt.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
        opt.Cookie.Name = "MultiShopCookie";
        opt.ExpireTimeSpan = TimeSpan.FromDays(5);
        opt.SlidingExpiration = true;
    })
    // (Ýsteðe baðlý) API'ler Bearer gerektiriyorsa ek þema olarak ekle (default þema Cookie kalýr)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, o =>
    {
        // Geliþtirmede self-signed kullanýyorsan:
        o.RequireHttpsMetadata = false;
        // o.Authority = "...";
        // o.Audience  = "...";
    });

builder.Services.AddAuthorization();

// 3) HttpContext + HttpClient + Servisler
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient("Gateway", (sp, c) =>
{
    var svc = sp.GetRequiredService<IOptions<ServiceApiSettings>>().Value;
    c.BaseAddress = new Uri(svc.OcelotUrl); // http://localhost:5000
    c.DefaultRequestVersion = new Version(1, 1); // opsiyonel: HTTP/1.1 zorla
});
builder.Services.AddHttpClient<IIdentityService, IdentityService>(); // IdentityService için typed HttpClient
builder.Services.AddScoped<IloginService, LoginService>(); // (Arayüz ismi sende "IloginService" ise aynen kalsýn)

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// 4) Routing – Area + Default route (UseEndpoints yok, .NET 6+ pattern)
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
