using _4everStore.Data;
using _4everStore.RepAdmin;
using _4everStore.RepUser;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Localization;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddAuthentication().AddGoogle(googleOpion =>
{
    googleOpion.ClientId = builder.Configuration.GetSection("googlekey:clientID").Value;
    googleOpion.ClientSecret = builder.Configuration.GetSection("googlekey:clientsec").Value;
});


// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
    .AddDataAnnotationsLocalization();
builder.Services.AddLocalization(options => options.ResourcesPath = "Resource");
builder.Services.Configure<RequestLocalizationOptions>(options =>
{

    var supportedCultures = new[] 
    { 
        new CultureInfo("ar-iq"),
        new CultureInfo("en-US")
    };
    options.DefaultRequestCulture = new RequestCulture("ar-iq");
    options.SupportedCultures = supportedCultures;

});




builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("MyConection")
    ));


builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddTransient<IRepAdmin,RepAdmin>();
builder.Services.AddTransient<IUserRep, UserRep>();

var app = builder.Build();

app.UseRequestLocalization();





// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseEndpoints(endpoints => endpoints.MapRazorPages());


using (var scop = app.Services.CreateScope())
{
    var rolemanger =
        scop.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    var roles = new[] { "Admin", "User" };
    foreach (var role in roles)
    {
        if (!await rolemanger.RoleExistsAsync(role))
        {
            await rolemanger.CreateAsync(new IdentityRole(role));
        }

    }
}

using (var scop = app.Services.CreateScope())
{
    var USerMAnger =
        scop.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
    string email = "admin@email.com";
    string password = "Admin1234@";
    if (await USerMAnger.FindByEmailAsync(email) == null)
    {
        var user = new IdentityUser();
        user.UserName = email;
        user.Email = email;
        user.EmailConfirmed = true;
        await USerMAnger.CreateAsync(user, password);
        await USerMAnger.AddToRoleAsync(user, "Admin");

    }
}


app.Run();