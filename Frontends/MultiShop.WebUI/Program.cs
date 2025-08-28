using Microsoft.AspNetCore.Authentication.JwtBearer;
using MultiShop.WebUI.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddCookie(JwtBearerDefaults.AuthenticationScheme, opt =>
    {
    opt.LoginPath= "/Login/Index/"; //Giriþ sayfasý yolu
        opt.LogoutPath= "/Login/Logout/"; //Çýkýþ sayfasý yolu
        opt.AccessDeniedPath= "/Login/AccessDenied/"; //Eriþim reddedildi sayfasý yolu
        opt.Cookie.HttpOnly= true; //Cookie'nin sadece HTTP üzerinden eriþilebilir olmasýný saðlar
        opt.Cookie.SameSite= SameSiteMode.Lax; //Cross-site isteklerde cookie'nin gönderilme davranýþýný belirler
        opt.Cookie.SecurePolicy= CookieSecurePolicy.SameAsRequest; //Cookie'nin güvenli olup olmadýðýný belirler
        opt.Cookie.Name= "MultiShopCookie"; //Cookie'nin adý
    });

builder.Services.AddHttpContextAccessor(); //HttpContext'e eriþim için

builder.Services.AddScoped<IloginService, LoginService>(); //IloginService arayüzünü LoginService ile iliþkilendiriyoruz

builder.Services.AddHttpClient();

builder.Services.AddControllersWithViews();

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
app.UseAuthentication(); //Kimlik doðrulama ara katmanýný ekliyoruz

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
// Areamýzý tanýmlýyoruz
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
});

app.Run();
