using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using MultiShop.WebUI.Services;
using MultiShop.WebUI.Services.Concrete;
using MultiShop.WebUI.Services.Interfaces;
using MultiShop.WebUI.Settings;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddCookie(JwtBearerDefaults.AuthenticationScheme, opt =>
    {
    opt.LoginPath= "/Login/Index/"; //Giri� sayfas� yolu
        opt.LogoutPath= "/Login/Logout/"; //��k�� sayfas� yolu
        opt.AccessDeniedPath= "/Login/AccessDenied/"; //Eri�im reddedildi sayfas� yolu
        opt.Cookie.HttpOnly= true; //Cookie'nin sadece HTTP �zerinden eri�ilebilir olmas�n� sa�lar
        opt.Cookie.SameSite= SameSiteMode.Lax; //Cross-site isteklerde cookie'nin g�nderilme davran���n� belirler
        opt.Cookie.SecurePolicy= CookieSecurePolicy.SameAsRequest; //Cookie'nin g�venli olup olmad���n� belirler
        opt.Cookie.Name= "MultiShopCookie"; //Cookie'nin ad�
    });

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, opt =>
{
    opt.LoginPath = "/Login/Index/";
    opt.ExpireTimeSpan = TimeSpan.FromDays(5);
    opt.Cookie.Name = "MultiShopCookie";
    opt.SlidingExpiration = true;

}); //

builder.Services.AddHttpContextAccessor(); //HttpContext'e eri�im i�in

builder.Services.AddScoped<IloginService, LoginService>(); //IloginService aray�z�n� LoginService ile ili�kilendiriyoruz
builder.Services.AddHttpClient<IIdentityService,IdentityService>(); // 

builder.Services.AddHttpClient();

builder.Services.AddControllersWithViews();

builder.Services.Configure<ClientSettings>(builder.Configuration.GetSection("ClientSettings")); // appsettings.json dosyas�nda "ClientSettings" adl� bir b�l�m varsa, oradaki verileri ClientSettings s�n�f�na map (ba�la) ediyor.

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication(); //Kimlik do�rulama ara katman�n� ekliyoruz

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
// Aream�z� tan�ml�yoruz
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
});

app.Run();
