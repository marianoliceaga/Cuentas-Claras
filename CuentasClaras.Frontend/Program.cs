using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using CuentasClaras.Frontend;
using CuentasClaras.Frontend.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Configure HttpClient to use the auth infrastructure
builder.Services.AddHttpClient("ServerAPI", client => client.BaseAddress = new Uri("https://localhost:7010"))
    .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

// Supply HttpClient instances that include access tokens when making requests to the server project
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("ServerAPI"));

// Add Microsoft Authentication (MSAL) for Azure Entra (Azure AD)
builder.Services.AddMsalAuthentication(options =>
{
    // Configuration is read from wwwroot/appsettings.json AzureAd section
    builder.Configuration.Bind("AzureAd", options.ProviderOptions.Authentication);

    // Use redirect (default) or popup based on needs
    // options.ProviderOptions.LoginMode = "redirect";

    // Add any API scopes you need to request a token for (replace with your API scope)
    // Example: options.ProviderOptions.DefaultAccessTokenScopes.Add("api://{api-client-id}/access_as_user");
});

// Add services
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<ILookupService, LookupService>();
builder.Services.AddScoped<ILocalityService, LocalityService>();
builder.Services.AddScoped<ICollectorService, CollectorService>();
builder.Services.AddScoped<ISalesmanService, SalesmanService>();
builder.Services.AddScoped<IPlanService, PlanService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductPlanService, ProductPlanService>();

await builder.Build().RunAsync();
