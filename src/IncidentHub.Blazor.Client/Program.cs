using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

// Ce programme s'exécute DANS LE NAVIGATEUR (WebAssembly), pas sur le serveur.
var builder = WebAssemblyHostBuilder.CreateDefault(args);

// HttpClient pointé vers le site d'origine : le seul moyen pour ce code
// d'obtenir des données est d'appeler le serveur en HTTP.
builder.Services.AddScoped(_ => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
