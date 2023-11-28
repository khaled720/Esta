using ESTA.Models;
using ESTA.Repository;
using ESTA.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

using ESTA.Mappers;
using ESTA.Helpers;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using ESTA.Resources;
using System.Reflection;
using ESTA;
using System.Security.Policy;
using Microsoft.AspNetCore.Mvc.Routing;
using ESTA.Controllers;
using EntityFrameworkCore.UseRowNumberForPaging;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

//builder.WebHost.UseIISIntegration();
// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSession( /*opt=>opt.IdleTimeout=TimeSpan.FromMinutes(1)*/);

builder.Services.AddDbContext<AppDbContext>(
    opt =>
    opt.UseSqlServer(
        builder.Configuration.GetConnectionString("dev_conn"), o => o.UseRowNumberForPaging())
    );


////
///



var localizationRequest = new RequestLocalizationOptions();

//var cultures= new [] {"en","ar" };
//// Forcing Gregorian date format in arabic
var caltures = new List<CultureInfo>() {new CultureInfo("en") ,new CultureInfo("ar")
{ DateTimeFormat = { Calendar = new GregorianCalendar() } } };

localizationRequest.SupportedCultures = caltures;
localizationRequest.SetDefaultCulture(caltures[0].Name);
localizationRequest.SupportedUICultures = caltures;
localizationRequest.ApplyCurrentCultureToResponseHeaders = true;


/////
///

//configure localization
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

//builder.Services.Configure<RequestLocalizationOptions>(options =>
//{
//    //configure supported languages.
//    var supportedCultures = new[] { new CultureInfo("en"), new CultureInfo("ar") };

//    options.DefaultRequestCulture = new RequestCulture("en");
//    options.SupportedCultures = supportedCultures;
//    options.SupportedUICultures = supportedCultures;
//    options.ApplyCurrentCultureToResponseHeaders = true;
//});


//var config = new MapperConfiguration(cfg =>
//{
//    cfg.AddProfile(new ForumMapper());
//    cfg.AddProfile(new EventsMapper());
//    cfg.AddProfile(new UserMapper());
//});
//var mapper = config.CreateMapper();
builder.Services.AddHttpContextAccessor();
//builder.Services.AddSingleton(mapper);


builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services
    .AddMvc()
    .AddViewLocalization(Microsoft.AspNetCore.Mvc.Razor.LanguageViewLocationExpanderFormat.Suffix)
    .AddDataAnnotationsLocalization(
    //opt =>
    //    opt.DataAnnotationLocalizerProvider = (type, factory) => 
    //    {

    //        var type1 = typeof(SharedResource);
    //        var assemblyName = new AssemblyName(type1.Assembly.GetType().Assembly.FullName!);

    //        return factory.Create("SharedResource", assemblyName.Name!);
    //    }
    );

builder.Services.AddAuthorization(
    opt =>
    {
        opt.AddPolicy("RequireAdminRole", p => p.RequireRole("Admin"));
        opt.AddPolicy("AdminOrModerator", p => p.RequireRole("Admin", "Moderator"));
    }
);
builder.Services
    .AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedEmail = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
   
   
    options.Events = new CookieAuthenticationEvents
    {
        OnRedirectToLogin = x =>
        {
            //  x.Response.Redirect(x.Request.Scheme+"://"+x.Request.Host+"/Account/Login");

            x.Response.Redirect("/Account/Login");
            
            return Task.CompletedTask;
        }
    };

});

var app = builder.Build();

//tell app to use the resources.
app.UseRequestLocalization(
   // app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value
   localizationRequest
);
ImageHelper.Configure(app.Environment);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    //    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
//app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
/*
var supportedCultures = new[] { "en", "ar" };
var localizationOptions = new RequestLocalizationOptions()
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures)
    .SetDefaultCulture("en");
*/
//app.UseRequestLocalization(localizationOptions);

app.UseRouting();
app.UseAuthentication();


app.UseAuthorization();



//app.UseEndpoints(endpoints =>
//{   
//    endpoints.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=home}/{action=index}/{id?}"
//  );
//    endpoints.MapAreaControllerRoute(
//      name: "AdminArea",
//      areaName:"Admin",
//      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
//    ).RequireAuthorization("AdminOrModerator");

//    endpoints.MapAreaControllerRoute(
//      name: "PaymentArea",
//      areaName:"Payment",
//      pattern: "Payment/{controller=Home}/{action=Index}/{id?}"
//    ).RequireAuthorization();




//});

app.MapControllerRoute(
    name: "MyArea",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");



app.Lifetime.ApplicationStarted.Register(() =>
{
    var scope = app.Services.CreateScope();
    AppDbContext dbcontext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    //  dbcontext.Database.EnsureCreated();
    try
    {
        dbcontext.Database.Migrate();  
        CreateSuperUser(scope.ServiceProvider.GetRequiredService<UserManager<User>>());
    }
    catch (Exception ex)
    {
        app.UseExceptionHandler("/Home/Error");
    }


});



app.Run();


#region SuperUser
void CreateSuperUser(UserManager<User> userManager)
{
    var user = userManager
        .FindByEmailAsync(app.Configuration["AdminCredentials:Email"].ToString())
        .GetAwaiter()
        .GetResult();
    if (user == null)
    {
        try
        {
            var admin = new User
            {
                Email = app.Configuration["AdminCredentials:Email"].ToString(),
                FullName = app.Configuration["AdminCredentials:FullName"].ToString(),
                UserName = app.Configuration["AdminCredentials:Email"].ToString(),
                LevelId = 1,
                IsApproved = true,
                EmailConfirmed = true
            };

            var result = userManager
                .CreateAsync(admin, app.Configuration["AdminCredentials:Password"].ToString())
                .GetAwaiter()
                .GetResult();

            if (result.Succeeded)
            {
                var res = userManager.AddToRoleAsync(admin, "Admin").GetAwaiter().GetResult();
            }
        }
        catch (Exception ex) { }
    }
}
#endregion
