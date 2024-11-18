using ESTA.Areas.Admin.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ESTA.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize("RequireAdminRole")]
    public class AdminstrationController : Controller
    {
        private readonly RoleManager<IdentityRole> _role;

        public AdminstrationController(RoleManager<IdentityRole> role)
        {
            _role = role;
        }
        public IActionResult Index()
        {
            List<IdentityRole> roles = _role.Roles.ToList();
            ViewBag.Roles = roles;

            return View();
        }
        public async Task<IActionResult> DeleteRoleAsync(string id)
        {
            var role = await _role.FindByIdAsync(id);
            await _role.DeleteAsync(role);

            return RedirectToAction("Index");
        }
        public IActionResult CreateRole()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateRoleAsync(CreateRole createRole)
        {
            var res = await _role.CreateAsync(new IdentityRole { Name = createRole.RoleName });

            if (res.Succeeded)
            {
                return RedirectToAction("Index");
            }
            return View(createRole);
        }
    }
}
