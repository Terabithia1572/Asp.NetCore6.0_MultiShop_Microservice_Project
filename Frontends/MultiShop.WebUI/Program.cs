using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using MultiShop.WebUI.Handlers;
using MultiShop.WebUI.Services.BasketServices;
using MultiShop.WebUI.Services.CargoServices.CargoCompanyServices;
using MultiShop.WebUI.Services.CargoServices.CargoCustomerServices;
using MultiShop.WebUI.Services.CatalogServices.AboutServices;
using MultiShop.WebUI.Services.CatalogServices.BrandServices;
using MultiShop.WebUI.Services.CatalogServices.CategoryServices;
using MultiShop.WebUI.Services.CatalogServices.ContactServices;
using MultiShop.WebUI.Services.CatalogServices.FeatureServices;
using MultiShop.WebUI.Services.CatalogServices.FeatureSliderServices;
using MultiShop.WebUI.Services.CatalogServices.OfferDiscountServices;
using MultiShop.WebUI.Services.CatalogServices.ProductDetailServices;
using MultiShop.WebUI.Services.CatalogServices.ProductImageServices;
using MultiShop.WebUI.Services.CatalogServices.ProductServices;
using MultiShop.WebUI.Services.CatalogServices.SpecialOfferServices;
using MultiShop.WebUI.Services.CommentServices;
using MultiShop.WebUI.Services.Concrete;
using MultiShop.WebUI.Services.DiscountServices;
using MultiShop.WebUI.Services.Interfaces;
using MultiShop.WebUI.Services.MessageServices;
using MultiShop.WebUI.Services.OrderServices.OrderAddressServices;
using MultiShop.WebUI.Services.OrderServices.OrderOrderingServices;
using MultiShop.WebUI.Services.StatisticServices.CatalogStatisticServices;
using MultiShop.WebUI.Services.StatisticServices.DiscountStatisticServices;
using MultiShop.WebUI.Services.StatisticServices.MessageStatisticServices;
using MultiShop.WebUI.Services.StatisticServices.UserStatisticServices;
using MultiShop.WebUI.Services.UserIdentityServices;
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
builder.Services.AddScoped<ResourceOwnerPasswordTokenHandler>();

