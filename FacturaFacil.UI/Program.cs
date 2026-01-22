using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using FacturaFacil.UI;
using MudBlazor.Services;
using Blazored.LocalStorage;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddMudServices();
builder.Services.AddBlazoredLocalStorage();

// Configuración flexible del HttpClient
if (builder.HostEnvironment.IsDevelopment())
{
    // En desarrollo local (dotnet run), la API corre en otro puerto (7100)
    builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:7100/") });
}
else
{
    // En producción (Docker/Nginx), la API está en el mismo dominio bajo /api/
    builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
}

await builder.Build().RunAsync();
