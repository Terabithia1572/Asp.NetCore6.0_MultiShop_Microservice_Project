using Microsoft.AspNetCore.Authentication.JwtBearer;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = builder.Configuration["IdentityServerUrl"]; // IdentityServer URL'si
        options.Audience = "ResourceOcelot"; // API Audience
        options.RequireHttpsMetadata = false; // HTTPS gereksinimi
    });


IConfiguration configuration=new ConfigurationBuilder().AddJsonFile("ocelot.json").Build(); // Bu kod 
builder.Services.AddOcelot(configuration);
var app = builder.Build();
await app.UseOcelot();

app.MapGet("/", () => "Hello World!");

app.Run();
