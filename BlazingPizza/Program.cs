using BlazingPizza.Components;
using BlazingPizza.Data;
using BlazingPizza.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddServerSideBlazor();
builder.Services.AddControllers();
builder.Services.AddScoped<OrderState>();
// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddSingleton<PizzaService>();

builder.Services.AddHttpClient();

builder.Services.AddDbContext<PizzaStoreContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Add auth services
builder.Services.AddApiAuthorization(options => {
    options.AuthenticationPaths.LogInPath = "authentication/login";
    options.AuthenticationPaths.LogInCallbackPath = "authentication/login-callback";
    options.AuthenticationPaths.LogInFailedPath = "authentication/login-failed";
    options.AuthenticationPaths.LogOutPath = "authentication/logout";
    options.AuthenticationPaths.LogOutCallbackPath = "authentication/logout-callback";
    options.AuthenticationPaths.LogOutFailedPath = "authentication/logout-failed";
    options.AuthenticationPaths.LogOutSucceededPath = "authentication/logged-out";
    options.AuthenticationPaths.ProfilePath = "authentication/profile";
    options.AuthenticationPaths.RegisterPath = "authentication/register";
});

builder.Services.AddDefaultIdentity<PizzaStoreUser>(options => options.SignIn.RequireConfirmedAccount = true)
        .AddEntityFrameworkStores<PizzaStoreContext>();

//builder.Services.AddIdentityServer();
        //.AddApiAuthorization<PizzaStoreUser, PizzaStoreContext>();

builder.Services.AddAuthentication();
//        .AddIdentityServerJwt();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();


app.UseAuthentication();
//app.UseIdentityServer();
app.UseAuthorization();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();


//app.MapRazorPages();
//app.MapBlazorHub();
//app.MapFallbackToPage("/_Host");
app.MapControllerRoute("default", "{controller=Specials}/{action=GetSpecials}/{id?}");

var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
using (var scope = scopeFactory.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<PizzaStoreContext>();
    if (db.Database.EnsureCreated())
    {
        SeedData.Initialize(db);
    }
}





















app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
