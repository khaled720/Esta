using ESTA.Models;
using ESTA.Repository;
using ESTA.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESTA.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize("RequireAdminRole")]
    public class ResultsController : Controller
	{
		private readonly IUnitOfWork uow;

		public ResultsController(IUnitOfWork uow)
		{
			this.uow = uow;
		}
		public IActionResult Index(int courseId)
		{
			uow.UsersCoursesRep.GetAllUsersEnrolledinCourse(courseId);




            return View();
		}

        public async Task<IActionResult> AddResult(int courseId,string userId)
        {
      var usercourse=  await    uow.UsersCoursesRep.GetUserCourse(courseId,userId);




            return View(usercourse);
        }
        [HttpPost]
        public async Task<IActionResult>  AddResult(UserCourse userCourse)
        {
        await    uow.UsersCoursesRep.UpdateUserCourseResult(userCourse.CourseId,userCourse.UserId,userCourse.Grade);
           await uow.SaveChangesAsync();



            return RedirectToAction("CourseInfo", "Courses", new { area = "Admin", id = userCourse.CourseId });

        }
        //public IActionResult Index(int courseId)
        //{
        //    uow.UsersCoursesRep.UpdateUserCourseResult(courseId,userId,grade);




        //    return View();
        //}










    }
}
