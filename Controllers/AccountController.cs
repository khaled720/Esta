using System.Diagnostics.Eventing.Reader;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using ESTA.Models;
using ESTA.Repository.IRepository;
using ESTA.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ESTA.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<User> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<User> userManager;
        private readonly IUnitOfWork appRep;

        public AccountController(
            SignInManager<User> _signInManager,
            RoleManager<IdentityRole> roleManager,
            UserManager<User> userManager,
            IUnitOfWork appRep
        )
        {
            signInManager = _signInManager;
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.appRep = appRep;
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (signInManager.IsSignedIn(User))
                Redirect("/Home/Index");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel LoginModel)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(
                    LoginModel.Email,
                    LoginModel.Password,
                    false,
                    lockoutOnFailure: false
                );

                if (result.Succeeded)
                {
                    return Redirect("/User/Profile");
                }
                else if (result.IsNotAllowed)
                {
                    var user = await userManager.FindByEmailAsync(LoginModel.Email);

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
                                        "We Have Emailed you with your confirmation link ,please confirm your Email!"
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
                }
                else
                {
                    ModelState.AddModelError(string.Empty, result.ToString());
                    return View();
                }
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            RegisterViewModel registerModel = new RegisterViewModel();
            try
            {
                var Questions = await appRep.QuestionRep.GetAllQuestions();
                registerModel.Questions = Questions.ToList();
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
                var c = ModelState.Values;

                if (ModelState.IsValid)
                {
                    User user = new User();
                    user.ConvertRegisterModelToUser(registerModel);

                    var result = await userManager.CreateAsync(user, registerModel.Password);

                    if (result.Succeeded)
                    {
                        await this.appRep.UserAnswerRep.AddAnswers(
                            registerModel.ConvertQuestionsToUserAnswer(user.Id)
                        );
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
                            "Click this link to confirm your <strong>email</strong> <br> "
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
                                    Title = "Email Confirmation Needed",
                                    Description =
                                        "We Have Emailed you with your activation link ,please confirm your email!"
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
                            ModelState.AddModelError(string.Empty, item.Description);
                        }
                        //      registerModel.Questions = await Uow.QuestionRep.GetAllQuestions();
                        return View(registerModel);
                    }
                }
                //var errors = ModelState
                //    .Select(x => x.Value.Errors)
                //    .Where(y => y.Count > 0)
                //    .ToList();
                ModelState.AddModelError(string.Empty, "Oops,Registration Failed");
                //      registerModel.Questions = await Uow.QuestionRep.GetAllQuestions();
                return View(registerModel);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Try Again Later !!!");
                //        registerModel.Questions = await Uow.QuestionRep.GetAllQuestions();
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

        public async Task<IActionResult> Logout()
        {
            await this.signInManager.SignOutAsync();
            HttpContext.Session.Clear();
            return Redirect("/Home/Index");
        }

        public IActionResult CreateUser(Level level)
        {
            return View();
        }
    }
}
