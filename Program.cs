using BethanysPieShop.Data;
using BethanysPieShop.MockRepos;
using BethanysPieShop.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews().AddJsonOptions(options=> options.JsonSerializerOptions.ReferenceHandler=ReferenceHandler.IgnoreCycles);
builder.Services.AddSession();

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IShoppingCart, ShoppingCartRepository>(sp => ShoppingCartRepository.GetCart(sp));

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IPieRepository, PieRepository>();
builder.Services.AddScoped<IOrderRepository,OrderRepository>();

builder.Services.AddDbContext<ApplicationDbContext>(options=>options.UseSqlServer(
                                builder.Configuration.GetConnectionString("DefaultConn")));

builder.Services.AddDefaultIdentity<IdentityUser>().AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddRazorPages();



builder.Services.AddServerSideBlazor();

//above is the extnsion method for that i have not need to bring any nuget package
var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}




app.MapBlazorHub();

app.UseSession();
app.UseStaticFiles();
app.MapRazorPages();
/*app.MapDefaultControllerRoute();
*/
app.MapControllerRoute(
    name:"default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

DbInitializer.SeedDatabase(app);

//configure below that anything that arrives at app/whatever will be handled by App/Index
app.MapFallbackToPage("/app/{*catchall}","/App/Index");

app.Run();
//blazor uses components to add functionality
//componenet is basically the building block
//when we want a standalone blazor application then we have a main component and we will have another component nested in that and making it multi level nesting
// component can be a screen filling or it can be a page
//button can also be a page
//