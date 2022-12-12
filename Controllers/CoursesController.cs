using ESTA.Models;
using ESTA.Repository.IRepository;
using ESTA.ViewModels;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ESTA.Controllers
{

    public class CoursesController : Controller
    {
        private readonly IAppRep appRep;

        public CoursesController( IAppRep appRep)
        {
            this.appRep = appRep;
        }
        public async Task<IActionResult> Index()
        {
        List<Course> courses= (List<Course>)await appRep.CoursesRep.GetAllCourses();
            return View(courses);
        }

        [HttpGet]
        public  async Task<IActionResult> AddCourse()
        {
            var clvm =new CourseLevelsViewModel();
            clvm.course = new Course();
            clvm.Levels = (List<Level>?) await appRep.LevelRep.GetAllLevels();
            if (clvm.Levels==null){clvm.Levels = new List<Level>();}
          

            return View(clvm);
        }

        [HttpPost]
        public async Task<IActionResult> AddCourse(CourseLevelsViewModel clvm)
        {
           
            if (ModelState.IsValid) {if (clvm.course != null) {
                    if (clvm.ImgFile != null) 
                    {
                   //     clvm.course.SavePhoto(clvm.ImgFile);
                    
                    }
             await appRep.CoursesRep.AddCourse(clvm.course);
                    clvm.course = new Course();
 
            }
          
            var isAdded = await appRep.SaveAsync();
            if (isAdded)
            {
            
                return RedirectToAction("Index");

            }
            else {
                    clvm.Levels = (List<Level>?)await appRep.LevelRep.GetAllLevels();
                    if (clvm.Levels == null) { clvm.Levels = new List<Level>(); }
                    return View(clvm);
            }

            }
            else
            {
                clvm.Levels = (List<Level>?)await appRep.LevelRep.GetAllLevels();
                if (clvm.Levels == null) { clvm.Levels = new List<Level>(); }
                return View(clvm);
            }


        }



        [HttpGet]
        public async Task<IActionResult> EditCourse(int id)
        {
            var clvm = new CourseLevelsViewModel();
            clvm.course =await appRep.CoursesRep.GetCourse(id);
            clvm.Levels = (List<Level>?)await appRep.LevelRep.GetAllLevels();
            if (clvm.Levels == null) { clvm.Levels = new List<Level>(); }


            return View(clvm);
        }


        [HttpPost]
        public async Task<IActionResult> EditCourse(CourseLevelsViewModel clvm)
        {
            if (ModelState.IsValid)
            {
                var isAdded=false;
                if (clvm.course != null)
                {//edit should happen here
                isAdded =     await appRep.CoursesRep.EditCourse(clvm.course);
                  
                 //   clvm.course = new Course();

                }

               
                if (isAdded)
                {

                    return RedirectToAction("Index");

                }
                else
                {
                    clvm.Levels = (List<Level>?)await appRep.LevelRep.GetAllLevels();
                    if (clvm.Levels == null) { clvm.Levels = new List<Level>(); }
                    return View(clvm);
                }

            }
            else
            {
                clvm.Levels = (List<Level>?)await appRep.LevelRep.GetAllLevels();
                if (clvm.Levels == null) { clvm.Levels = new List<Level>(); }
                return View(clvm);
            }


        }



        [HttpGet]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var clvm = new CourseLevelsViewModel();
            clvm.course =await appRep.CoursesRep.GetCourse(id);
            clvm.Levels = (List<Level>?)await appRep.LevelRep.GetAllLevels();
            if (clvm.Levels == null) { clvm.Levels = new List<Level>(); }


            return View(clvm);
        }




    }
}