var values=builder.Configuration.GetSection("ServiceApiSettings").Get<ServiceApiSettings>(); // Doðrudan alým örneði (opsiyonel) 
builder.Services.AddHttpClient<IUserService, UserService>(opt =>
{
   opt.BaseAddress=new Uri(values.IdentityServerUrl); // http://localhost:5001
   opt.DefaultRequestVersion=new Version(1,1); // opsiyonel: HTTP/1.1 zorla
}).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>(); // HttpClient'a delegating handler ekle
builder.Services.AddHttpClient<ICatalogStatisticService, CatalogStatisticService>(opt =>
{
    // Ocelot üzerinden Catalog servisini iþaret et
    opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Catalog.Path}");
    // Örn: http://localhost:5000/services/catalog/
    opt.DefaultRequestVersion = new Version(1, 1);
}).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();
builder.Services.AddHttpClient<IDiscountStatisticService, DiscountStatisticService>(opt =>
{
    // Ocelot üzerinden Catalog servisini iþaret et
    opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Discount.Path}");
    // Örn: http://localhost:5000/services/catalog/
    opt.DefaultRequestVersion = new Version(1, 1);
}).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();
builder.Services.AddHttpClient<IMessageStatisticService, MessageStatisticService>(opt =>
{
    // Ocelot üzerinden Catalog servisini iþaret et
    opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Message.Path}");
    // Örn: http://localhost:5000/services/catalog/
    opt.DefaultRequestVersion = new Version(1, 1);
}).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();
builder.Services.AddHttpClient<IUserStatisticService, UserStatisticService>(opt =>
{
    // Ocelot üzerinden Catalog servisini iþaret et
    opt.BaseAddress = new Uri(values.IdentityServerUrl);
    // Örn: http://localhost:5000/services/catalog/
    opt.DefaultRequestVersion = new Version(1, 1);
}).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();
builder.Services.AddHttpClient<IUserIdentityService, UserIdentityService>(opt =>
{
    opt.BaseAddress = new Uri(values.IdentityServerUrl); // http://localhost:5001
    opt.DefaultRequestVersion = new Version(1, 1); // opsiyonel: HTTP/1.1 zorla
}).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>(); // HttpClient'a delegating handler ekle
builder.Services.AddHttpClient<IBasketService, BasketService>(opt =>
{
    opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Basket.Path}"); // http://localhost:5001
    opt.DefaultRequestVersion = new Version(1, 1); // opsiyonel: HTTP/1.1 zorla
}).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>(); // HttpClient'a delegating handler ekle
builder.Services.AddHttpClient<ICargoCompanyService, CargoCompanyService>(opt =>
{
    opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Cargo.Path}"); // http://localhost:5001
    opt.DefaultRequestVersion = new Version(1, 1); // opsiyonel: HTTP/1.1 zorla
}).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>(); // HttpClient'a delegating handler ekle
builder.Services.AddHttpClient<ICargoCustomerService, CargoCustomerService>(opt =>
{
    opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Cargo.Path}"); // http://localhost:5001
    opt.DefaultRequestVersion = new Version(1, 1); // opsiyonel: HTTP/1.1 zorla
}).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>(); // HttpClient'a delegating handler ekle
builder.Services.AddHttpClient<IOrderOrderingService, OrderOrderingService>(opt =>
{
    opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Order.Path}"); // http://localhost:5001
    opt.DefaultRequestVersion = new Version(1, 1); // opsiyonel: HTTP/1.1 zorla
}).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>(); // HttpClient'a delegating handler ekle
builder.Services.AddHttpClient<IMessageService, MessageService>(opt =>
{
    opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Message.Path}"); // http://localhost:5001
    opt.DefaultRequestVersion = new Version(1, 1); // opsiyonel: HTTP/1.1 zorla
}).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>(); // HttpClient'a delegating handler ekle
builder.Services.AddHttpClient<IOrderAddressService, OrderAddressService>(opt =>
{
    opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Order.Path}"); // http://localhost:5001
    opt.DefaultRequestVersion = new Version(1, 1); // opsiyonel: HTTP/1.1 zorla
}).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>(); // HttpClient'a delegating handler ekle
builder.Services.AddHttpClient<IDiscountService, DiscountService>(opt =>
{
    opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Discount.Path}"); // http://localhost:5001
    opt.DefaultRequestVersion = new Version(1, 1); // opsiyonel: HTTP/1.1 zorla
}).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>(); // HttpClient'a delegating handler ekle
builder.Services.AddHttpClient<ICategoryService, CategoryService>(opt =>
{
    opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Catalog.Path}"); // http://localhost:1002
}).AddHttpMessageHandler<ClientCredentialTokenHandler>(); // HttpClient'a delegating handler ekle
builder.Services.AddHttpClient<IProductService, ProductService>(opt =>
{
    opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Catalog.Path}"); // http://localhost:1002
}).AddHttpMessageHandler<ClientCredentialTokenHandler>(); // HttpClient'a delegating handler ekle
builder.Services.AddHttpClient<ISpecialOfferService, SpecialOfferService>(opt =>
{
    opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Catalog.Path}"); // http://localhost:1002
}).AddHttpMessageHandler<ClientCredentialTokenHandler>(); // HttpClient'a delegating handler ekle
builder.Services.AddHttpClient<IFeatureSliderService, FeatureSliderService>(opt =>
{
    opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Catalog.Path}"); // http://localhost:1002
}).AddHttpMessageHandler<ClientCredentialTokenHandler>(); // HttpClient'a delegating handler ekle
builder.Services.AddHttpClient<IFeatureService, FeatureService>(opt =>
{
    opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Catalog.Path}"); // http://localhost:1002
}).AddHttpMessageHandler<ClientCredentialTokenHandler>(); // HttpClient'a delegating handler ekle
builder.Services.AddHttpClient<IOfferDiscountService, OfferDiscountService>(opt =>
{
    opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Catalog.Path}"); // http://localhost:1002
}).AddHttpMessageHandler<ClientCredentialTokenHandler>(); // HttpClient'a delegating handler ekle
builder.Services.AddHttpClient<IBrandService, BrandService>(opt =>
{
    opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Catalog.Path}"); // http://localhost:1002
}).AddHttpMessageHandler<ClientCredentialTokenHandler>(); // HttpClient'a delegating handler ekle
builder.Services.AddHttpClient<IAboutService, AboutService>(opt =>
{
    opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Catalog.Path}"); // http://localhost:1002
}).AddHttpMessageHandler<ClientCredentialTokenHandler>(); // HttpClient'a delegating handler ekle
builder.Services.AddHttpClient<IProductImageService, ProductImageService>(opt =>
{
    opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Catalog.Path}"); // http://localhost:1002
}).AddHttpMessageHandler<ClientCredentialTokenHandler>(); // HttpClient'a delegating handler ekle
builder.Services.AddHttpClient<IProductDetailService, ProductDetailService>(opt =>
{
    opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Catalog.Path}"); // http://localhost:1002
}).AddHttpMessageHandler<ClientCredentialTokenHandler>(); // HttpClient'a delegating handler ekle
builder.Services.AddHttpClient<ICommentService, CommentService>(opt =>
{
    opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Comment.Path}"); // http://localhost:1007
}).AddHttpMessageHandler<ClientCredentialTokenHandler>(); // HttpClient'a delegating handler ekle
builder.Services.AddHttpClient<IContactService, ContactService>(opt =>
{
    opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Catalog.Path}"); // http://localhost:1002
}).AddHttpMessageHandler<ClientCredentialTokenHandler>(); // HttpClient'a delegating handler ekle





builder.Services.AddScoped<ClientCredentialTokenHandler>();
builder.Services.AddHttpClient<IClientCredentialTokenService, ClientCredentialTokenService>(); // ClientCredentialTokenService için typed HttpClient
builder.Services.AddAccessTokenManagement(); // Token yönetimi için
//NUL UPDATE

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
