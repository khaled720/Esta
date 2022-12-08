using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ESTA.Models
{
    public class AppDbContext : IdentityDbContext<User>
    {

        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {

        }

        public override DbSet<User> Users { get; set; }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Forum> Formus { get; set; }
        public DbSet<UserForum> UsersForums { get; set; }
        public DbSet<Level> Levels { get; set; }

        public DbSet<State> States { get; set; }


        public DbSet<UserCourse> UserCourses { get; set; }


    }
}
