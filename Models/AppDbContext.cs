﻿using Microsoft.EntityFrameworkCore;
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


            builder.Entity<UserAnswer>().HasKey(y=>new { y.UserId,y.QuestionId});
            builder.Entity<UserAnswer>().HasOne(y => y.question).WithMany(y => y.userAnswers).HasForeignKey(y=>y.QuestionId);
            builder.Entity<UserAnswer>().HasOne(y => y.user).WithMany(y => y.userAnswers).HasForeignKey(y => y.UserId);


            builder.Entity<Question>().HasData(new Question { Id = 1 ,QuestionArtxt= "كيف تعرفت على الجمعية المصرية للمحللين الفنيين؟", QuestionEntxt= "How did you get to know the Egyptian Society of Technical Analysts?", IsYesNo=false}) ;
            builder.Entity<Question>().HasData(new Question { Id = 2, QuestionArtxt = "ماھي معلوماتك عن الجمعية المصرية للمحللين الفنيين؟", QuestionEntxt = "What is your information about the Egyptian Society of Technical Analysts?", IsYesNo = false });
            builder.Entity<Question>().HasData(new Question { Id = 3, QuestionArtxt = "لماذا ترغب في االلتحاق بالجمعية المصرية للمحللين الفنيين؟", QuestionEntxt = "Why would you like to join the Egyptian Society of Technical Analysts?", IsYesNo = false });
            builder.Entity<Question>().HasData(new Question { Id = 4, QuestionArtxt = "لو التحقت بالجمعية المصرية للمحللين الفنيين كيف يمكن أن تخدمھا؟", QuestionEntxt = "If you joined the Egyptian Society of Technical Analysts, how would you serve it?", IsYesNo = false });
            builder.Entity<Question>().HasData(new Question { Id = 5, QuestionArtxt = "ھل أنت عضو بجمعيات مماثلة سواء داخل مصر أو خارجھا؟", QuestionEntxt = "Are you a member of similar Societies inside or outside Egypt?", IsYesNo = true });
            builder.Entity<Question>().HasData(new Question { Id = 6, QuestionArtxt = "ھل لديك أي دراية بالتحليل الفني؟", QuestionEntxt = "Do you have any knowledge of technical analysis?", IsYesNo = true });
            builder.Entity<Question>().HasData(new Question { Id = 7, QuestionArtxt = "ھل لديك أي دراية بالتحليل المالي؟", QuestionEntxt = "Do you have any knowledge of financial analysis?", IsYesNo = true });
            builder.Entity<Question>().HasData(new Question { Id = 8, QuestionArtxt = "ھل أنت مستثمر بأسواق المال؟", QuestionEntxt = "Are you an investor in the capital markets?", IsYesNo = true });
            builder.Entity<Question>().HasData(new Question { Id = 9, QuestionArtxt = "ھل سبق توقيع أى عقوبات أو جزاءات عليك أو خضعت للتحقيق من ھيئة سوق المال أو\r\nالبورصة أو أى جھة رقابية أخرى داخل مصر أو خارجھا خالل الخمس سنين الماضية؟",
                QuestionEntxt = "Have any sanctions or penalties been imposed on you or have you been investigated by the Capital Market Authority or\r\nThe stock exchange or any other regulatory body inside or outside Egypt during the past five years?", IsYesNo = true });
          

        }

        public override DbSet<User> Users { get; set; }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Forum> Formus { get; set; }

        public DbSet<Level> Levels { get; set; }

        public DbSet<State> States { get; set; }



        public DbSet<Question> Questions { get; set; }
    


        public DbSet<UserForum> UsersForums { get; set; }
   
        public DbSet<UserCourse> UserCourses { get; set; }


        /// <summary>
        /// ///
        /// </summary>
        public DbSet<UserAnswer> UserAnswers { get; set; }

        public DbSet<Content> Contents { get; set; }

        public DbSet<Director> Directors { get; set; }
        public DbSet<Contact> Contacts { get; set; }

    }
}
