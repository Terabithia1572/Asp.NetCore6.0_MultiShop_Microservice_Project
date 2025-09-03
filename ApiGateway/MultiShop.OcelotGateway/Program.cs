using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);


IConfiguration configuration=new ConfigurationBuilder().AddJsonFile("ocelot.json").Build(); // Bu kod 
builder.Services.AddOcelot(configuration);
var app = builder.Build();
await app.UseOcelot();

app.MapGet("/", () => "Hello World!");

app.Run();
