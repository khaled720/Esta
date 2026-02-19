using System;
using System.Security.Claims;
using ESTA.Areas.Payment.Models;
using ESTA.Areas.Payment.Repository.IRespository;
using ESTA.Helpers;
using ESTA.Models;
using ESTA.Repository.IRepository;
using ESTA.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateAndTime.Workdays;

namespace ESTA.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IStringLocalizer<SharedResource> localizer;
        private readonly IUnitOfWork appRep;
        private readonly IWebHostEnvironment hostEnvironment;
        private readonly UserManager<User> userManager;

        public UserController(IStringLocalizer<SharedResource> localizer,
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

            await appRep.UsersCoursesRep.RemovePaylaterUsersExceeded3days();
            await appRep.SaveChangesAsync();

            var ExpiryMonth = await appRep.ConstantsRep.getMempershipExpiryMonth();

            var Today = DateTime.Today;
            var days = DateTime.DaysInMonth(Today.Year, ExpiryMonth);
            var ExpiryDate = new DateTime(Today.Year, ExpiryMonth, days);

            var CurrentUser = await userManager.GetUserAsync(User);
            var PFP = appRep.ImageRep.GetUserProfilePic(CurrentUser.Id);
            var userCourses = CurrentUser.Courses;
            var country = appRep.CountriesRep.GetCountry(CurrentUser.Country);

            string CountryName;
            if (country != null)
                CountryName = Thread.CurrentThread.CurrentCulture.Name == "ar" ? country.NameAr : country.NameEn;
            else
                CountryName = CurrentUser.Country;

            ViewProfile profile = new()
            {
                MempershipDaysToEnd = (ExpiryDate - Today).Days,
                Id = CurrentUser.Id,
                FullNameAr = CurrentUser.FullNameAr,
                FullName = CurrentUser.FullName,
                Email = CurrentUser.Email,
                Birthdate = CurrentUser.Birthdate,
                Country = CountryName,
                Job = CurrentUser.Job,
                UserId = CurrentUser.NationalCardID ?? CurrentUser.Passport ?? "",
                MembershipNumber = CurrentUser.MembershipNumber,
                MobilePhone = CurrentUser.MobilePhone,
                ProfilePic = PFP != null ? PFP.Path : Constants.DefaultPFP,
                IsMempershipPaid = CurrentUser.IsMempershipPaid,
                Visible = CurrentUser.VisibleProfile,
                IsModerator = await userManager.IsInRoleAsync(CurrentUser, "Moderator"),
                CoursesCount = userCourses != null ? userCourses.Count() : 0,
                FinishedCourses = userCourses != null ? userCourses.Where(x => x.StateId == 3 || x.StateId == 5).Count() : 0,
                ForumsCount = appRep.ForumRep.GetSpecificForumByLevelId(CurrentUser.LevelId).Count(),
            };

            ViewBag.ExpiryMonth = ExpiryMonth;
            return View(profile);
        }
        [HttpPost]
        public async Task<IActionResult> UploadPfpAsync(IFormFile Photo)
        {
            var CurrentUser = await userManager.GetUserAsync(User);
            var PFP = appRep.ImageRep.GetUserProfilePic(CurrentUser.Id);
            await appRep.ImageRep.RemoveImageByTypeAsync(4, CurrentUser.Id);
            var SavePath = hostEnvironment.WebRootPath + Constants.ProfilePicturesImagesSavingPath;

            var PhotoName = await FileUpload.SavePhotoAsync(
                Photo,
                CurrentUser.FullName,
                SavePath
            );

            var userImages = new UserImage() { TypeId = 4, Path = Constants.ProfilePicturesImagesSavingPath + PhotoName, UserId = CurrentUser.Id };
            var res = await appRep.ImageRep.AddImages(userImages);

            if (res)
            {
                await appRep.SaveChangesAsync();

                if (PFP != null)
                {
                    try
                    {
                        System.IO.File.Delete(hostEnvironment.WebRootPath + PFP.Path);
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
            return Json(true);
        }

        [HttpPost]
        public async Task<IActionResult> ProfileVisibilityAsync(string visible)
        {
            var CurrentUser = await userManager.GetUserAsync(User);
            CurrentUser.VisibleProfile = bool.Parse(visible.ToLower());

            // Apply the changes if any to the db
            await userManager.UpdateAsync(CurrentUser);

            return Json(true);
        }

        public async Task<IActionResult> Courses()
        {

            // user corses shuild be lodd her
            //User Id Must be Dynamic
            var courses = await appRep.UserRep.GetMyCourses(User.FindFirstValue(ClaimTypes.NameIdentifier));

            return View(courses);
        }





        //enroll & pay later
        public async Task<IActionResult> EnrollCourse(int Id, int level)
        {

            if (User != null && User.Identity.IsAuthenticated)
            {
                if (!await appRep.UserRep.IsUserMempershipPaid(User.FindFirstValue(ClaimTypes.NameIdentifier)))
                {

                    return View("_Info", new Info(localizer.GetString("cannotenroll"), localizer.GetString("membershipNotPaid")));

                }

                var course = await appRep.CoursesRep.GetCourse(Id);
                var applicants = await appRep.CoursesRep.GetEnrolledUsersInCourseLength(Id);
                if (course.MaxAllowedMembersCount > applicants)
                {

                    if (course.StartDate != null && (course.StartDate - DateTime.Now).Value.Days > 10)
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
                            return View("_Info", new Info(localizer.GetString("cannotenroll"), localizer.GetString("preqnotComplete")));
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

                        return View("_Info", new Info(localizer.GetString("Pay Later"), localizer.GetString("paylaterinfo")));

                        //return RedirectToAction("profile");

                    }
                    else
                    {
                        return View("_Info", new Info(localizer.GetString("cannotenroll"), localizer.GetString("coursewillstart")));
                    }



                }
                else
                {
                    return View("_Info", new Info(localizer.GetString("cannotenroll"), localizer.GetString("coursecomplete")));
                }
            }
            else
            {
                return Redirect("Account/Login");
            }
        }

        //course Id Enroll + pay now
        public async Task<IActionResult> PayEnrollCourse(int Id, int level)
        {

            if (!await appRep.UserRep.IsUserMempershipPaid(User.FindFirstValue(ClaimTypes.NameIdentifier)))
            {

                return View("_Info", new Info(localizer.GetString("cannotenroll"), localizer.GetString("membershipNotPaid")));

            }
            if (await appRep.UserRep.IsForeignUser(User.FindFirstValue(ClaimTypes.NameIdentifier)))
            {

                return View("_Info", new Info(localizer.GetString("cannotenroll"), localizer.GetString("foreignPay")));

            }
            var course = await appRep.CoursesRep.GetCourse(Id);
            var applicants = await appRep.CoursesRep.GetEnrolledUsersInCourseLength(Id);
            if (course.MaxAllowedMembersCount > applicants)
            {
                try
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
                        return View("_Info", new Info(localizer.GetString("cannotenroll"), localizer.GetString("preqnotComplete")));
                    }


                    CourseOrder order = new CourseOrder();
                    int length = await appRep.CourseOrdersRep.GetMaxId();
                    order.OrderNumber = "CO-" + (length + 100);
                    order.BuildOrder(course);

                    order.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                    TempData["myData"] = JsonConvert.SerializeObject(order, new JsonSerializerSettings() { ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore });



                    return RedirectToAction("ConfirmCourseOrder", "Orders",

                        new
                        {
                            area = "Payment"
                        });
                }
                catch (Exception e)
                {
                    return View("_Info", new Info(localizer.GetString("cannotenroll") + " !", localizer.GetString("coursecomplete")));


                }



            }
            else
            {
                return View("_Info", new Info(localizer.GetString("cannotenroll"), localizer.GetString("coursecomplete")));
            }





        }
        public IActionResult CourseEnrolled()
        {



            return View();
        }




    }
}
