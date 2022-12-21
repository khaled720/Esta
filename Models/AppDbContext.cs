using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Reflection.Emit;
using Microsoft.AspNetCore.Identity;
using System.Runtime.CompilerServices;
using System.ComponentModel.DataAnnotations.Schema;

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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            builder.Entity<UserCourse>().HasKey(x => new { x.CourseId, x.UserId });
            builder.Entity<UserCourse>().HasOne(y => y.course).WithMany(b => b.users).HasForeignKey(y => y.CourseId);
            builder.Entity<UserCourse>().HasOne(y => y.user).WithMany(b => b.Courses).HasForeignKey(y => y.UserId);

            builder.Entity<UserForum>().HasKey(x => new { x.ForumId, x.UserId });
            builder.Entity<UserForum>().HasOne(y => y.user).WithMany(b => b.Forums).HasForeignKey(y => y.UserId);
            builder.Entity<UserForum>().HasOne(y => y.forum).WithMany(b => b.Users).HasForeignKey(y => y.ForumId);

            builder.Entity<Level>().HasData(new Level() { Id = 1,TypeName="Ceta Level 1" }) ;
            builder.Entity<Level>().HasData(new Level() { Id = 2, TypeName = "Ceta Level 2" }); 
              builder.Entity<Level>().HasData(new Level() { Id = 3, TypeName = "Ceta Level 3" });
            builder.Entity<Level>().HasData(new Level() { Id = 4, TypeName = "Non Ceta Level" });

            builder.Entity<State>().HasData(new State() { Id = 1, StateName = "Enrolled" });
            builder.Entity<State>().HasData(new State() { Id = 2, StateName = "In Progress" });
            builder.Entity<State>().HasData(new State() { Id =3, StateName = "Completed" });

            builder.Entity<IdentityRole>().HasData(new IdentityRole() { Name = "Admin", NormalizedName = "ADMIN" });
           builder.Entity<IdentityRole>().HasData(new IdentityRole() { Name = "User", NormalizedName = "USER" });

          
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
