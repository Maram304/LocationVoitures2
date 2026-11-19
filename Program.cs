using LocationVoitures.Web.Data;
using LocationVoitures.Web.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// =========================================================================
// 1. CONFIGURATION DU DBCONTEXT AVEC SQL SERVER LOCALDB
// =========================================================================
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Ajout du support pour Contrôleurs et Vues MVC
builder.Services.AddControllersWithViews();
// 2. ENREGISTREMENT DU REPOSITORY DANS LE CONTENEUR IOC (Cycle Scoped)
builder.Services.AddScoped<ICategoryVoitureRepository, CategoryVoitureRepository>();

// 3. ENREGISTREMENT DU REPOSITORY DES VOITURES (Cycle Scoped)
builder.Services.AddScoped<IVoitureRepository, VoitureRepository>();

var app = builder.Build();

// Configuration du pipeline HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // Indispensable pour servir les images depuis wwwroot

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();