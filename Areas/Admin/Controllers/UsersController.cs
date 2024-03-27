using System.Net.Mime;
using System.Security.Claims;
using AutoMapper;
using AutoMapper.Configuration.Conventions;
using ESTA.Helpers;
using ESTA.Models;
using ESTA.Repository.IRepository;
using ESTA.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;

namespace ESTA.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize("RequireAdminRole")]
    public class UsersController : Controller
    {
        private readonly IUnitOfWork appRep;
        private readonly IWebHostEnvironment hostEnvironment;
        private readonly UserManager<User> userManager;


        public IMapper mapper { get; }

        public UsersController(
            IUnitOfWork appRep,
            IWebHostEnvironment hostEnvironment,
            UserManager<User> userManager,
            IMapper mapper

        )
        {
            this.appRep = appRep;
            this.hostEnvironment = hostEnvironment;
            this.userManager = userManager;
            this.mapper = mapper;

        }

        public async Task<IActionResult> Index(PagerViewModel<User> pagerViewModel, int page = 1, string query = "", int type = 1/*1 all users 2 old 3 new members*/)
        {

            var allUsers = (List<User>)await appRep.UserRep.GetAllUsers();

            //list of users only not admin or moderator
            List<User> users;
            switch (type)
            {
                case 1:
                    users = allUsers.Where(y => userManager.IsInRoleAsync(y, "User")
                          .GetAwaiter().GetResult() == true).ToList();
                    break;
                case 2:
                    users = allUsers.Where(y => userManager.IsInRoleAsync(y, "User")
                          .GetAwaiter().GetResult() == true).Where(y => !string.IsNullOrEmpty(y.MembershipNumber)).ToList();
                    break;
                case 3:
                    users = allUsers.Where(y => userManager.IsInRoleAsync(y, "User")
                          .GetAwaiter().GetResult() == true).Where(y => string.IsNullOrEmpty(y.MembershipNumber)).ToList();
                    break;

                case 4:
                    users = allUsers.Where(y => userManager.IsInRoleAsync(y, "User")
                          .GetAwaiter().GetResult() == true).Where(y => y.Country == "Egypt").ToList();
                    break;
                case 5:
                    users = allUsers.Where(y => userManager.IsInRoleAsync(y, "User")
                          .GetAwaiter().GetResult() == true).Where(y => y.Country != "Egypt").ToList();
                    break;

                default:

                    users = allUsers.Where(y => userManager.IsInRoleAsync(y, "User")
                          .GetAwaiter().GetResult() == true).ToList();
                    break;

            }


            pagerViewModel.CurrentPage = page;

            pagerViewModel.Update(users);

            ViewBag.type = type;

            return View(pagerViewModel);


        }

        public async Task<IActionResult> EditMempership(string id, bool isPaid)
        {
            if (isPaid)
            {
                await appRep.UserRep.PayMempership(id);
            }
            else
            {
                await appRep.UserRep.RevokeMempershipPayment(id);
            }


            await appRep.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> EditApproval(string id, bool isApproved)
        {
            await appRep.UserRep.EditUserApproval(id, isApproved);
            await appRep.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> DeleteUser(string userId)
        {
            await appRep.UserRep.DeleteUser(userId);
            await appRep.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> EditEmailConfirmation(string id, bool isConfirmed)
        {
            await appRep.UserRep.EditUserEmailConfirmationApproval(id, isConfirmed);
            await appRep.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> EditPersonalInfo(EditUserPersonalInfoViewModel userData)
        {
            try
            {
                var dbUser = await userManager.FindByIdAsync(userData.Id);

                dbUser.FullName = userData.FullName;
                dbUser.HomePhone = userData.HomePhone;
                dbUser.FullNameAr = userData.FullNameAr;
                dbUser.MobilePhone = userData.MobilePhone;
                dbUser.Birthdate = userData.Birthdate;
                dbUser.MembershipNumber = userData.MembershipNumber;

                if (User.IsInRole("Admin"))
                {
                    dbUser.NationalCardID = userData.NationalCardID;

                    dbUser.Email = userData.Email;
                    dbUser.Passport = userData.Passport;
                }

                if (userData.NationalIdImages != null)
                {
                    await appRep.ImageRep.RemoveImageByTypeAsync(1, dbUser.Id);
                    await this.appRep.SaveChangesAsync();
                    foreach (var image in userData.NationalIdImages)
                    {

                        try
                        {
                            var SavePath = hostEnvironment.WebRootPath + Constants.NationalIDsImagesSavingPath;
                            var PhotoName = await FileUpload.SavePhotoAsync(
                             image,
                                dbUser.FullName + " " + dbUser.Id,
                                SavePath
                            );
                            //userData.userImages.Add(new UserImage() { TypeId = 1, Path = Constants.NationalIDsImagesSavingPath + PhotoName, UserId = dbUser.Id });
                            //adding to database
                            await appRep.ImageRep.AddImages(new UserImage() { TypeId = 1, Path = Constants.NationalIDsImagesSavingPath + PhotoName, UserId = dbUser.Id });

                            await this.appRep.SaveChangesAsync();
                        }
                        catch (Exception ex)
                        {

                            throw;
                        }

                    }

                    //     user.NationalIDImagePath = Constants.NationalIDsImagesSavingPath + PhotoName;
                }



                if (userData.PassportImages != null)
                {
                    await appRep.ImageRep.RemoveImageByTypeAsync(2, dbUser.Id);
                    await this.appRep.SaveChangesAsync();
                    foreach (var image in userData.PassportImages)
                    {
                        try
                        {
                            var SavePath = hostEnvironment.WebRootPath + Constants.PassportsImagesSavingPath;
                            var PhotoName = await FileUpload.SavePhotoAsync(
                             image,
                                dbUser.FullName + " " + dbUser.Id,
                                SavePath
                            );
                            //userData.userImages.Add(new UserImage() { TypeId = 2, Path = Constants.PassportsImagesSavingPath + PhotoName, UserId = dbUser.Id });
                            //adding to database
                            await appRep.ImageRep.AddImages(new UserImage() { TypeId = 2, Path = Constants.PassportsImagesSavingPath + PhotoName, UserId = dbUser.Id });

                            await this.appRep.SaveChangesAsync();
                        }
                        catch (Exception)
                        {

                            throw;
                        }

                    }

                    //     user.NationalIDImagePath = Constants.NationalIDsImagesSavingPath + PhotoName;
                }




                await userManager.UpdateAsync(dbUser);

                return RedirectToAction("UserDetails", new { userId = userData.Id });
            }
            catch (Exception)
            {
                return Content("Edit Fail");
            }
        }

        public async Task<IActionResult> EditAddressInfo(EditUserAddressInfoViewModel userData)
        {
            try
            {
                var dbUser = await userManager.FindByIdAsync(userData.Id);

                dbUser.FlatNumber = userData.FlatNumber;
                dbUser.BlockNumber = userData.BlockNumber;
                dbUser.Floor = userData.Floor;
                dbUser.MessagingAddress = userData.MessagingAddress;
                dbUser.City = userData.City;
                dbUser.StreetName = userData.StreetName;
                dbUser.Country = userData.Country;
                dbUser.Hometown = userData.Hometown;
                dbUser.Area = userData.Area;
                dbUser.PostalCode = userData.PostalCode;



                if (User.IsInRole("Admin"))
                {
                    await userManager.UpdateAsync(dbUser);
                }



                return RedirectToAction("UserDetails", new { userId = userData.Id });
            }
            catch (Exception)
            {
                return RedirectToAction("UserDetails", new { userId = userData.Id });
            }
        }

        public async Task<IActionResult> EditWorkInfo(EditUserWorkInfoViewModel userData)
        {
            try
            {
                var dbUser = await userManager.FindByIdAsync(userData.Id);

                dbUser.WorkPhone = userData.WorkPhone;
                dbUser.WorkAddress = userData.WorkAddress;
                dbUser.WorkFax = userData.WorkFax;
                //  dbUser.WorkLeavingDate = userData.WorkLeavingDate;
                // dbUser.WorkLeavingReasons = userData.WorkLeavingReasons;
                dbUser.Company = userData.Company;
                dbUser.Job = userData.Job;




                if (User.IsInRole("Admin"))
                {
                    await userManager.UpdateAsync(dbUser);
                }


                return RedirectToAction("UserDetails", new { userId = userData.Id });
            }
            catch (Exception)
            {
                return RedirectToAction("UserDetails", new { userId = userData.Id });
            }
        }

        public async Task<IActionResult> EditEducationInfo(EditUserEducationInfoViewModel userData)
        {
            try
            {
                var dbUser = await userManager.FindByIdAsync(userData.Id);

                dbUser.AcademicQualification = userData.AcademicQualification;
                dbUser.University = userData.University;
                dbUser.GradutionYear = userData.GradutionYear;
                dbUser.HighStudies = userData.HighStudies;

                if (userData.AcademicQualificationImages != null)
                {
                    await appRep.ImageRep.RemoveImageByTypeAsync(3, dbUser.Id);
                    await this.appRep.SaveChangesAsync();
                    foreach (var image in userData.AcademicQualificationImages)
                    {
                        try
                        {
                            var SavePath = hostEnvironment.WebRootPath + Constants.GraduationCertificateImagesSavingPath;
                            var PhotoName = await FileUpload.SavePhotoAsync(
                             image,
                                dbUser.FullName + " " + dbUser.Id,
                                SavePath
                            );
                            //userData.userImages.Add(new UserImage() { TypeId = 3, Path = Constants.GraduationCertificateImagesSavingPath + PhotoName, UserId = dbUser.Id });
                            //adding to database
                            await appRep.ImageRep.AddImages(new UserImage() { TypeId = 3, Path = Constants.GraduationCertificateImagesSavingPath + PhotoName, UserId = dbUser.Id });

                            await this.appRep.SaveChangesAsync();
                        }
                        catch (Exception ex)
                        {

                            //        throw;
                        }

                    }




                }



                if (User.IsInRole("Admin"))
                {
                    await userManager.UpdateAsync(dbUser);
                }



                return RedirectToAction("UserDetails", new { userId = userData.Id });
            }
            catch (Exception)
            {
                return RedirectToAction("UserDetails", new { userId = userData.Id });
            }
        }

        [HttpGet]
        public IActionResult ResetPassword(string Email)
        {

            return View(new ResetPasswordViewModel() { Email = Email });

        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel rpvm)
        {

            var email = rpvm.Email;

            var user = await userManager.FindByEmailAsync(email);
            if (user != null)
            {
                var token = await userManager.GeneratePasswordResetTokenAsync(user);
                var j = userManager.ResetPasswordAsync(user, token, rpvm.NewPassword);


            }
            return RedirectToAction("Index", "Users", new { area = "Admin" });


        }

        public async Task<IActionResult> UserDetails(string userId)
        {
            try
            {
                var user = await appRep.UserRep.GetUser(userId);

                user.userImages = await appRep.ImageRep.GetUserDocsImages(userId);
                ViewBag.isCertified = await appRep.CertifiedMempersRep.IsCertifiedMember(user.FullName);
                return View(user);
            }
            catch (Exception e)
            {
                return RedirectToAction("Index");
            }

        }

        public async Task<IActionResult> ExportUsersToFileAsync()
        {
            var allUsers = (List<User>)await appRep.UserRep.GetAllUsers();
            List<User> users;

            users = allUsers.Where(y => userManager.IsInRoleAsync(y, "User")
                          .GetAwaiter().GetResult() == true).ToList();

            ExcelPackage Ep = new ExcelPackage();
            ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("ESTA Members");
            Sheet.Cells["A1"].Value = "Full Name";
            Sheet.Cells["B1"].Value = "Full Name Arabic";
            Sheet.Cells["C1"].Value = "Mobile phone";
            Sheet.Cells["D1"].Value = "Home phone";
            Sheet.Cells["E1"].Value = "Email";
            Sheet.Cells["F1"].Value = "City";
            Sheet.Cells["G1"].Value = "Country";
            Sheet.Cells["H1"].Value = "National ID";
            Sheet.Cells["I1"].Value = "Passport";
            Sheet.Cells["J1"].Value = "Mempership number";
            int row = 2;

            foreach (var item in users)
            {
                Sheet.Cells[string.Format("A{0}", row)].Value = item.FullName;
                Sheet.Cells[string.Format("B{0}", row)].Value = item.FullNameAr;
                Sheet.Cells[string.Format("C{0}", row)].Value = item.MobilePhone;
                Sheet.Cells[string.Format("D{0}", row)].Value = item.HomePhone;
                Sheet.Cells[string.Format("E{0}", row)].Value = item.Email;
                Sheet.Cells[string.Format("F{0}", row)].Value = item.City;
                Sheet.Cells[string.Format("G{0}", row)].Value = item.Country;
                Sheet.Cells[string.Format("H{0}", row)].Value = item.NationalCardID;
                Sheet.Cells[string.Format("I{0}", row)].Value = item.Passport;
                Sheet.Cells[string.Format("J{0}", row)].Value = item.MembershipNumber;
                row++;
            }

            Sheet.Cells["A:AZ"].AutoFitColumns();

            return File(Ep.GetAsByteArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ESTA Members.xlsx");
        }
    }
}
