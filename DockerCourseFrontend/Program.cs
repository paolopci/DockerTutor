using DockerCourseFrontend;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// CORREZIONE: URL dell'API configurato correttamente
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5163/") });

await builder.Build().RunAsync();