using ESTA.Areas.Admin.Controllers;
using ESTA.Areas.Admin.Models;
using ESTA.Areas.Payment.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;

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

            modelBuilder.Entity<ForumBannedUser>().HasOne(y => y.Forum).WithMany(b => b.ForumBannedUser).HasForeignKey(y => y.ForumId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<ForumBannedUser>().HasOne(y => y.User).WithMany(b => b.ForumBannedUser).HasForeignKey(y => y.UserId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<ForumBannedUser>().HasOne(y => y.User).WithMany(b => b.ForumBannedUser).HasForeignKey(y => y.UserId).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ModeratorForum>().HasKey(sc => new { sc.ForumId, sc.UserId });
            modelBuilder.Entity<ModeratorForum>().HasOne(y => y.Forum).WithMany(b => b.ModeratorForums).HasForeignKey(y => y.ForumId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<ModeratorForum>().HasOne(y => y.User).WithMany(b => b.ModeratorForums).HasForeignKey(y => y.UserId).OnDelete(DeleteBehavior.NoAction);

            //modelBuilder.Entity<UserCourse>().HasKey(x => new { x.CourseId, x.UserId });
            modelBuilder.Entity<UserCourse>().HasOne(y => y.course).WithMany(b => b.users).HasForeignKey(y => y.CourseId);
            modelBuilder.Entity<UserCourse>().HasOne(y => y.user).WithMany(b => b.Courses).HasForeignKey(y => y.UserId);

            //modelBuilder.Entity<UserForum>().HasKey(x => new { x.ForumId, x.UserId });
            modelBuilder.Entity<UserForum>().HasOne(y => y.user).WithMany(b => b.userForum).HasForeignKey(y => y.userId);
            modelBuilder.Entity<UserForum>().HasOne(y => y.forum).WithMany(b => b.UserForum).HasForeignKey(y => y.forumId);

            modelBuilder.Entity<Level>().HasData(new Level() { Id = 1, TypeName = "Ceta Level 1" });
            modelBuilder.Entity<Level>().HasData(new Level() { Id = 2, TypeName = "Ceta Level 2" });
            modelBuilder.Entity<Level>().HasData(new Level() { Id = 3, TypeName = "Ceta Level 3" });
            modelBuilder.Entity<Level>().HasData(new Level() { Id = 4, TypeName = "Non Ceta Level" });

            modelBuilder.Entity<State>().HasData(new State() { Id = 1, StateName = "Enrolled" });
            modelBuilder.Entity<State>().HasData(new State() { Id = 2, StateName = "In Progress" });
            modelBuilder.Entity<State>().HasData(new State() { Id = 3, StateName = "Completed" });
            modelBuilder.Entity<State>().HasData(new State() { Id = 4, StateName = "Refunded" });
            modelBuilder.Entity<State>().HasData(new State() { Id = 5, StateName = "Failed" });

            //modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole() { Name = "Admin", NormalizedName = "ADMIN" });
            //modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole() { Name = "User", NormalizedName = "USER" });
            //modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole() { Name = "Moderator", NormalizedName = "MODERATOR" });


            modelBuilder.Entity<UserAnswer>().HasKey(y => new { y.UserId, y.QuestionId });
            modelBuilder.Entity<UserAnswer>().HasOne(y => y.question).WithMany(y => y.userAnswers).HasForeignKey(y => y.QuestionId);
            modelBuilder.Entity<UserAnswer>().HasOne(y => y.user).WithMany(y => y.userAnswers).HasForeignKey(y => y.UserId);

            modelBuilder.Entity<UserForum>()
                .HasOne(x => x.Parent)
                .WithMany(u => u.Replies)
                .IsRequired(false)
                .HasForeignKey(x => x.ParentId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Question>().HasData(new Question { Id = 1, QuestionArtxt = "كيف تعرفت على الجمعية المصرية للمحللين الفنيين؟", QuestionEntxt = "How did you get to know the Egyptian Society of Technical Analysts?", IsYesNo = false });
            modelBuilder.Entity<Question>().HasData(new Question { Id = 2, QuestionArtxt = "ماھي معلوماتك عن الجمعية المصرية للمحللين الفنيين؟", QuestionEntxt = "What is your information about the Egyptian Society of Technical Analysts?", IsYesNo = false });
            modelBuilder.Entity<Question>().HasData(new Question { Id = 3, QuestionArtxt = "لماذا ترغب في االلتحاق بالجمعية المصرية للمحللين الفنيين؟", QuestionEntxt = "Why would you like to join the Egyptian Society of Technical Analysts?", IsYesNo = false });
            modelBuilder.Entity<Question>().HasData(new Question { Id = 4, QuestionArtxt = "لو التحقت بالجمعية المصرية للمحللين الفنيين كيف يمكن أن تخدمھا؟", QuestionEntxt = "If you joined the Egyptian Society of Technical Analysts, how would you serve it?", IsYesNo = false });
            modelBuilder.Entity<Question>().HasData(new Question { Id = 5, QuestionArtxt = "ھل أنت عضو بجمعيات مماثلة سواء داخل مصر أو خارجھا؟", QuestionEntxt = "Are you a member of similar Societies inside or outside Egypt?", IsYesNo = true });
            modelBuilder.Entity<Question>().HasData(new Question { Id = 6, QuestionArtxt = "ھل لديك أي دراية بالتحليل الفني؟", QuestionEntxt = "Do you have any knowledge of technical analysis?", IsYesNo = true });
            modelBuilder.Entity<Question>().HasData(new Question { Id = 7, QuestionArtxt = "ھل لديك أي دراية بالتحليل المالي؟", QuestionEntxt = "Do you have any knowledge of financial analysis?", IsYesNo = true });
            modelBuilder.Entity<Question>().HasData(new Question { Id = 8, QuestionArtxt = "ھل أنت مستثمر بأسواق المال؟", QuestionEntxt = "Are you an investor in the capital markets?", IsYesNo = true });
            modelBuilder.Entity<Question>().HasData(new Question
            {
                Id = 9,
                QuestionArtxt = "ھل سبق توقيع أى عقوبات أو جزاءات عليك أو خضعت للتحقيق من ھيئة سوق المال أو\r\nالبورصة أو أى جھة رقابية أخرى داخل مصر أو خارجھا خالل الخمس سنين الماضية؟",
                QuestionEntxt = "Have any sanctions or penalties been imposed on you or have you been investigated by the Capital Market Authority or\r\nThe stock exchange or any other regulatory body inside or outside Egypt during the past five years?",
                IsYesNo = true
            });

            modelBuilder.Entity<Forum>()
                .HasOne(x => x.level)
                .WithMany(u => u.forum)
                .HasForeignKey(x => x.LevelId);



            modelBuilder.Entity<PrerequisiteCourse>().HasKey(x => new { x.MainCourseId, x.PrerequisiteCourseId });
            //modelBuilder.Entity<PrerequisiteCourse>().HasOne(y => y.MainCourse)
            //    .WithMany(b => b.PrerequisiteCourses).HasForeignKey(y => y.MainCourseId);
            //modelBuilder.Entity<PrerequisiteCourse>().HasOne(y => y.prerequisiteCourse)
            //    .WithMany(b => b.PrerequisiteCourses).HasForeignKey(y => y.PrerequisiteCourseId);


            modelBuilder.Entity<ImageType>().HasData(new ImageType() { Id = 1, Name = "NationalId" });
            modelBuilder.Entity<ImageType>().HasData(new ImageType() { Id = 2, Name = "Passport" });
            modelBuilder.Entity<ImageType>().HasData(new ImageType() { Id = 3, Name = "Gradution" });
            modelBuilder.Entity<ImageType>().HasData(new ImageType() { Id = 4, Name = "ProfilePicture" });

            modelBuilder.Entity<Countries>().HasData(
new Countries() { Id = 1, Code = "AF", NameEn = "Afghanistan", NameAr = "أفغانستان" },
new Countries() { Id = 2, Code = "AL", NameEn = "Albania", NameAr = "ألبانيا" },
new Countries() { Id = 3, Code = "AX", NameEn = "Aland Islands", NameAr = "جزر آلاند" },
new Countries() { Id = 4, Code = "DZ", NameEn = "Algeria", NameAr = "الجزائر" },
new Countries() { Id = 5, Code = "AS", NameEn = "American Samoa", NameAr = "ساموا-الأمريكي" },
new Countries() { Id = 6, Code = "AD", NameEn = "Andorra", NameAr = "أندورا" },
new Countries() { Id = 7, Code = "AO", NameEn = "Angola", NameAr = "أنغولا" },
new Countries() { Id = 8, Code = "AI", NameEn = "Anguilla", NameAr = "أنغويلا" },
new Countries() { Id = 9, Code = "AQ", NameEn = "Antarctica", NameAr = "أنتاركتيكا" },
new Countries() { Id = 10, Code = "AG", NameEn = "Antigua and Barbuda", NameAr = "أنتيغوا وبربودا" },
new Countries() { Id = 11, Code = "AR", NameEn = "Argentina", NameAr = "الأرجنتين" },
new Countries() { Id = 12, Code = "AM", NameEn = "Armenia", NameAr = "أرمينيا" },
new Countries() { Id = 13, Code = "AW", NameEn = "Aruba", NameAr = "أروبه" },
new Countries() { Id = 14, Code = "AU", NameEn = "Australia", NameAr = "أستراليا" },
new Countries() { Id = 15, Code = "AT", NameEn = "Austria", NameAr = "النمسا" },
new Countries() { Id = 16, Code = "AZ", NameEn = "Azerbaijan", NameAr = "أذربيجان" },
new Countries() { Id = 17, Code = "BS", NameEn = "Bahamas", NameAr = "الباهاماس" },
new Countries() { Id = 18, Code = "BH", NameEn = "Bahrain", NameAr = "البحرين" },
new Countries() { Id = 19, Code = "BD", NameEn = "Bangladesh", NameAr = "بنغلاديش" },
new Countries() { Id = 20, Code = "BB", NameEn = "Barbados", NameAr = "بربادوس" },
new Countries() { Id = 21, Code = "BY", NameEn = "Belarus", NameAr = "روسيا البيضاء" },
new Countries() { Id = 22, Code = "BE", NameEn = "Belgium", NameAr = "بلجيكا" },
new Countries() { Id = 23, Code = "BZ", NameEn = "Belize", NameAr = "بيليز" },
new Countries() { Id = 24, Code = "BJ", NameEn = "Benin", NameAr = "بنين" },
new Countries() { Id = 25, Code = "BL", NameEn = "Saint Barthelemy", NameAr = "سان بارتيلمي" },
new Countries() { Id = 26, Code = "BM", NameEn = "Bermuda", NameAr = "جزر برمودا" },
new Countries() { Id = 27, Code = "BT", NameEn = "Bhutan", NameAr = "بوتان" },
new Countries() { Id = 28, Code = "BO", NameEn = "Bolivia", NameAr = "بوليفيا" },
new Countries() { Id = 29, Code = "BA", NameEn = "Bosnia and Herzegovina", NameAr = "البوسنة و الهرسك" },
new Countries() { Id = 30, Code = "BW", NameEn = "Botswana", NameAr = "بوتسوانا" },
new Countries() { Id = 31, Code = "BV", NameEn = "Bouvet Island", NameAr = "جزيرة بوفيه" },
new Countries() { Id = 32, Code = "BR", NameEn = "Brazil", NameAr = "البرازيل" },
new Countries() { Id = 33, Code = "IO", NameEn = "British Indian Ocean Territory", NameAr = "إقليم المحيط الهندي البريطاني" },
new Countries() { Id = 34, Code = "BN", NameEn = "Brunei Darussalam", NameAr = "بروني" },
new Countries() { Id = 35, Code = "BG", NameEn = "Bulgaria", NameAr = "بلغاريا" },
new Countries() { Id = 36, Code = "BF", NameEn = "Burkina Faso", NameAr = "بوركينا فاسو" },
new Countries() { Id = 37, Code = "BI", NameEn = "Burundi", NameAr = "بوروندي" },
new Countries() { Id = 38, Code = "KH", NameEn = "Cambodia", NameAr = "كمبوديا" },
new Countries() { Id = 39, Code = "CM", NameEn = "Cameroon", NameAr = "كاميرون" },
new Countries() { Id = 40, Code = "CA", NameEn = "Canada", NameAr = "كندا" },
new Countries() { Id = 41, Code = "CV", NameEn = "Cape Verde", NameAr = "الرأس الأخضر" },
new Countries() { Id = 42, Code = "KY", NameEn = "Cayman Islands", NameAr = "جزر كايمان" },
new Countries() { Id = 43, Code = "CF", NameEn = "Central African Republic", NameAr = "جمهورية أفريقيا الوسطى" },
new Countries() { Id = 44, Code = "TD", NameEn = "Chad", NameAr = "تشاد" },
new Countries() { Id = 45, Code = "CL", NameEn = "Chile", NameAr = "شيلي" },
new Countries() { Id = 46, Code = "CN", NameEn = "China", NameAr = "الصين" },
new Countries() { Id = 47, Code = "CX", NameEn = "Christmas Island", NameAr = "جزيرة عيد الميلاد" },
new Countries() { Id = 48, Code = "CC", NameEn = "Cocos (Keeling) Islands", NameAr = "جزر كوكوس" },
new Countries() { Id = 49, Code = "CO", NameEn = "Colombia", NameAr = "كولومبيا" },
new Countries() { Id = 50, Code = "KM", NameEn = "Comoros", NameAr = "جزر القمر" },
new Countries() { Id = 51, Code = "CG", NameEn = "Congo", NameAr = "الكونغو" },
new Countries() { Id = 52, Code = "CK", NameEn = "Cook Islands", NameAr = "جزر كوك" },
new Countries() { Id = 53, Code = "CR", NameEn = "Costa Rica", NameAr = "كوستاريكا" },
new Countries() { Id = 54, Code = "HR", NameEn = "Croatia", NameAr = "كرواتيا" },
new Countries() { Id = 55, Code = "CU", NameEn = "Cuba", NameAr = "كوبا" },
new Countries() { Id = 56, Code = "CY", NameEn = "Cyprus", NameAr = "قبرص" },
new Countries() { Id = 57, Code = "CW", NameEn = "Curaçao", NameAr = "كوراساو" },
new Countries() { Id = 58, Code = "CZ", NameEn = "Czech Republic", NameAr = "الجمهورية التشيكية" },
new Countries() { Id = 59, Code = "DK", NameEn = "Denmark", NameAr = "الدانمارك" },
new Countries() { Id = 60, Code = "DJ", NameEn = "Djibouti", NameAr = "جيبوتي" },
new Countries() { Id = 61, Code = "DM", NameEn = "Dominica", NameAr = "دومينيكا" },
new Countries() { Id = 62, Code = "DO", NameEn = "Dominican Republic", NameAr = "الجمهورية الدومينيكية" },
new Countries() { Id = 63, Code = "EC", NameEn = "Ecuador", NameAr = "إكوادور" },
new Countries() { Id = 64, Code = "EG", NameEn = "Egypt", NameAr = "مصر" },
new Countries() { Id = 65, Code = "SV", NameEn = "El Salvador", NameAr = "إلسلفادور" },
new Countries() { Id = 66, Code = "GQ", NameEn = "Equatorial Guinea", NameAr = "غينيا الاستوائي" },
new Countries() { Id = 67, Code = "ER", NameEn = "Eritrea", NameAr = "إريتريا" },
new Countries() { Id = 68, Code = "EE", NameEn = "Estonia", NameAr = "استونيا" },
new Countries() { Id = 69, Code = "ET", NameEn = "Ethiopia", NameAr = "أثيوبيا" },
new Countries() { Id = 70, Code = "FK", NameEn = "Falkland Islands (Malvinas)", NameAr = "جزر فوكلاند" },
new Countries() { Id = 71, Code = "FO", NameEn = "Faroe Islands", NameAr = "جزر فارو" },
new Countries() { Id = 72, Code = "FJ", NameEn = "Fiji", NameAr = "فيجي" },
new Countries() { Id = 73, Code = "FI", NameEn = "Finland", NameAr = "فنلندا" },
new Countries() { Id = 74, Code = "FR", NameEn = "France", NameAr = "فرنسا" },
new Countries() { Id = 75, Code = "GF", NameEn = "French Guiana", NameAr = "غويانا الفرنسية" },
new Countries() { Id = 76, Code = "PF", NameEn = "French Polynesia", NameAr = "بولينيزيا الفرنسية" },
new Countries() { Id = 77, Code = "TF", NameEn = "French Southern and Antarctic Lands", NameAr = "أراض فرنسية جنوبية وأنتارتيكية" },
new Countries() { Id = 78, Code = "GA", NameEn = "Gabon", NameAr = "الغابون" },
new Countries() { Id = 79, Code = "GM", NameEn = "Gambia", NameAr = "غامبيا" },
new Countries() { Id = 80, Code = "GE", NameEn = "Georgia", NameAr = "جيورجيا" },
new Countries() { Id = 81, Code = "DE", NameEn = "Germany", NameAr = "ألمانيا" },
new Countries() { Id = 82, Code = "GH", NameEn = "Ghana", NameAr = "غانا" },
new Countries() { Id = 83, Code = "GI", NameEn = "Gibraltar", NameAr = "جبل طارق" },
new Countries() { Id = 84, Code = "GG", NameEn = "Guernsey", NameAr = "غيرنزي" },
new Countries() { Id = 85, Code = "GR", NameEn = "Greece", NameAr = "اليونان" },
new Countries() { Id = 86, Code = "GL", NameEn = "Greenland", NameAr = "جرينلاند" },
new Countries() { Id = 87, Code = "GD", NameEn = "Grenada", NameAr = "غرينادا" },
new Countries() { Id = 88, Code = "GP", NameEn = "Guadeloupe", NameAr = "جزر جوادلوب" },
new Countries() { Id = 89, Code = "GU", NameEn = "Guam", NameAr = "جوام" },
new Countries() { Id = 90, Code = "GT", NameEn = "Guatemala", NameAr = "غواتيمال" },
new Countries() { Id = 91, Code = "GN", NameEn = "Guinea", NameAr = "غينيا" },
new Countries() { Id = 92, Code = "GW", NameEn = "Guinea-Bissau", NameAr = "غينيا-بيساو" },
new Countries() { Id = 93, Code = "GY", NameEn = "Guyana", NameAr = "غيانا" },
new Countries() { Id = 94, Code = "HT", NameEn = "Haiti", NameAr = "هايتي" },
new Countries() { Id = 95, Code = "HM", NameEn = "Heard and Mc Donald Islands", NameAr = "جزيرة هيرد وجزر ماكدونالد" },
new Countries() { Id = 96, Code = "HN", NameEn = "Honduras", NameAr = "هندوراس" },
new Countries() { Id = 97, Code = "HK", NameEn = "Hong Kong", NameAr = "هونغ كونغ" },
new Countries() { Id = 98, Code = "HU", NameEn = "Hungary", NameAr = "المجر" },
new Countries() { Id = 99, Code = "IS", NameEn = "Iceland", NameAr = "آيسلندا" },
new Countries() { Id = 100, Code = "IN", NameEn = "India", NameAr = "الهند" },
new Countries() { Id = 101, Code = "IM", NameEn = "Isle of Man", NameAr = "جزيرة مان" },
new Countries() { Id = 102, Code = "ID", NameEn = "Indonesia", NameAr = "أندونيسيا" },
new Countries() { Id = 103, Code = "IR", NameEn = "Iran", NameAr = "إيران" },
new Countries() { Id = 104, Code = "IQ", NameEn = "Iraq", NameAr = "العراق" },
new Countries() { Id = 105, Code = "IE", NameEn = "Ireland", NameAr = "إيرلندا" },
new Countries() { Id = 107, Code = "IT", NameEn = "Italy", NameAr = "إيطاليا" },
new Countries() { Id = 108, Code = "CI", NameEn = "Ivory Coast", NameAr = "ساحل العاج" },
new Countries() { Id = 109, Code = "JE", NameEn = "Jersey", NameAr = "جيرزي" },
new Countries() { Id = 110, Code = "JM", NameEn = "Jamaica", NameAr = "جمايكا" },
new Countries() { Id = 111, Code = "JP", NameEn = "Japan", NameAr = "اليابان" },
new Countries() { Id = 112, Code = "JO", NameEn = "Jordan", NameAr = "الأردن" },
new Countries() { Id = 113, Code = "KZ", NameEn = "Kazakhstan", NameAr = "كازاخستان" },
new Countries() { Id = 114, Code = "KE", NameEn = "Kenya", NameAr = "كينيا" },
new Countries() { Id = 115, Code = "KI", NameEn = "Kiribati", NameAr = "كيريباتي" },
new Countries() { Id = 116, Code = "KP", NameEn = "Korea(North Korea)", NameAr = "كوريا الشمالية" },
new Countries() { Id = 117, Code = "KR", NameEn = "Korea(South Korea)", NameAr = "كوريا الجنوبية" },
new Countries() { Id = 118, Code = "XK", NameEn = "Kosovo", NameAr = "كوسوفو" },
new Countries() { Id = 119, Code = "KW", NameEn = "Kuwait", NameAr = "الكويت" },
new Countries() { Id = 120, Code = "KG", NameEn = "Kyrgyzstan", NameAr = "قيرغيزستان" },
new Countries() { Id = 121, Code = "LA", NameEn = "Lao PDR", NameAr = "لاوس" },
new Countries() { Id = 122, Code = "LV", NameEn = "Latvia", NameAr = "لاتفيا" },
new Countries() { Id = 123, Code = "LB", NameEn = "Lebanon", NameAr = "لبنان" },
new Countries() { Id = 124, Code = "LS", NameEn = "Lesotho", NameAr = "ليسوتو" },
new Countries() { Id = 125, Code = "LR", NameEn = "Liberia", NameAr = "ليبيريا" },
new Countries() { Id = 126, Code = "LY", NameEn = "Libya", NameAr = "ليبيا" },
new Countries() { Id = 127, Code = "LI", NameEn = "Liechtenstein", NameAr = "ليختنشتين" },
new Countries() { Id = 128, Code = "LT", NameEn = "Lithuania", NameAr = "لتوانيا" },
new Countries() { Id = 129, Code = "LU", NameEn = "Luxembourg", NameAr = "لوكسمبورغ" },
new Countries() { Id = 130, Code = "LK", NameEn = "Sri Lanka", NameAr = "سريلانكا" },
new Countries() { Id = 131, Code = "MO", NameEn = "Macau", NameAr = "ماكاو" },
new Countries() { Id = 132, Code = "MK", NameEn = "Macedonia", NameAr = "مقدونيا" },
new Countries() { Id = 133, Code = "MG", NameEn = "Madagascar", NameAr = "مدغشقر" },
new Countries() { Id = 134, Code = "MW", NameEn = "Malawi", NameAr = "مالاوي" },
new Countries() { Id = 135, Code = "MY", NameEn = "Malaysia", NameAr = "ماليزيا" },
new Countries() { Id = 136, Code = "MV", NameEn = "Maldives", NameAr = "المالديف" },
new Countries() { Id = 137, Code = "ML", NameEn = "Mali", NameAr = "مالي" },
new Countries() { Id = 138, Code = "MT", NameEn = "Malta", NameAr = "مالطا" },
new Countries() { Id = 139, Code = "MH", NameEn = "Marshall Islands", NameAr = "جزر مارشال" },
new Countries() { Id = 140, Code = "MQ", NameEn = "Martinique", NameAr = "مارتينيك" },
new Countries() { Id = 141, Code = "MR", NameEn = "Mauritania", NameAr = "موريتانيا" },
new Countries() { Id = 142, Code = "MU", NameEn = "Mauritius", NameAr = "موريشيوس" },
new Countries() { Id = 143, Code = "YT", NameEn = "Mayotte", NameAr = "مايوت" },
new Countries() { Id = 144, Code = "MX", NameEn = "Mexico", NameAr = "المكسيك" },
new Countries() { Id = 145, Code = "FM", NameEn = "Micronesia", NameAr = "مايكرونيزيا" },
new Countries() { Id = 146, Code = "MD", NameEn = "Moldova", NameAr = "مولدافيا" },
new Countries() { Id = 147, Code = "MC", NameEn = "Monaco", NameAr = "موناكو" },
new Countries() { Id = 148, Code = "MN", NameEn = "Mongolia", NameAr = "منغوليا" },
new Countries() { Id = 149, Code = "ME", NameEn = "Montenegro", NameAr = "الجبل الأسود" },
new Countries() { Id = 150, Code = "MS", NameEn = "Montserrat", NameAr = "مونتسيرات" },
new Countries() { Id = 151, Code = "MA", NameEn = "Morocco", NameAr = "المغرب" },
new Countries() { Id = 152, Code = "MZ", NameEn = "Mozambique", NameAr = "موزمبيق" },
new Countries() { Id = 153, Code = "MM", NameEn = "Myanmar", NameAr = "ميانمار" },
new Countries() { Id = 154, Code = "NA", NameEn = "Namibia", NameAr = "ناميبيا" },
new Countries() { Id = 155, Code = "NR", NameEn = "Nauru", NameAr = "نورو" },
new Countries() { Id = 156, Code = "NP", NameEn = "Nepal", NameAr = "نيبال" },
new Countries() { Id = 157, Code = "NL", NameEn = "Netherlands", NameAr = "هولندا" },
new Countries() { Id = 158, Code = "AN", NameEn = "Netherlands Antilles", NameAr = "جزر الأنتيل الهولندي" },
new Countries() { Id = 159, Code = "NC", NameEn = "New Caledonia", NameAr = "كاليدونيا الجديدة" },
new Countries() { Id = 160, Code = "NZ", NameEn = "New Zealand", NameAr = "نيوزيلندا" },
new Countries() { Id = 161, Code = "NI", NameEn = "Nicaragua", NameAr = "نيكاراجوا" },
new Countries() { Id = 162, Code = "NE", NameEn = "Niger", NameAr = "النيجر" },
new Countries() { Id = 163, Code = "NG", NameEn = "Nigeria", NameAr = "نيجيريا" },
new Countries() { Id = 164, Code = "NU", NameEn = "Niue", NameAr = "ني" },
new Countries() { Id = 165, Code = "NF", NameEn = "Norfolk Island", NameAr = "جزيرة نورفولك" },
new Countries() { Id = 166, Code = "MP", NameEn = "Northern Mariana Islands", NameAr = "جزر ماريانا الشمالية" },
new Countries() { Id = 167, Code = "NO", NameEn = "Norway", NameAr = "النرويج" },
new Countries() { Id = 168, Code = "OM", NameEn = "Oman", NameAr = "عمان" },
new Countries() { Id = 169, Code = "PK", NameEn = "Pakistan", NameAr = "باكستان" },
new Countries() { Id = 170, Code = "PW", NameEn = "Palau", NameAr = "بالاو" },
new Countries() { Id = 171, Code = "PS", NameEn = "Palestine", NameAr = "فلسطين" },
new Countries() { Id = 172, Code = "PA", NameEn = "Panama", NameAr = "بنما" },
new Countries() { Id = 173, Code = "PG", NameEn = "Papua New Guinea", NameAr = "بابوا غينيا الجديدة" },
new Countries() { Id = 174, Code = "PY", NameEn = "Paraguay", NameAr = "باراغواي" },
new Countries() { Id = 175, Code = "PE", NameEn = "Peru", NameAr = "بيرو" },
new Countries() { Id = 176, Code = "PH", NameEn = "Philippines", NameAr = "الفليبين" },
new Countries() { Id = 177, Code = "PN", NameEn = "Pitcairn", NameAr = "بيتكيرن" },
new Countries() { Id = 178, Code = "PL", NameEn = "Poland", NameAr = "بولندا" },
new Countries() { Id = 179, Code = "PT", NameEn = "Portugal", NameAr = "البرتغال" },
new Countries() { Id = 180, Code = "PR", NameEn = "Puerto Rico", NameAr = "بورتو ريكو" },
new Countries() { Id = 181, Code = "QA", NameEn = "Qatar", NameAr = "قطر" },
new Countries() { Id = 182, Code = "RE", NameEn = "Reunion Island", NameAr = "ريونيون" },
new Countries() { Id = 183, Code = "RO", NameEn = "Romania", NameAr = "رومانيا" },
new Countries() { Id = 184, Code = "RU", NameEn = "Russian", NameAr = "روسيا" },
new Countries() { Id = 185, Code = "RW", NameEn = "Rwanda", NameAr = "رواندا" },
new Countries() { Id = 186, Code = "KN", NameEn = "Saint Kitts and Nevis", NameAr = "سانت كيتس ونيفس," },
new Countries() { Id = 187, Code = "MF", NameEn = "Saint Martin (French part)", NameAr = "ساينت مارتن فرنسي" },
new Countries() { Id = 188, Code = "SX", NameEn = "Sint Maarten (Dutch part)", NameAr = "ساينت مارتن هولندي" },
new Countries() { Id = 189, Code = "LC", NameEn = "Saint Pierre and Miquelon", NameAr = "سان بيير وميكلون" },
new Countries() { Id = 190, Code = "VC", NameEn = "Saint Vincent and the Grenadines", NameAr = "سانت فنسنت وجزر غرينادين" },
new Countries() { Id = 191, Code = "WS", NameEn = "Samoa", NameAr = "ساموا" },
new Countries() { Id = 192, Code = "SM", NameEn = "San Marino", NameAr = "سان مارينو" },
new Countries() { Id = 193, Code = "ST", NameEn = "Sao Tome and Principe", NameAr = "ساو تومي وبرينسيبي" },
new Countries() { Id = 194, Code = "SA", NameEn = "Saudi Arabia", NameAr = "المملكة العربية السعودية" },
new Countries() { Id = 195, Code = "SN", NameEn = "Senegal", NameAr = "السنغال" },
new Countries() { Id = 196, Code = "RS", NameEn = "Serbia", NameAr = "صربيا" },
new Countries() { Id = 197, Code = "SC", NameEn = "Seychelles", NameAr = "سيشيل" },
new Countries() { Id = 198, Code = "SL", NameEn = "Sierra Leone", NameAr = "سيراليون" },
new Countries() { Id = 199, Code = "SG", NameEn = "Singapore", NameAr = "سنغافورة" },
new Countries() { Id = 200, Code = "SK", NameEn = "Slovakia", NameAr = "سلوفاكيا" },
new Countries() { Id = 201, Code = "SI", NameEn = "Slovenia", NameAr = "سلوفينيا" },
new Countries() { Id = 202, Code = "SB", NameEn = "Solomon Islands", NameAr = "جزر سليمان" },
new Countries() { Id = 203, Code = "SO", NameEn = "Somalia", NameAr = "الصومال" },
new Countries() { Id = 204, Code = "ZA", NameEn = "South Africa", NameAr = "جنوب أفريقيا" },
new Countries() { Id = 205, Code = "GS", NameEn = "South Georgia and the South Sandwich", NameAr = "المنطقة القطبية الجنوبية" },
new Countries() { Id = 206, Code = "SS", NameEn = "South Sudan", NameAr = "السودان الجنوبي" },
new Countries() { Id = 207, Code = "ES", NameEn = "Spain", NameAr = "إسبانيا" },
new Countries() { Id = 208, Code = "SH", NameEn = "Saint Helena", NameAr = "سانت هيلانة" },
new Countries() { Id = 209, Code = "SD", NameEn = "Sudan", NameAr = "السودان" },
new Countries() { Id = 210, Code = "SR", NameEn = "Suriname", NameAr = "سورينام" },
new Countries() { Id = 211, Code = "SJ", NameEn = "Svalbard and Jan Mayen", NameAr = "سفالبارد ويان ماين" },
new Countries() { Id = 212, Code = "SZ", NameEn = "Swaziland", NameAr = "سوازيلند" },
new Countries() { Id = 213, Code = "SE", NameEn = "Sweden", NameAr = "السويد" },
new Countries() { Id = 214, Code = "CH", NameEn = "Switzerland", NameAr = "سويسرا" },
new Countries() { Id = 215, Code = "SY", NameEn = "Syria", NameAr = "سوريا" },
new Countries() { Id = 216, Code = "TW", NameEn = "Taiwan", NameAr = "تايوان" },
new Countries() { Id = 217, Code = "TJ", NameEn = "Tajikistan", NameAr = "طاجيكستان" },
new Countries() { Id = 218, Code = "TZ", NameEn = "Tanzania", NameAr = "تنزانيا" },
new Countries() { Id = 219, Code = "TH", NameEn = "Thailand", NameAr = "تايلندا" },
new Countries() { Id = 220, Code = "TL", NameEn = "Timor-Leste", NameAr = "تيمور الشرقية" },
new Countries() { Id = 221, Code = "TG", NameEn = "Togo", NameAr = "توغو" },
new Countries() { Id = 222, Code = "TK", NameEn = "Tokelau", NameAr = "توكيلاو" },
new Countries() { Id = 223, Code = "TO", NameEn = "Tonga", NameAr = "تونغا" },
new Countries() { Id = 224, Code = "TT", NameEn = "Trinidad and Tobago", NameAr = "ترينيداد وتوباغو" },
new Countries() { Id = 225, Code = "TN", NameEn = "Tunisia", NameAr = "تونس" },
new Countries() { Id = 226, Code = "TR", NameEn = "Turkey", NameAr = "تركيا" },
new Countries() { Id = 227, Code = "TM", NameEn = "Turkmenistan", NameAr = "تركمانستان" },
new Countries() { Id = 228, Code = "TC", NameEn = "Turks and Caicos Islands", NameAr = "جزر توركس وكايكوس" },
new Countries() { Id = 229, Code = "TV", NameEn = "Tuvalu", NameAr = "توفالو" },
new Countries() { Id = 230, Code = "UG", NameEn = "Uganda", NameAr = "أوغندا" },
new Countries() { Id = 231, Code = "UA", NameEn = "Ukraine", NameAr = "أوكرانيا" },
new Countries() { Id = 232, Code = "AE", NameEn = "United Arab Emirates", NameAr = "الإمارات العربية المتحدة" },
new Countries() { Id = 233, Code = "GB", NameEn = "United Kingdom", NameAr = "المملكة المتحدة" },
new Countries() { Id = 234, Code = "US", NameEn = "United States", NameAr = "الولايات المتحدة" },
new Countries() { Id = 235, Code = "UM", NameEn = "US Minor Outlying Islands", NameAr = "قائمة الولايات والمناطق الأمريكية" },
new Countries() { Id = 236, Code = "UY", NameEn = "Uruguay", NameAr = "أورغواي" },
new Countries() { Id = 237, Code = "UZ", NameEn = "Uzbekistan", NameAr = "أوزباكستان" },
new Countries() { Id = 238, Code = "VU", NameEn = "Vanuatu", NameAr = "فانواتو" },
new Countries() { Id = 239, Code = "VE", NameEn = "Venezuela", NameAr = "فنزويلا" },
new Countries() { Id = 240, Code = "VN", NameEn = "Vietnam", NameAr = "فيتنام" },
new Countries() { Id = 241, Code = "VI", NameEn = "Virgin Islands (U.S.)", NameAr = "الجزر العذراء الأمريكي" },
new Countries() { Id = 242, Code = "VA", NameEn = "Vatican City", NameAr = "فنزويلا" },
new Countries() { Id = 243, Code = "WF", NameEn = "Wallis and Futuna Islands", NameAr = "والس وفوتونا" },
new Countries() { Id = 244, Code = "EH", NameEn = "Western Sahara", NameAr = "الصحراء الغربية" },
new Countries() { Id = 245, Code = "YE", NameEn = "Yemen", NameAr = "اليمن" },
new Countries() { Id = 246, Code = "ZM", NameEn = "Zambia", NameAr = "زامبيا" },
new Countries() { Id = 247, Code = "ZW", NameEn = "Zimbabwe", NameAr = "زمبابوي" }

);
            base.OnModelCreating(modelBuilder);
        }

        public override DbSet<User> Users { get; set; }

        public DbSet<Course> Courses { get; set; }
        public DbSet<EventsNews> EventsNews { get; set; }
        public DbSet<Forum> Forums { get; set; }

        public DbSet<Level> Levels { get; set; }

        public DbSet<State> States { get; set; }

        public DbSet<Countries> Countries { get; set; }

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

        public DbSet<ImageType> ImageTypes { get; set; }
        public DbSet<UserImage> UserImages { get; set; }

        public DbSet<ModeratorForum> ModeratorForums { get; set; }
        public DbSet<ForumBannedUser> ForumBannedUser { get; set; }


        public DbSet<MempershipOrder> MempershipOrders { get; set; }

        public DbSet<MempershipPayment> MempershipPayments { get; set; }


        public DbSet<CourseOrder> CoursesOrders { get; set; }
        public DbSet<CoursePayment> CoursesPayments { get; set; }
        public DbSet<GlobalConstants> Constants { get; set; }

        public DbSet<Refund> RefundRequests { get; set; }

        public DbSet<CertifiedMember> CertifiedMembers { get; set; }

        public DbSet<PrerequisiteCourse> PrerequisiteCourses { get; set; }
        public DbSet<HomeBanner> HomeBanners { get; set; }
        public DbSet<Logo> Logos { get; set; }


    }
}
