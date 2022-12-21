using System.Runtime.CompilerServices;
using System.Security.Claims;
using ESTA.Models;
using ESTA.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ESTA.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;

        public AccountController(SignInManager<User> _signInManager, UserManager<User> userManager)
        {
            signInManager = _signInManager;
            this.userManager = userManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
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
                else
                {
                    ModelState.AddModelError(string.Empty, result.ToString());
                    return View();
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            RegisterViewModel registerView = new RegisterViewModel();
            return View(registerView);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    User user = new User();
                    user.Email = registerModel.Email;
                    user.UserName = registerModel.Email;
                    user.FullName = "Khaled Samir2";
                    user.LevelId = 2;

                    var result = await userManager.CreateAsync(user, registerModel.Password);
                    if (result.Succeeded)
                    {
                        return Redirect("/Account/Login");
                    }
                    else
                    {
                        foreach (var item in result.Errors.ToList())
                        {
                            ModelState.AddModelError(string.Empty,item.Description);
                        }
                      
                        return View();
                    }
                   }
                ModelState.AddModelError(string.Empty, "Registration did not complete");
                return View();
            }
            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty, "Try Again Later !!!");
                return View();
            }
           
            }
           
        

        public async Task<IActionResult> Logout()
        {
            await this.signInManager.SignOutAsync();
            HttpContext.Session.Clear();
            return Redirect("/Home/Index");
        }
    }
}
