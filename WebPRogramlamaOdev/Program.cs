using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WebPRogramlamaOdev.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Razor;
using WebPRogramlamaOdev.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlCon"));
});

builder.Services.AddIdentity<IdentityUser,IdentityRole>()
    .AddDefaultTokenProviders()
    .AddDefaultUI()
    .AddEntityFrameworkStores<AppDbContext>();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequiredLength = 3;
    options.Password.RequireUppercase= false;
    options.Password.RequireDigit = false;
    options.Password.RequireNonAlphanumeric = false;


}


);
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.AddSingleton<LanguageService>();

builder.Services.Configure<RequestLocalizationOptions>(
                options =>
                {
                    var supportedCultures = new List<CultureInfo>
                        {
                            new CultureInfo("en-US"),
                            new CultureInfo("tr-TR"),
                        };
                    options.DefaultRequestCulture = new RequestCulture("tr-TR");
                    options.SupportedCultures = supportedCultures;
                    options.SupportedUICultures = supportedCultures;
                    options.RequestCultureProviders.Insert(0, new QueryStringRequestCultureProvider());


                }
                );

builder.Services.AddMvc()
                .AddViewLocalization()
                .AddDataAnnotationsLocalization(options =>
                {
                    options.DataAnnotationLocalizerProvider = (type, factory) =>
                    {

                        var assemblyName = new AssemblyName(typeof(ShareResource).GetTypeInfo().Assembly.FullName);

                        return factory.Create("ShareResource", assemblyName.Name);

                    };

                });



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

var locOptions = app.Services.GetService<IOptions<RequestLocalizationOptions>>();

app.UseRequestLocalization(locOptions.Value);
app.UseHttpsRedirection();
app.UseStaticFiles();

var options = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
app.UseRequestLocalization(options.Value);

app.UseRouting();
app.UseAuthentication();;

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
