using ESTA.Areas.Admin.Models;
using ESTA.Areas.Admin.ViewModels;
using ESTA.Helpers;
using ESTA.Models;
using ESTA.Repository.IRepository;
using ESTA.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace ESTA.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CoursesController : Controller
    {
        private readonly IUnitOfWork appRep;
        private readonly IWebHostEnvironment hostEnvironment;

        public CoursesController(IUnitOfWork appRep, IWebHostEnvironment hostEnvironment)
        {
            this.appRep = appRep;
            this.hostEnvironment = hostEnvironment;
        }






        [Authorize("RequireAdminRole")]
        [HttpGet]
        public async Task<IActionResult> AssignCoursePrerequisite(int cId)
        {
            var courses = (List<Course>)await appRep.CoursesRep.GetAllCourses();
          
            PrerequisiteCourseViewModel prerequisite = new();
            prerequisite.PreCourses = new List<PreCourse>();
            prerequisite.MainCourse = courses.Find(y=>y.Id==cId);
              courses.Remove(prerequisite.MainCourse);

            foreach (var course in courses)
            {
                var isPre =await appRep.CoursesRep.IsPrerequisiteCourse(prerequisite.MainCourse.Id,course.Id);
                prerequisite.PreCourses.Add(new PreCourse() { course=course,isPrerequisite=isPre});
            }


            return View(prerequisite);
        }


        [HttpPost]
        public async Task<IActionResult> AssignCoursePrerequisite(PrerequisiteCourseViewModel prerequisite)
        {

            var precourses = new List<PrerequisiteCourse>();

            foreach (var item in prerequisite.PreCourses)
            {
                if (item.isPrerequisite)
                {
                    precourses.Add(new PrerequisiteCourse() { MainCourseId = prerequisite.MainCourse.Id, PrerequisiteCourseId = item.course.Id });
                }
            }

            await appRep.CoursesRep.AddPrerequisiteCourses(precourses);
            await appRep.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [Authorize("RequireAdminRole")]
        [HttpGet]
        public async Task<IActionResult> Index(PagerViewModel<Course> pagerViewModel,int page=1)
        {
            //if (pagerViewModel.lengthOfFullList == 0) 
            //{ 
            pagerViewModel.CurrentPage = page;
            var courses =(List<Course>)await appRep.CoursesRep.GetAllCourses();
            
            //pagerViewModel.lengthOfFullList = courses.Count;
            //}

            pagerViewModel.Update(courses);

         



        //    var courseList = appRep.GetPaginatedList<Course>(currentPage,pageSize);




            return View(pagerViewModel);
        }


        //[Authorize("RequireAdminRole")]
        //[HttpPost]
        //public IActionResult Index(PagerViewModel<Course> pagerViewModel)
        //{
        //  //  List<Course> courses = (List<Course>)await appRep.CoursesRep.GetAllCourses();







        //    return View(pagerViewModel);
        //}



        [Authorize("RequireAdminRole")]
        [HttpGet]
        public async Task<IActionResult> AddCourse()
        {
            var clvm = new CourseLevelsViewModel();
            clvm.course = new Course();
            clvm.Levels = (List<Level>?)await appRep.LevelRep.GetAllLevels();
            if (clvm.Levels == null)
            {
                clvm.Levels = new List<Level>();
            }

            return View(clvm);
        }

        [Authorize("RequireAdminRole")]
        [HttpPost]
        public async Task<IActionResult> AddCourse(CourseLevelsViewModel clvm)
        {
            if (ModelState.IsValid)
            {
                if (clvm.course != null)
                {
                    try
                    {
                        if (clvm.ImgFile != null)
                        {
                            var ImageName = clvm.course.Title;
                            var SavePath = hostEnvironment.WebRootPath + "/Images/Courses/";
                            var GeneratedFileName = await FileUpload.SavePhotoAsync(
                                clvm.ImgFile,
                                ImageName,
                                SavePath
                            );
                            if (GeneratedFileName != "")
                            {
                                clvm.course.PhotoPath = "/Images/Courses/" + GeneratedFileName;
                            }
                            else
                            {
                                clvm.course.PhotoPath = "/Images/Courses/Learn.jpg";
                            }
                            ///clvm.course.PhotoPath = "/Images/Courses/Learn.jpg";
                        }
                    }
                    catch (Exception)
                    {
                        clvm.course.PhotoPath = "/Images/Courses/Learn.jpg";
                    }

                    await appRep.CoursesRep.AddCourse(clvm.course);
                    clvm.course = new Course();
                }

                if (await appRep.SaveChangesAsync())
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    clvm.Levels = (List<Level>?)await appRep.LevelRep.GetAllLevels();
                    if (clvm.Levels == null)
                    {
                        clvm.Levels = new List<Level>();
                    }
                    return View(clvm);
                }
            }
            else
            {
                clvm.Levels = (List<Level>?)await appRep.LevelRep.GetAllLevels();
                if (clvm.Levels == null)
                {
                    clvm.Levels = new List<Level>();
                }

            
                return View(clvm);
            }
        }

        [Authorize("RequireAdminRole")]
        [HttpGet]
        public async Task<IActionResult> EditCourse(int id)
        {
            var clvm = new CourseLevelsViewModel();
            clvm.course = await appRep.CoursesRep.GetCourse(id);
            clvm.Levels = (List<Level>?)await appRep.LevelRep.GetAllLevels();
            if (clvm.Levels == null)
            {
                clvm.Levels = new List<Level>();
            }

            return View(clvm);
        }

        [Authorize("RequireAdminRole")]
        [HttpPost]
        public async Task<IActionResult> EditCourse(CourseLevelsViewModel clvm)
        {
            if (ModelState.IsValid)
            {
                var isEdited = false;
                if (clvm.course != null)
                { //edit should happen here
                    try
                    {
                        if (clvm.ImgFile != null)
                        {
                            var ImageName = clvm.course.Title;
                            var SavePath = hostEnvironment.WebRootPath + "/Images/Courses/";
                            var GeneratedFileName = await FileUpload.SavePhotoAsync(
                                clvm.ImgFile,
                                ImageName,
                                SavePath
                            );
                            if (GeneratedFileName != "")
                            {
                                clvm.course.PhotoPath = "/Images/Courses/" + GeneratedFileName;
                            }
                        }
                    }
                    catch (Exception ex) { }

                    isEdited = await appRep.CoursesRep.EditCourse(clvm.course);

                    //   clvm.course = new Course();
                }

                if (isEdited)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    clvm.Levels = (List<Level>?)await appRep.LevelRep.GetAllLevels();
                    if (clvm.Levels == null)
                    {
                        clvm.Levels = new List<Level>();
                    }
                    return View(clvm);
                }
            }
            else
            {
                clvm.Levels = (List<Level>?)await appRep.LevelRep.GetAllLevels();
                if (clvm.Levels == null)
                {
                    clvm.Levels = new List<Level>();
                }
                return View(clvm);
            }
        }

        [Authorize("RequireAdminRole")]
        [HttpGet]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            if (appRep.CoursesRep.DeleteCourse(id))
            {
                try
                {
                    await appRep.SaveChangesAsync();
                }
                catch (Exception)
                {
                    //do nothing
                }
            }

            return RedirectToAction("Index");
        }

        [Authorize("RequireAdminRole")]
        [HttpGet]
        public async Task<IActionResult> CourseInfo(int id)
        {
            try
            {
                var course = await appRep.CoursesRep.GetCourse(id);
                List<UserCourse> users = await appRep.CoursesRep.GetEnrolledUsersInCourse(id);
                AdminCourseInfoViewModel acivm = new AdminCourseInfoViewModel();
                acivm.course = course;
                acivm.Users = users;
                return View(acivm);
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public  IActionResult UpdateCoursePaymentStatuse(int cid,string uid,bool paymentstate)
        {
            try
            {
                appRep.UsersCoursesRep.UpdateUserCoursePaymentStatus(uid, cid, paymentstate);
                appRep.SaveChangesAsync().Wait();

                return RedirectToAction("CourseInfo", new {id=cid });
            }
            catch (Exception e )
            {
                return RedirectToAction("CourseInfo", new { id = cid });
            }
        }



    }
}
