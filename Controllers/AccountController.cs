using System.Diagnostics.Eventing.Reader;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security.Claims;
//using AspNetCore;
using ESTA.API_Controllers;
using ESTA.Helpers;
using ESTA.Models;
using ESTA.Repository.IRepository;
using ESTA.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Localization;

namespace ESTA.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<User> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<User> userManager;
        private readonly IUnitOfWork appRep;
        private readonly IWebHostEnvironment hostEnvironment;
        private readonly IStringLocalizer<SharedResource> localizer;

        public AccountController(
            SignInManager<User> _signInManager,
            RoleManager<IdentityRole> roleManager,
            UserManager<User> userManager,
            IUnitOfWork appRep,
            IWebHostEnvironment hostEnvironment
            ,IStringLocalizer<SharedResource> localizer
        )
        {
            signInManager = _signInManager;
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.appRep = appRep;
            this.hostEnvironment = hostEnvironment;
            this.localizer = localizer;
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (signInManager.IsSignedIn(User))
           return     RedirectToAction("Index", "Home");

            return View();
        }

        //[HttpPost]
        //public async Task<IActionResult> Login(LoginViewModel LoginModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var result = await signInManager.PasswordSignInAsync(
        //            LoginModel.Email,
        //            LoginModel.Password,
        //            false,
        //            lockoutOnFailure: false
        //        );

        //        if (result.Succeeded)
        //        {
        //            var user = await userManager.FindByEmailAsync(LoginModel.Email);

        //            if (user.IsDeleted) { 
        //                 return View(
        //                    "ConfirmEmail",
        //                    new ErrorViewModel
        //                    {
        //                        Title = "Account Deleted",
        //                        Description =
        //                            "We are sorry to inform you that your account was deleted for violating our conditions&terms. for more info contact support team."
        //                    }
        //                );
        //            }


        //            if ( user.IsApproved == true)
        //            {
        //                if (await userManager.IsInRoleAsync(user, "Admin"))
        //                {
        //                    return RedirectToAction("index", "courses", new { area = "Admin" });
        //                }
        //                else if (await userManager.IsInRoleAsync(user, "Moderator"))
        //                {
        //                    return RedirectToAction("index", "Forums", new { area = "Admin" });
        //                }
        //                else
        //                {
        //                    return RedirectToAction("profile", "user");
        //                }
        //            }
        //            else
        //            {
        //                return View(
        //                    "ConfirmEmail",
        //                    new ErrorViewModel
        //                    {
        //                        Title = "Waiting for Approval",
        //                        Description =
        //                            "We are checking your registration Info once we finish will Email you , this proccess may take 2 or 3 days !"
        //                    }
        //                );
        //            }


                    
        //        }
        //        else if (result.IsNotAllowed)
        //        {
        //            var user = await userManager.FindByEmailAsync(LoginModel.Email);

        //            if (user != null && !user.EmailConfirmed)
        //            {
        //                var token = userManager.GenerateEmailConfirmationTokenAsync(user);
        //                var confirmEmailUrl = Url.Action(
        //                    "ConfirmEmail",
        //                    "Account",
        //                    new { UserId = user.Id, Token = token.Result },
        //                    Request.Scheme
        //                );
        //                var isEmailSent = EmailSender.Send_Mail(
        //                    user.Email,
        //                    "Click this link to confirm your <strong>Email</strong> <br> "
        //                        + confirmEmailUrl,
        //                    "Confirm Your Email",
        //                    "Esta"
        //                );
        //                if (isEmailSent)
        //                {
        //                    return View(
        //                        "ConfirmEmail",
        //                        new ErrorViewModel
        //                        {
        //                            Title = "Email Confirmation Requested to login",
        //                            Description =
        //                                "We Have Emailed you  with your confirmation link ,please confirm your Email!"
        //                        }
        //                    );
        //                }
        //                else
        //                {
        //                    return View(
        //                        "ConfirmEmail",
        //                        new ErrorViewModel
        //                        {
        //                            Title = "Email Confirmation Failed",
        //                            Description =
        //                                "Confirmation of your email has some issues ! please provide a valid Email"
        //                        }
        //                    );
        //                }
        //            }
        //        }
        //        else
        //        {
        //            var error = result.ToString();
        //            ModelState.AddModelError(string.Empty, "Incorrect Email or Password");
        //             return View();
        //        }
        //    }
        //    return View();
        //}

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel LoginModel)
        {
            if (ModelState.IsValid)
            {

                var user = await userManager.FindByEmailAsync(LoginModel.Email);

                if (user != null)
                {


                    if (user != null && user.IsApproved == true&&user.EmailConfirmed==true&&user.IsDeleted==false)
                    {
                        var result = await signInManager.PasswordSignInAsync(LoginModel.Email,
                            LoginModel.Password, false, false);
                        if (result.Succeeded)
                        {
                            if (await userManager.IsInRoleAsync(user, "Admin"))
                            {
                                return RedirectToAction("index", "courses", new { area = "Admin" });
                            }
                            else if(await userManager.IsInRoleAsync(user, "Moderator")&& await userManager.IsInRoleAsync(user, "User"))
                            {
                                return RedirectToAction("profile", "user");
                            }
                            else if (await userManager.IsInRoleAsync(user, "Moderator"))
                            {
                                // 
                                return RedirectToAction("index", "Forums", new { area = "Admin" });
                            }
                            else 
                            {
                                return RedirectToAction("Index", "Home");
                            }

                        }
                        else if (result.IsNotAllowed)
                        {
                            var error = result.ToString();
                            ModelState.AddModelError(string.Empty, "Incorrect Email or Password");
                            return View();

                        }
                        else if (result.Succeeded == false && result.IsNotAllowed == false) 
                        {
                            var error = result.ToString();
                            ModelState.AddModelError(string.Empty, "Incorrect Email or Password");
                            return View();
                        }
                    }




                    if (
                        await appRep.ConstantsRep.getMempershipExpiryMonth()
                        <
                        DateTime.Now.Month
                        ) {

                  await      appRep.UserRep.RevokeMempershipPayment(user.Id);
                        await appRep.SaveChangesAsync();
                    }


                    if (user.IsDeleted)
                    {
                        return View(
                           "ConfirmEmail",
                           new ErrorViewModel
                           {
                               Title = "Account Deleted",
                               Description =
                                   "We are sorry to inform you that your account was deleted for violating our conditions&terms. for more info contact support team."
                           }
                       );
                    }



                    if (user != null && !user.EmailConfirmed)
                    {
                        var token = userManager.GenerateEmailConfirmationTokenAsync(user);
                        var confirmEmailUrl = Url.Action(
                            "ConfirmEmail",
                            "Account",
                            new { UserId = user.Id, Token = token.Result },
                            Request.Scheme
                        );
                        var isEmailSent = EmailSender.Send_Mail(
                            user.Email,
                            "Click this link to confirm your <strong>Email</strong> <br> "
                                + confirmEmailUrl,
                            "Confirm Your Email",
                            "Esta"
                        );
                        if (isEmailSent)
                        {
                            return View(
                                "ConfirmEmail",
                                new ErrorViewModel
                                {
                                    Title = "Email Confirmation Requested to login",
                                    Description =
                                        "We Have Emailed you  with your confirmation link ,please confirm your Email!"
                                }
                            );
                        }
                        else
                        {
                            return View(
                                "ConfirmEmail",
                                new ErrorViewModel
                                {
                                    Title = "Email Confirmation Failed",
                                    Description =
                                        "Confirmation of your email has some issues ! please provide a valid Email"
                                }
                            );
                        }

                    }

                    if (user != null && user.IsApproved == true)
                    {
                        if (await userManager.IsInRoleAsync(user, "Admin"))
                        {
                            return RedirectToAction("index", "courses", new { area = "Admin" });
                        }
                        else if (await userManager.IsInRoleAsync(user, "Moderator"))
                        {
                            // 
                            return RedirectToAction("index", "Forums", new { area = "Admin" });
                        }
                        else
                        {
                            return RedirectToAction("profile", "user");
                        }
                    }
                    else {
                        return    View(
                               "ConfirmEmail",
                               new ErrorViewModel
                               {
                                   Title = "Account Not Approved",
                                   Description =
                                       "We are checking your Account and Will approve it soon"
                               }
                           );

                    }








                }
                else
                {
                    return View();
                }

            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            if (signInManager.IsSignedIn(User))
           return    RedirectToAction("Index", "Home");
            RegisterViewModel registerModel = new RegisterViewModel();
            try
            {
                var Questions = await appRep.QuestionRep.GetAllQuestions();
                registerModel.Questions = Questions.ToList();
                registerModel.codeofEthics =
                    Thread.CurrentThread.CurrentCulture.Name == "ar"
                        ? appRep.ContentRep.GetContent("ethics").DescriptionAr
                        : appRep.ContentRep.GetContent("ethics").DescriptionEn;
            }
            catch (Exception)
            {
                registerModel.Questions = new List<Question>();
            }

            return View(registerModel);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerModel)
        {
            try
            {
                var validationStatesValues = ModelState.Values;

                /// Next is edit passport and nationalid client and server validation

           
                        if (ModelState.IsValid)
                {

                    var IsNationalityvalid = registerModel.IsNationalityClaimsValid();

                    if (!IsNationalityvalid&&registerModel.Country=="Egypt")
                    {
                        if (string.IsNullOrEmpty(registerModel.NationalCardID)) {
                             ModelState.TryAddModelError(nameof(registerModel.NationalCardID), localizer["required"]);

                        }
                        if (registerModel.NationalCardImages==null) {
                            ModelState.TryAddModelError(nameof(registerModel.NationalCardImages), localizer["required"]);

                        }
                        if (registerModel.NationalCardImages != null&&registerModel.NationalCardImages.Count<2)
                        {
                            ModelState.TryAddModelError(nameof(registerModel.NationalCardImages), localizer["IdCardLimit"]);

                        }


                    }
                    if (!IsNationalityvalid && registerModel.Country != "Egypt")
                    {
                        if (string.IsNullOrEmpty(registerModel.Passport))
                        {
                            ModelState.TryAddModelError(nameof(registerModel.Passport), localizer["required"]);

                        }
                        if (registerModel.PassportImages == null)
                        {
                            ModelState.TryAddModelError(nameof(registerModel.PassportImages), localizer["required"]);

                        }
                    }

                    if (registerModel.IsNewMember ==false&& String.IsNullOrEmpty(registerModel.MembershipNumber))
                    {

                            ModelState.AddModelError(nameof(registerModel.MembershipNumber), localizer["required"]);
                            return View(registerModel);
                       


                    }


                    ///

                    User user = new User();

                    ////////// uploading Graduation Certificate Image
                    var userImages=new List<UserImage>();
                    try
                    {
                        if (registerModel.GraduationCertificateImages != null)
                        {
                            foreach (var image in registerModel.GraduationCertificateImages)
                            {
                            var SavePath = hostEnvironment.WebRootPath + Constants.GraduationCertificateImagesSavingPath;
                            var PhotoName = await FileUpload.SavePhotoAsync(
                                image,
                             registerModel.FullName,
                                SavePath
                            );
                                //               user.GradutionImagePath = Constants.GraduationCertificateImagesSavingPath + PhotoName;
                                userImages.Add(new UserImage() {TypeId=3,Path= Constants.GraduationCertificateImagesSavingPath + PhotoName,UserId=user.Id });
                                //we should add images to database

                            }
                           
                        }
                        else
                        {
                            ModelState.AddModelError(
                                "GraduationCertificateImage",
                                "Graduation Image Is Required"
                            );
                        }
                    }
                    catch (Exception)
                    {
                        ModelState.AddModelError(
                            "GraduationCertificateImage",
                            "Graduation Image Is Required"
                        );
                    }
                    ///// Uploading National ID Image
                    try
                    {
                        if (registerModel.NationalCardImages != null)
                        {
                           
                            foreach (var image in registerModel.NationalCardImages)
                            {
                            var SavePath = hostEnvironment.WebRootPath + Constants.NationalIDsImagesSavingPath;
                            var PhotoName = await FileUpload.SavePhotoAsync(
                             image,
                                  registerModel.FullName,
                                SavePath
                            );
                                userImages.Add(new UserImage() { TypeId = 1, Path = Constants.NationalIDsImagesSavingPath + PhotoName, UserId = user.Id });
                                //adding to database

                            }

                            //     user.NationalIDImagePath = Constants.NationalIDsImagesSavingPath + PhotoName;
                        }
                        //else
                        //{
                        //    ModelState.AddModelError("NationalCardImage", "National Card Image Is Required");
                        //}
                    }
                    catch (Exception)
                    {
                        ModelState.AddModelError(
                            "NationalCardImage",
                            "National Card Image Is Required"
                        );
                    }
                    /////// // Uploading Passport ID Image
                    try
                    {

                   
                        if (registerModel.PassportImages != null)
                        {
                              foreach (var image in registerModel.PassportImages)
                        {
                             var SavePath = hostEnvironment.WebRootPath + Constants.PassportsImagesSavingPath;
                            var PhotoName = await FileUpload.SavePhotoAsync(
                              image,
                                  registerModel.FullName,
                                SavePath
                            );

                                userImages.Add(new UserImage() { TypeId = 2, Path = Constants.PassportsImagesSavingPath + PhotoName, UserId = user.Id });
                                //add to db
                                await appRep.ImageRep.AddImages(userImages);

                                await this.appRep.SaveChangesAsync();
                            }

                            //        user.PassportImagePath = Constants.PassportsImagesSavingPath + PhotoName;
                        }
                        //else
                        //{
                        //    ModelState.AddModelError("NationalCardImage", "National Card Image Is Required");
                        //}
                    }
                    catch (Exception)
                    {
                        ModelState.AddModelError(
                            "NationalCardImage",
                            "Passport Card Image Is Required"
                        );
                    }
                    //////////////////////////
                    ///

                 
            
                 



                    
                    user.ConvertRegisterModelToUser(registerModel);

                    var result = await userManager.CreateAsync(user, registerModel.Password);

                    if (result.Succeeded)
                    {
                        await this.appRep.UserAnswerRep.AddAnswers(
                            registerModel.ConvertQuestionsToUserAnswer(user.Id)
                        );
                        await appRep.ImageRep.AddImages(userImages);

                        await this.appRep.SaveChangesAsync();
                        var token = userManager.GenerateEmailConfirmationTokenAsync(user);
                        await userManager.AddToRoleAsync(user, "User");
                        var confirmEmailUrl = Url.Action(
                            "ConfirmEmail",
                            "Account",
                            new { UserId = user.Id, Token = token.Result },
                            Request.Scheme
                        );

                        var isEmailSent = EmailSender.Send_Mail(
                            user.Email,
                            "Click this link to confirm your <strong>Email</strong> <br> "
                                + confirmEmailUrl,
                            "Confirm Your Email",
                            "Esta"
                        );
                        if (isEmailSent)
                        {
                            return View(
                                "ConfirmEmail",
                                new ErrorViewModel
                                {
                                    Title = localizer["emailconfirm"],
                                    Description =
                                   localizer["emailconfirmdesc"]
                                }
                            );
                        }
                        else
                        {
                            return View(
                                "ConfirmEmail",
                                new ErrorViewModel
                                {
                                    Title = "Email Confirmation Failed",
                                    Description =
                                        "Confirmation of your email has some issues ! please provide a valid email"
                                }
                            );
                        }
                    }
                    else
                    {
                        foreach (var item in result.Errors.ToList())
                        {
                            ModelState.AddModelError("", item.Description);
                        }
                        //      registerModel.Questions = await Uow.QuestionRep.GetAllQuestions();
                        registerModel.codeofEthics =
                            Thread.CurrentThread.CurrentCulture.Name == "ar"
                                ? appRep.ContentRep.GetContent("ethics").DescriptionAr
                                : appRep.ContentRep.GetContent("ethics").DescriptionEn;
                        return View(registerModel);
                    }
                }
                else
                { // Model not valid
                    //var errors = ModelState
                    //    .Select(x => x.Value.Errors)
                    //    .Where(y => y.Count > 0)
                    //    .ToList();
                    var invalidStatesValues = validationStatesValues
                        .Where(y => y.ValidationState == ModelValidationState.Invalid)
                        .ToList();
                    for (int i = 0; i < invalidStatesValues.Count(); i++)
                    {
                        for (int j = 0; j < invalidStatesValues[i].Errors.Count(); j++)
                        {
                            ModelState.AddModelError(
                                string.Empty,
                                invalidStatesValues[i].Errors[j].ErrorMessage
                            );
                        }
                    }

                    //      registerModel.Questions = await Uow.QuestionRep.GetAllQuestions();
                    registerModel.codeofEthics =
                        Thread.CurrentThread.CurrentCulture.Name == "ar"
                            ? appRep.ContentRep.GetContent("ethics").DescriptionAr
                            : appRep.ContentRep.GetContent("ethics").DescriptionEn;
                    return View(registerModel);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Try Again Later !!!");
                //        registerModel.Questions = await Uow.QuestionRep.GetAllQuestions();
                registerModel.codeofEthics =
                    Thread.CurrentThread.CurrentCulture.Name == "ar"
                        ? appRep.ContentRep.GetContent("ethics").DescriptionAr
                        : appRep.ContentRep.GetContent("ethics").DescriptionEn;
                return View(registerModel);
            }
        }

        public async Task<IActionResult> ConfirmEmail(string UserId, string Token)
        {
            if (String.IsNullOrEmpty(UserId) || String.IsNullOrEmpty(Token))
            {
                return View(new ErrorViewModel { Title = "Email Confirmation Failed " });
            }
            var user = await userManager.FindByIdAsync(UserId);

            var result = await userManager.ConfirmEmailAsync(user, Token);

            if (result.Succeeded)
            {
                return View(
                    new ErrorViewModel
                    {
                        Title = "Email Confirmation Success",
                        Description = "Congratulations,Your Email was confirmed successfully"
                    }
                );
            }
            else
            {
                return View(
                    new ErrorViewModel
                    {
                        Title = "Email Confirmation Failed",
                        Description = "Oops,Your Email was not confirmed ,please use a valid email!"
                    }
                );
            }
        }



        [HttpGet]
        public IActionResult ResetPassword() 
        {
            return View() ;
       }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel rpvm)
        {
            var email= rpvm.Email;

            var user = await userManager.FindByEmailAsync(email);
            if (user!=null)
            {
                var token = await userManager.GeneratePasswordResetTokenAsync(user);

                var resetPasswordUrl = Url.Action(
                           "ChangePassword",
                           "Account",
                           new { email = user.Email, token = token },
                           Request.Scheme
                       );

                EmailSender.Send_Mail(
                    user.Email, 
                    "Click this link to reset your password "+resetPasswordUrl, 
                    "Reset Password", 
                    "Esta");
            
            return View("_Info", new Helpers.Info("Password Reset", "We Have Sent You via Email Reset Password Link"));
            }
            return View("_Info", new Helpers.Info("Password Reset", "We Couldn't send Reset Password Link ,Review  Your Email and try again! "));



        }



        [HttpGet]
        public IActionResult ChangePassword(string email,string token)
        {

            return View(new ResetPasswordViewModel(email,token) );
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ResetPasswordViewModel rpvm)
        {

            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(rpvm.Email);
                var result = await userManager.ResetPasswordAsync(user, rpvm.Token, rpvm.NewPassword);
               

                if(result.Succeeded)
                return View("_Info", new Info("Reset Password Succeeded", "your password has been changed .try to login now"));
                if (result.Errors.Any())
                    return View("_Info", new Info("Reset Password Failed", result.Errors.ToString()));
            
            }
    
                return View();
          
            
            }







        public async Task<IActionResult> Logout()
        {
    

            await this.signInManager.SignOutAsync();
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult CreateUser(Level level)
        {
            return View();
        }
    }
}
