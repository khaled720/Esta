using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Reflection.Emit;
using Microsoft.AspNetCore.Identity;
namespace ESTA.Models
{
    public sealed class AppDbContext : IdentityDbContext<User>
    {
       

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            //  configurationBuilder.IgnoreAny(typeof(ForeignKeyIndexConvention));
        }
  
        protected async override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            /* 
                  builder.Entity<IdentityRole>().HasData(new IdentityRole() { Name = "Admin", NormalizedName = "ADMIN" });
                 builder.Entity<IdentityRole>().HasData(new IdentityRole() { Name = "User", NormalizedName = "USER" });

              builder.Entity<Level>().HasData(new Level() { Id = 1,TypeName="Ceta Level 1" }) ;
              builder.Entity<Level>().HasData(new Level() { Id = 2, TypeName = "Ceta Level 2" }); 
              builder.Entity<Level>().HasData(new Level() { Id = 3, TypeName = "Ceta Level 3" });
            */
            // builder.

          

          await  userManager.CreateAsync(new User() { FullName = "Adminstration", Email = "Admin@Admin.com" }, "Admin_2022");
           await userManager.AddToRoleAsync(new User() { FullName = "Adminstration", Email = "Admin@Admin.com" }, "Admin");
        }

        public override DbSet<User> Users { get; set; }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Forum> Formus { get; set; }

        public DbSet<Level> Levels { get; set; }

        public DbSet<State> States { get; set; }

        public DbSet<UserForum> UsersForums { get; set; }

        public DbSet<UserCourse> UserCourses { get; set; }
    }
}
