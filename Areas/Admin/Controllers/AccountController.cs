using ESTA.Areas.Admin.ViewModels;
using ESTA.Models;
using ESTA.Repository.IRepository;
using ESTA.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.Xml.Linq;

namespace ESTA.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> _role;

        public AccountController(
            SignInManager<User> _signInManager,
            UserManager<User> userManager,
            RoleManager<IdentityRole> role
        )
        {
            signInManager = _signInManager;
            this.userManager = userManager;
            _role = role;
        }
        [Authorize("RequireAdminRole")]
        public async Task<IActionResult> IndexAsync()
        {
            List<ViewAdmins> AdminsList = new();
            List<User> users = userManager.Users.ToList();

            foreach (var user in users)
            {
                var roles = await userManager.GetRolesAsync(user);

                if (roles != null && roles.Any(x => x != "User"))
                    AdminsList.Add(new ViewAdmins { Id = user.Id, Name = user.UserName, Roles = string.Join(',', roles) });
            }
            //users.ForEach(async (x) =>
            //{
            //    var roles = await userManager.GetRolesAsync(x);
            //    //AdminsList.Add(new ViewAdmins { Id = x.Id, Name = x.UserName, Roles = string.Join(',', roles) });
            //});

            return View(AdminsList);
        }

        [Authorize("RequireAdminRole")]
        public IActionResult CreateAdmin()
        {
            ViewBag.roleList = RolesToListItems();

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

                foreach (var role in admin.RoleId)
                    await userManager.AddToRoleAsync(findUser, role);

                ViewBag.success = true;
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.roleList = RolesToListItems();

                foreach (var error in res.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            ViewBag.success = false;
            return View();
        }

        [Authorize("RequireAdminRole")]
        public async Task<IActionResult> EditUser(string userId)
        {
            EditUser editUser = new()
            {
                Id = userId
            };
            var user = await userManager.FindByIdAsync(userId);
            editUser.RoleId = await userManager.GetRolesAsync(user);

            var RoleList = RolesToListItems();

            ViewBag.roleList = RoleList;

            return View(editUser);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(EditUser editUser)
        {
            var user = await userManager.FindByIdAsync(editUser.Id);
            var UsersRoles = await userManager.GetRolesAsync(user);

            //insert newly added role
            foreach (var role in editUser.RoleId)
            {
                if (!UsersRoles.Contains(role))
                    await userManager.AddToRoleAsync(user, role);
            }

            //remove deleted roles
            foreach (var role in UsersRoles)
            {
                if (!editUser.RoleId.Contains(role))
                    await userManager.RemoveFromRoleAsync(user, role);
            }

            return RedirectToAction("Index");
        }

        [Authorize]
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
        //AccessDenied
        private List<SelectListItem> RolesToListItems()
        {
            List<SelectListItem> roleList = new();
            List<IdentityRole> roles = _role.Roles.ToList();

            foreach (var role in roles)
            {
                roleList.Add(new SelectListItem
                {
                    Text = role.Name,
                    Value = role.Name
                });
            }

            return roleList;
        }
    }
}
