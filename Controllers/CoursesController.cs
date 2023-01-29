﻿using System.Text.Encodings.Web;
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

        public CoursesController(IUnitOfWork appRep, IWebHostEnvironment hostEnvironment)
        {
            this.appRep = appRep;
            this.hostEnvironment = hostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            List<Course> courses = (List<Course>)await appRep.CoursesRep.GetAllCourses();
         //   PaginatedList<Course> dd;
            return View(courses);
        }

        public async Task<IActionResult> OtherCourses()
        {
            List<Course> courses = (List<Course>)await appRep.CoursesRep.GetAllOtherCourses();
            return View(courses);
        }

        public async Task<IActionResult> CetaCourses()
        {
            List<Course> courses = (List<Course>)await appRep.CoursesRep.GetAllCetaCourses();
            return View(courses);
        }

        public async Task<IActionResult> CetaHolders()
        {
            List<User> users = (List<User>)await appRep.CoursesRep.GetAllCetaHolders();
            return View(users);
        }

        public async Task<IActionResult> CourseDetails(int id)
        {
            var course = await appRep.CoursesRep.GetCourse(id);

            return View(course);
        }

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
    }

    public class EmailClass
    {
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Name { get; set; }
        public string Message { get; set; }
    }

    [Route("api/[controller]")]
    public class EndpointController : ControllerBase
    {
        private readonly IUnitOfWork appRep;
        private readonly IWebHostEnvironment hostEnvironment;

        public EndpointController(IUnitOfWork appRep, IWebHostEnvironment hostEnvironment)
        {
            this.appRep = appRep;
            this.hostEnvironment = hostEnvironment;
        }

        [HttpGet]
        public async Task<IEnumerable<Course>> Get()
        {
             var courses=await appRep.CoursesRep.GetAllCourses();
            return courses;
        }

        [HttpPost]
        public bool SendEmail(EmailClass email)
        {
            return EmailSender.Send_Mail(
                "k900000001@gmail.com",
                email.Message,
                email.Subject,
                "Contact"
            );
        }
    }
}
