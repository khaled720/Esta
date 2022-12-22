using ESTA.Models;
using ESTA.Repository;
using ESTA.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
//using ESTA.Migrations;

var builder = WebApplication.CreateBuilder(args);

//builder.WebHost.UseIISIntegration();
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession( /*opt=>opt.IdleTimeout=TimeSpan.FromMinutes(1)*/
);
builder.Services.AddDbContext<AppDbContext>(
    opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("dev_conn"))
);



builder.Services.AddScoped<IAppRep, AppRep>();

builder.Services.AddAuthorization(
    opt => opt.AddPolicy("RequireAdminRole", p => p.RequireRole("Admin"))
);
builder.Services
    .AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>();

var app = builder.Build();

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
;

app.UseAuthorization();

app.MapControllerRoute(name: "default", pattern: "{controller=Account}/{action=Login}/{id?}");


app.Lifetime.ApplicationStarted.Register(() =>
{
    var scope = app.Services.CreateScope();
    AppDbContext dbcontext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    //  dbcontext.Database.EnsureCreated();
  //  dbcontext.Database.Migrate();
    //CreateSuperUser(scope.ServiceProvider.GetRequiredService<UserManager<User>>());
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
