using System.Text.Encodings.Web;
using ESTA.Models;
using ESTA.Repository;
using ESTA.Repository.IRepository;
using ESTA.ViewModels;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ESTA.Controllers
{

    [Route("Admin/{controller}/{action=Index}/{id?}")]
    public class CoursesController : Controller
    {
        private readonly IAppRep appRep;
        private readonly IWebHostEnvironment hostEnvironment;


        public CoursesController(IAppRep appRep, IWebHostEnvironment hostEnvironment)
        {
            this.appRep = appRep;
            this.hostEnvironment = hostEnvironment;

        }

        public async Task<IActionResult> Index()
        {

            List<Course> courses = (List<Course>)await appRep.CoursesRep.GetAllCourses();
            return View(courses);
        }

        [HttpGet]
        public async Task<IActionResult> AddCourse()
        {
            var clvm = new CourseLevelsViewModel();
            clvm.course = new Course();
            clvm.Levels = (List<Level>?)
                await appRep.LevelRep.GetAllLevels();
            if (clvm.Levels == null)
            {
                clvm.Levels = new List<Level>();
            }

            return View(clvm);
        }

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
                            clvm.course.PhotoPath = "/Images/Courses/Learn.jpg";
                        }
                    }
                    catch (Exception)
                    {
                        clvm.course.PhotoPath = "/Images/Courses/Learn.jpg";
                    }

                    await appRep.CoursesRep.AddCourse(clvm.course);
                    clvm.course = new Course();
                }

                if (await appRep.SaveAsync())
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
                    catch (Exception) { }

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

        [HttpGet]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            if ( appRep.CoursesRep.DeleteCourse(id))
            {
                try
                {
 await  appRep.SaveAsync();
                }
                catch (Exception)
                {
//do nothing
                } }
           
        

            return RedirectToAction("Index");
        }




        [HttpGet]
        public async Task<IActionResult> CourseInfo(int id)
        {

            try
            {
        var course=await appRep.CoursesRep.GetCourse(id);

            return View(course);
            }
            catch (Exception)
            {

                return RedirectToAction("Index");
            }
    

         
        }






    }






    [Route("api/[controller]")]
    public class CoursesApiController : ControllerBase
    {

        private readonly IAppRep appRep;
        private readonly IWebHostEnvironment hostEnvironment;

        public CoursesApiController(IAppRep appRep, IWebHostEnvironment hostEnvironment)
        {
            this.appRep = appRep;
            this.hostEnvironment = hostEnvironment;
        }

        [Route("SearchForCourse")]
        [HttpPost]
        public async Task<string> SearchForCourse(string name)
        {
            return await appRep.CoursesRep.SearchForCourse(name);

        }
    }
}
