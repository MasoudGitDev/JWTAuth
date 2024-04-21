using Blazored.LocalStorage;
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using ClientApp;
using ClientApp.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");



builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7224/") });

builder.Services
    .AddBlazorise(options => {
        options.Immediate = true;
    })
    .AddBootstrapProviders()
    .AddFontAwesomeIcons();


builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider , AuthStateProvider>();
builder.Services.AddScoped<IAccountService , AccountManagerService>();
builder.Services.AddScoped<IEmailService , EmailManagerService>();
builder.Services.AddScoped<IPasswordManagerService , PasswordManagerService>();
builder.Services.AddScoped<ICaptchaManagerService , CaptchaManagerService>();


await builder.Build().RunAsync();
