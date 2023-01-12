using ESTA.Models;
using ESTA.Repository;
using ESTA.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

using AutoMapper;
using ESTA.Mappers;
using ESTA.Helpers;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

//builder.WebHost.UseIISIntegration();
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession( /*opt=>opt.IdleTimeout=TimeSpan.FromMinutes(1)*/
);
builder.Services.AddDbContext<AppDbContext>(
    opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("dev_conn"))
);


//configure localization
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
//configure supported languages.
var supportedCultures = new[]
{
    new CultureInfo("en"),
    new CultureInfo("ar")
};
builder.Services.Configure<RequestLocalizationOptions>(options => {
    options.DefaultRequestCulture = new RequestCulture("en");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});

var config = new MapperConfiguration(cfg =>
{
    cfg.AddProfile(new ForumMapper());
    cfg.AddProfile(new EventsMapper());
});
var mapper = config.CreateMapper();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton(mapper);
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddAuthorization(
    opt => opt.AddPolicy("RequireAdminRole", p => p.RequireRole("Admin"))
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
            x.Response.Redirect("/Account/Login");
            return Task.CompletedTask;
        }
    };

});

var app = builder.Build();

//tell app to use the resources.
app.UseRequestLocalization(
    app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value
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
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();
app.UseAuthentication();


app.UseAuthorization();

app.MapControllerRoute(name: "default", pattern: "{controller=Account}/{action=Login}/{id?}");


app.Lifetime.ApplicationStarted.Register(() =>
{
    var scope = app.Services.CreateScope();
    AppDbContext dbcontext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    //  dbcontext.Database.EnsureCreated();
    dbcontext.Database.Migrate();
    CreateSuperUser(scope.ServiceProvider.GetRequiredService<UserManager<User>>());
});

app.Run();

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
