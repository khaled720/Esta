using ESTA.Models;
using ESTA.Repository.IRepository;
using ESTA.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace ESTA.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;

        public AccountController(
            SignInManager<User> _signInManager,
            UserManager<User> userManager
        )
        {
            signInManager = _signInManager;
            this.userManager = userManager;
        }
        [Authorize("RequireAdminRole")]
        public IActionResult UserChangePassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UserChangePassword(ChangePassword changePassword)
        {
            var CurrentUser = await userManager.GetUserAsync(User);
            var res = await userManager.ChangePasswordAsync(CurrentUser, changePassword.CurrentPassword, changePassword.NewPassword);

            if (res.Succeeded)
            {
                await signInManager.SignOutAsync();
                return RedirectToAction("Login", "Account", new { area = "" });
            }

            foreach (var error in res.Errors)
                ModelState.AddModelError("", error.Description);

            return View(changePassword);
        }

        [Authorize("RequireAdminRole")]

        public IActionResult CreateAdmin()
        {
            ViewBag.success = false;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateAdminAsync(CreateAdmin admin)
        {
            var res = await userManager.CreateAsync(new User
            {
                UserName = admin.Email,
                Email = admin.Email,
                EmailConfirmed = true,
                LevelId = 1,
                IsApproved = true
            }, admin.NewPassword);

            if (res.Succeeded)
            {
                User findUser = await userManager.FindByNameAsync(admin.Email);
                await userManager.AddToRoleAsync(findUser, "Admin");

                ViewBag.success = true;
                return View();
            }
            else
            {
                foreach (var error in res.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            ViewBag.success = false;
            return View();
        }
    }
}
