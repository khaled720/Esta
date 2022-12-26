using ESTA.Models;
using ESTA.Repository;
using ESTA.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using ESTA.Migrations;
using AutoMapper;
using ESTA.Mappers;
using ESTA.Helpers;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

//builder.WebHost.UseIISIntegration();
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession(/*opt=>opt.IdleTimeout=TimeSpan.FromMinutes(1)*/);
builder.Services.AddDbContext<AppDbContext>(opt=>
opt.UseSqlServer(builder.Configuration.GetConnectionString("dev_conn")));

builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<AppDbContext>();
builder.Services.AddScoped<IAppRep,AppRep>();
builder.Services.AddAuthorization(opt=>opt.AddPolicy("RequireAdminRole",p=>p.RequireRole("Admin")));
builder.Services.AddIdentityCore<User>(options => 
options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>();

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
app.UseAuthentication();;

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Lifetime.ApplicationStarted.Register(() =>
{
    var scope = app.Services.CreateScope();

    AppDbContext dbcontext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    //  dbcontext.Database.EnsureCreated();
    dbcontext.Database.Migrate();
    CreateSuperUser(scope.ServiceProvider.GetRequiredService<UserManager<User>>());
});

app.Run();



 void CreateSuperUser(UserManager<User> userManager){

  var d=  userManager.FindByEmailAsync(app.Configuration["AdminCredentials:Email"].ToString()).GetAwaiter().GetResult();    

}

