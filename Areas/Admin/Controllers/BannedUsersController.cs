using AutoMapper;
using ESTA.Models;
using ESTA.Repository.IRepository;
using ESTA.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ESTA.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize("AdminOrModerator")]
    public class BannedUsersController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork appRep;
        private readonly UserManager<User> _userManager;

        public BannedUsersController(IUnitOfWork appRep, IMapper mapper, UserManager<User> userManager)
        {
            this.appRep = appRep;
            _mapper = mapper;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> LoadTable()
        {
            var AllBannedUser = appRep.ForumBannedUserRep.GetAllBannedUser();

            var user = await _userManager.GetUserAsync(User);
            var Mod = await _userManager.IsInRoleAsync(user, "Moderator");

            if (Mod)
            {
                var ModForums = appRep.ModeratorRep.GetModeratorForumById(user.Id);

                AllBannedUser = AllBannedUser.Where(x => ModForums.Any(y => y.ForumId == x.ForumId)).ToList();
            }

            var ViewBannedUser = _mapper.Map<List<ForumBannedUser>, List<ViewBanned>>(AllBannedUser);

            return Json(new { data = ViewBannedUser });
        }

        public async Task<IActionResult> UnbanUser(int id)
        {
            var BannedUser = appRep.ForumBannedUserRep.GetBannedUserById(id);

            if (BannedUser != null)
                BannedUser.Active = 0;

            await appRep.SaveChangesAsync();

            return Json(true);
        }
    }
}
