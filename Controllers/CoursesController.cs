using System.Security.Claims;
using System.Text.Encodings.Web;
using ESTA.Areas.Admin.Models;
using ESTA.Helpers;
using ESTA.Models;
using ESTA.Repository;
using ESTA.Repository.IRepository;
using ESTA.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ESTA.Controllers
{
    //  [Route("Admin/{controller}/{action=Index}/{id?}")]
    public class CoursesController : Controller
    {
        private readonly IUnitOfWork appRep;
        private readonly IWebHostEnvironment hostEnvironment;
        private readonly IConfiguration configuration;

        public CoursesController(IUnitOfWork appRep, IWebHostEnvironment hostEnvironment, IConfiguration configuration)
        {
            this.appRep = appRep;
            this.hostEnvironment = hostEnvironment;
            this.configuration = configuration;
        }

        public async Task<IActionResult> OtherCourses()
        {
            List<Course> courses = (List<Course>)await appRep.CoursesRep.GetAllOtherCourses();
            courses.Select(c =>
            {
                c.Description = HtmlHelper.RemoveHTMLTags(c.Description, 50);
                c.DescriptionAr = HtmlHelper.RemoveHTMLTags(c.DescriptionAr, 50);
                return c;
            }).ToList();
            return View(courses);

        }

        public async Task<IActionResult> CetaCourses()
        {
            List<Course> courses = (List<Course>)await appRep.CoursesRep.GetAllCetaCourses();

            courses.Select(c =>
            {
                c.Description = HtmlHelper.RemoveHTMLTags(c.Description, 50);
                c.DescriptionAr = HtmlHelper.RemoveHTMLTags(c.DescriptionAr, 50);
                return c;
            }).ToList();

            // Get CETA content
            var content = appRep.ContentRep.GetContent("CETA");

            ViewCETA viewCETA = new()
            {
                courses = courses,
                CETAContent = content
            };

            return View(viewCETA);
        }

        public async Task<IActionResult> CetaHolders()
        {
            List<CertifiedMember> users = await appRep.CertifiedMempersRep.GetAllMembers();// (List<User>)await appRep.CoursesRep.GetAllCetaHolders();
            return View(users);
        }

        public async Task<IActionResult> CourseDetails(int id)
        {
            try
            {
                await appRep.UsersCoursesRep.RemovePaylaterUsersExceeded3days();
                await appRep.SaveChangesAsync();

                var cdvm = new CourseDetailsViewModel();

                cdvm.course = await appRep.CoursesRep.GetCourse(id);

                cdvm.UsersEnrolledCount = await appRep.CoursesRep.GetEnrolledUsersInCourseLength(id);
                cdvm.isCourseEnrolled = await appRep.CoursesRep.IsCourseEnrolledByUser(id, User.FindFirstValue(ClaimTypes.NameIdentifier));

                cdvm.IsCourseRefunded = await appRep.UsersCoursesRep.IsCourseRefunded(cdvm.course.Id, User.FindFirstValue(ClaimTypes.NameIdentifier));
                cdvm.userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
                cdvm.IsMempershipPaid = await appRep.UserRep.IsUserMempershipPaid(User.FindFirstValue(ClaimTypes.NameIdentifier));

                cdvm.isCompleted = appRep.UsersCoursesRep.IsUserCompletedCourse(id, cdvm.userid);

                cdvm.PrerequisiteCourses = await appRep.CoursesRep.GetPrerequisiteCourses(cdvm.course.Id);
                var userCourses = await appRep.UserRep.GetMyCourses(cdvm.userid);
                int MatchCounter = 0;
                for (int i = 0; i < cdvm.PrerequisiteCourses.Count; i++)
                {
                    //search this course, only if the state is 3 as in completed.
                    var isfound = userCourses.Where(y => y.CourseId ==
                            cdvm.PrerequisiteCourses[i].PrerequisiteCourseId && y.StateId == 3).Any();

                    if (isfound)
                    {
                        cdvm.PrerequisiteCourses[i].isPassed = true;
                        MatchCounter++;
                    }
                }
                if (!String.IsNullOrEmpty(cdvm.userid) && MatchCounter == cdvm.PrerequisiteCourses.Count)
                {
                    cdvm.IsPrerequisiteCoursesPassed = true;
                }

                return View(cdvm);
            }
            catch (Exception)
            {

                return View();
            }

        }


    }



}
