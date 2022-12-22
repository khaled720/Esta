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
  
        protected async override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserForum>()
                .HasOne(x => x.user)
                .WithMany(u => u.userForum)
                .HasForeignKey(x => x.userId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<UserForum>()
                .HasOne(x => x.forum)
                .WithMany(u => u.UserForum)
                .HasForeignKey(x => x.forumId);

            modelBuilder.Entity<UserForum>()
                .HasOne(x => x.Parent)
                .WithMany(u => u.Replies)
                .IsRequired(false)
                .HasForeignKey(x => x.ParentId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<User>()
                .HasOne(x => x.level)
                .WithMany(u => u.user)
                .HasForeignKey(x => x.LevelId);

            modelBuilder.Entity<Forum>()
                .HasOne(x => x.level)
                .WithMany(u => u.forum)
                .HasForeignKey(x => x.LevelId);

            base.OnModelCreating(modelBuilder);
        }

        public override DbSet<User> Users { get; set; }

        public DbSet<Course> Courses { get; set; }
        public DbSet<EventsNews> EventsNews { get; set; }
        public DbSet<Forum> Forums { get; set; }

        public DbSet<Level> Levels { get; set; }

        public DbSet<State> States { get; set; }

        public DbSet<UserForum> UsersForums { get; set; }

        public DbSet<UserCourse> UserCourses { get; set; }
    }
}
