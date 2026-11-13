using IncidentHub.Blazor.Client;
using IncidentHub.Blazor.Components;
using IncidentHub.Blazor.Data;

var builder = WebApplication.CreateBuilder(args);

// ---- Services ----
// Composants Razor, avec les deux modes interactifs : Server et WebAssembly.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddSingleton<InMemoryIncidentRepository>();

var app = builder.Build();

// ---- Pipeline ----
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();
app.UseAntiforgery();
app.MapStaticAssets();

// Minimal API utilisée par le composant WebAssembly, qui ne peut pas lire
// le dépôt : il tourne dans le navigateur. Cette URL est publique :
// n'importe qui peut l'appeler (navigateur, curl...), pas seulement notre composant.
// On renvoie des DTO : uniquement les champs choisis (pas d'e-mail).
app.MapGet("/api/incidents", (InMemoryIncidentRepository repository) =>
    repository.GetAll().Select(i => new IncidentDto(
        i.Id,
        i.Title,
        i.Severity.ToString(),
        i.Status.ToString(),
        i.AssignedTo?.Name,
        i.CreatedAt)));

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(IncidentHub.Blazor.Client._Imports).Assembly);

app.Run();
