// Point d'entrée de l'application web IncidentHub.
// Deux temps : 1) on déclare les SERVICES, 2) on construit le PIPELINE
// que chaque requête HTTP traverse, dans l'ordre où il est écrit.

var builder = WebApplication.CreateBuilder(args);

// ---- 1. Services (injection de dépendances) ----
builder.Services.AddRazorPages();   // active les pages .cshtml du dossier Pages/

var app = builder.Build();

// ---- 2. Pipeline : les middlewares, dans l'ordre ----

// Gestion d'erreurs : en production, une page d'erreur neutre (pas de pile d'appels
// affichée à l'utilisateur) et HSTS (le navigateur retient « toujours en HTTPS »).
// En développement, ASP.NET Core affiche une page d'erreur détaillée.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();  // redirige http:// vers https:// (si un port HTTPS est configuré)
app.UseStaticFiles();       // sert wwwroot/ (CSS, JS, images) et court-circuite la suite
                            // (le gabarit .NET 10 utilise MapStaticAssets(), version optimisée)
app.UseRouting();           // choisit la page qui correspond à l'URL
app.UseAuthorization();     // vérifie les droits AVANT d'exécuter la page

app.MapRazorPages();        // branche les pages : /Incidents -> Pages/Incidents/Index.cshtml

app.Run();
