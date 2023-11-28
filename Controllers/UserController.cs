using System;
using System.Security.Claims;
using ESTA.Areas.Payment.Models;
using ESTA.Areas.Payment.Repository.IRespository;
using ESTA.Helpers;
using ESTA.Models;
using ESTA.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;

namespace ESTA.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IStringLocalizer<SharedResource> localizer;
        private readonly IUnitOfWork appRep;
        private readonly IWebHostEnvironment hostEnvironment;
        private readonly UserManager<User> userManager;

        public UserController(IStringLocalizer<SharedResource> localizer ,
            IUnitOfWork appRep, IWebHostEnvironment hostEnvironment, UserManager<User> userManager)
        {
            this.localizer = localizer;
            this.appRep = appRep;
            this.hostEnvironment = hostEnvironment;
            this.userManager = userManager;
        }

  
        public async Task<IActionResult> Profile()
        {

            //  await appRep.UserRep.GetMyCourses(User.FindFirstValue(ClaimTypes.NameIdentifier));
               
               await   appRep.UsersCoursesRep.RemovePaylaterUsersExceeded3days();
               await appRep.SaveChangesAsync();
            
            ViewBag.ExpiryMonth= await appRep.ConstantsRep.getMempershipExpiryMonth();
      
            
            return View();
        }

        public async Task<IActionResult> Courses()
        {
  
            // user corses shuild be lodd her
            //User Id Must be Dynamic
          var courses=    await  appRep.UserRep.GetMyCourses(User.FindFirstValue(ClaimTypes.NameIdentifier));

            return View(courses);
        }





        //enroll & pay later
        public async Task<IActionResult> EnrollCourse(int Id,int level)
        {

            if (User!=null&&User.Identity.IsAuthenticated)
            {






                if (!await appRep.UserRep.IsUserMempershipPaid(User.FindFirstValue(ClaimTypes.NameIdentifier))) 
                {

                    return View("_Info", new Info(localizer.GetString("cannotenroll"), "Can not Enroll. Mempership Is Not Paid "));

                }
        
                var course = await appRep.CoursesRep.GetCourse(Id);
                var applicants = await appRep.CoursesRep.GetEnrolledUsersInCourseLength(Id);
                if (course.MaxAllowedMembersCount > applicants)
                {


                    if (course.StartDate != null && (course.StartDate - DateTime.Now).Value.Days> 10)
                    {




                        var PrerequisiteCourses = await appRep.CoursesRep.GetPrerequisiteCourses(Id);
                        var userCourses = await appRep.UserRep.GetMyCourses(User.FindFirstValue(ClaimTypes.NameIdentifier));
                        int MatchCounter = 0;
                        for (int i = 0; i < PrerequisiteCourses.Count(); i++)
                        {
                            var isfound = userCourses.Where(y => y.CourseId ==
                                   PrerequisiteCourses[i].PrerequisiteCourseId).Any();

                            if (isfound)
                            {
                                //PrerequisiteCourses[i].isPassed = true;
                                MatchCounter++;
                            }
                        }
                        if (MatchCounter != PrerequisiteCourses.Count())
                        {
                            return View("_Info", new Info(localizer.GetString("cannotenroll"), "Can not Enroll.Prerequisite Courses Not Completed"));
                        }



                        await appRep.UserRep.EnrollCourse(1, Id, User.FindFirstValue(ClaimTypes.NameIdentifier), false);
                        if (level < 4)
                        {
                            var usr = await userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
                            if (usr.LevelId < level)
                            {
                                usr.LevelId = level;
                                await userManager.UpdateAsync(usr);
                            }
                        }
                        await appRep.SaveChangesAsync();


                        return View();

                    }
                    else
                    {
                        return View("_Info", new Info(localizer.GetString("cannotenroll"), "Can not Enroll. Course Will start in less than 10 days" ));
                    }



                }
                else {
                return View("_Info", new Info(localizer.GetString("cannotenroll"), localizer.GetString("coursecomplete") ));
                }




            }
            else
            {
                return Redirect("Account/Login");
            }
        }

        //course Id Enroll + pay now
        public async Task<IActionResult> PayEnrollCourse(int Id,int level)
        {

     


            if (!await appRep.UserRep.IsUserMempershipPaid(User.FindFirstValue(ClaimTypes.NameIdentifier)))
            {

                return View("_Info", new Info(localizer.GetString("cannotenroll"), "Can not Enroll. Mempership Is Not Paid "));

            }
            if (await appRep.UserRep.IsForeignUser(User.FindFirstValue(ClaimTypes.NameIdentifier)))
            {

                return View("_Info", new Info(localizer.GetString("cannotenroll"), "Can not Enroll. Foreign Users Can not pay online "));

            }
            var course=await  appRep.CoursesRep.GetCourse(Id);
        var applicants= await    appRep.CoursesRep.GetEnrolledUsersInCourseLength(Id);
            if (course.MaxAllowedMembersCount > applicants) 
            {
                var PrerequisiteCourses = await appRep.CoursesRep.GetPrerequisiteCourses(Id);
                var userCourses = await appRep.UserRep.GetMyCourses(User.FindFirstValue(ClaimTypes.NameIdentifier));
                int MatchCounter = 0;
                for (int i = 0; i < PrerequisiteCourses.Count(); i++)
                {
                    var isfound = userCourses.Where(y => y.CourseId ==
                           PrerequisiteCourses[i].PrerequisiteCourseId).Any();

                    if (isfound)
                    {
                        //PrerequisiteCourses[i].isPassed = true;
                        MatchCounter++;
                    }
                }
                if (MatchCounter != PrerequisiteCourses.Count())
                {
                    return View("_Info", new Info(localizer.GetString("cannotenroll"), "Can not Enroll.Prerequisite Courses Not Completed"));
                }


                CourseOrder order = new CourseOrder();
           int length =await appRep.CourseOrdersRep.GetMaxId();
            order.OrderNumber = "CO-" +( length+100);
            order.BuildOrder(course);
         
            order.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            TempData["myData"] = JsonConvert.SerializeObject(order);
      


            return RedirectToAction("ConfirmCourseOrder", "Orders",

                new {
                    area = "Payment"
                });
            

            }
            else
            {
                return View("_Info",new Info(localizer.GetString("cannotenroll"), localizer.GetString("coursecomplete")) );
            }
         

          
         

        }
        public IActionResult CourseEnrolled()
        {
           


            return View();
        }


        

    }
}
