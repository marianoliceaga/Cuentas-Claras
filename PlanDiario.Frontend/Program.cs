using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PlanDiario.Frontend;
using PlanDiario.Frontend.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7010") }); // API URL

// Add services
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ILookupService, LookupService>();
builder.Services.AddScoped<ILocalityService, LocalityService>();
builder.Services.AddScoped<ICollectorService, CollectorService>();
builder.Services.AddScoped<ISalesmanService, SalesmanService>();

await builder.Build().RunAsync();
