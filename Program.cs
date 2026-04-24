using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PortalAcademico.Data;

var builder = WebApplication.CreateBuilder(args);

// 🔥 BASE DE DATOS (SQLite)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// 🔐 IDENTITY
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
    options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDbContext>();

// 🧠 SESSION
builder.Services.AddSession();

// ⚡ REDIS (CACHE + SESSION BACKEND)
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration["Redis:ConnectionString"];
});

// MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

// 🔧 MIDDLEWARE
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// 🔐 AUTH
app.UseAuthentication();
app.UseAuthorization();

// 🧠 SESSION (después de routing)
app.UseSession();

// 🌐 RUTAS
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// 👤 Identity
app.MapRazorPages();

app.Run();