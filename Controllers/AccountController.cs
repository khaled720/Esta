using ESTA.Models;
using ESTA.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ESTA.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;

        public AccountController(SignInManager<User> _signInManager,UserManager<User> userManager)
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
               
                var result = await signInManager.PasswordSignInAsync(LoginModel.Email,LoginModel.Password,false, lockoutOnFailure: false);
                Thread.Sleep(2000);
                if (result.Succeeded)
                {
                    var user = await this.userManager.GetUserAsync(HttpContext.User);


                    HttpContext.Session.SetString("UserId",user.Id);
                    HttpContext.Session.SetString("UserEmail", user.Email);

                    //_logger.LogInformation("User logged in.");
                    return Redirect("/User/Profile");
                }
                //if (result.RequiresTwoFactor)
                //{
                //    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
             
                //}
                //if (result.IsLockedOut)
                //{
                //   // _logger.LogWarning("User account locked out.");
                //    return RedirectToPage("./Lockout");
                //}
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
            RegisterViewModel registerView  =new RegisterViewModel();
            return View(registerView);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerModel)
        {

            if (ModelState.IsValid)
            {

                User user = new User();
                user.Email = registerModel.Email;
                user.UserName = registerModel.Email;
                user.FullName = "Khaled Samir";
                user.LevelId = 1;
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await userManager.CreateAsync(user,registerModel.Password);
                if (result.Succeeded)
                {
                    
                    //_logger.LogInformation("User logged in.");
                    return Redirect("Account/Login");
                }
                //if (result.RequiresTwoFactor)
                //{
                //    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });

                //}
                //if (result.IsLockedOut)
                //{
                //   // _logger.LogWarning("User account locked out.");
                //    return RedirectToPage("./Lockout");
                //}
                else
                {
                    ModelState.AddModelError(string.Empty, "Registration did not complete");
                    return View();
                }
            }
            return View();
        }


    
        public async Task<IActionResult> Logout()
        {
            await this.signInManager.SignOutAsync();
            HttpContext.Session.Clear();

          return  Redirect("/Home/Index");
        }



    }
}
