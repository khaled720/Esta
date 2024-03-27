using AutoMapper;
using ESTA.Models;
using ESTA.Repository;
using ESTA.Repository.IRepository;
using ESTA.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Hosting;

namespace ESTA.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize("RequireAdminRole")]
    public class ModeratorController : Controller
    {
        private readonly IUnitOfWork appRep;
        private readonly UserManager<User> userManager;

        public ModeratorController(IUnitOfWork appRep, UserManager<User> userManager)
        {
            this.appRep = appRep;
            this.userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var ModeratorsModel = await userManager.GetUsersInRoleAsync("Moderator");

            List<ViewModerator> ViewModerators = new();

            ModeratorsModel.ToList().ForEach(x =>
            {
                ViewModerators.Add(new ViewModerator
                {
                    Id = x.Id,
                    Name = x.FullName,
                    UserName = x.UserName
                });
            });

            return View(ViewModerators);
        }
        public IActionResult NewModerator()
        {
            ViewBag.SelectForum = ForumsToSelect();

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> NewModerator(NewModerator moderator)
        {
            var user = new User
            {
                Email = moderator.Email,
                UserName = moderator.Email,
                FullName = moderator.FullName,
                FullNameAr = moderator.FullName,
                IsApproved = true,
                Birthdate = new DateTime(),
                MobilePhone = String.Empty,
                HomePhone = String.Empty,
                EnglishReadingLevel = String.Empty,
                EnglishWritingLevel = String.Empty,
                Hometown = String.Empty,
                StreetName = String.Empty,
                BlockNumber = String.Empty,
                Floor = String.Empty,
                FlatNumber = String.Empty,
                Area = String.Empty,
                City = String.Empty,
                Country = String.Empty,
                Job = String.Empty,
                Company = String.Empty,
                WorkAddress = String.Empty,
                MessagingAddress = String.Empty,
                AcademicQualification = String.Empty,
                University = String.Empty,
                IsMempershipPaid = false,
                JoinDate = DateTime.Now,
                LevelId = 1,
                EmailConfirmed = true,
                GradutionYear = String.Empty,
                PostalCode = String.Empty,
                IsDeleted = false
            };

            var res = await userManager.CreateAsync(user, moderator.Password);

            if (res.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "Moderator");

                ModeratorForum ModeratorForum;

                moderator.SelectForum.ForEach(async x =>
                {
                    ModeratorForum = new()
                    {
                        UserId = user.Id,
                        ForumId = x
                    };

                    appRep.ModeratorRep.NewModeratorForum(ModeratorForum);
                    await appRep.SaveChangesAsync();

                });
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var error in res.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                ViewBag.SelectForum = ForumsToSelect();

                return View(moderator);
            }
        }


        public async Task<IActionResult>
        NewModeratorCurrentUserAsync()
        {
            ViewBag.SelectForum = ForumsToSelect();
            var users= await appRep.UserRep.GetAllUsers();

            var Users=new List<User>();

            foreach (var item in users)
            {
                if (!await userManager.IsInRoleAsync(item,"Moderator") && !await userManager.IsInRoleAsync(item, "Admin")) 
                {
                Users.Add(item);
                }
            }
            ViewBag.Users = Users;

            return View();
        }
        [HttpPost]
        public async Task<IActionResult>
        NewModeratorCurrentUser(User usr)
        {
     
            var res = await userManager.FindByIdAsync(usr.Id);

            if (res!=null)
            {
                await userManager.AddToRoleAsync(res, "Moderator");

                ModeratorForum ModeratorForum;

                usr.SelectForum.ForEach(async x =>
                {
                    ModeratorForum = new()
                    {
                        UserId = res.Id,
                        ForumId = x
                    };

                    appRep.ModeratorRep.NewModeratorForum(ModeratorForum);
                    await appRep.SaveChangesAsync();

                });
                return RedirectToAction("Index");
            }
            else
            {
                //foreach (var error in res.Errors)
                //{
                //    ModelState.AddModelError("", error.Description);
                //}

                ViewBag.SelectForum = ForumsToSelect();

                return View(res);
            }
        }







        public async Task<IActionResult> EditModerator(string id)
        {
            var User = await userManager.FindByIdAsync(id);

            EditModerator moderator = new()
            {
                Id = id,
                FullName = User.FullName,
                SelectForum = appRep.ModeratorRep.GetModeratorForumById(id).Select(x => x.ForumId).ToList(),
            };

            var SelectForum = ForumsToSelect();

            ViewBag.SelectForum = SelectForum;

            return View(moderator);
        }
        [HttpPost]
        public async Task<IActionResult> EditModerator(EditModerator moderator)
        {
            var User = await userManager.FindByIdAsync(moderator.Id);

            User.FullName = moderator.FullName;
            User.FullNameAr = moderator.FullName;

            var res = await userManager.UpdateAsync(User);

            if (res.Succeeded)
            {
                var Forums = appRep.ModeratorRep.GetModeratorForumById(moderator.Id);

                //insert newly added forums
                foreach (var forum in moderator.SelectForum)
                {
                    if (!Forums.Where(x => x.ForumId == forum).Any())
                    {
                        appRep.ModeratorRep.NewModeratorForum(new ModeratorForum()
                        {
                            UserId = moderator.Id,
                            ForumId = forum
                        });

                        await appRep.SaveChangesAsync();
                    }
                }

                //remove deleted forums
                foreach (var forum in Forums)
                {
                    if (!moderator.SelectForum.Where(x => x == forum.ForumId).Any())
                    {
                        appRep.ModeratorRep.RemoveModeratorForum(forum);

                        await appRep.SaveChangesAsync();
                    }
                }
                return RedirectToAction("Index");
            }
            else
            {
                return View(moderator);
            }
        }
        [HttpPost]
        public IActionResult DeleteModerator(int id)
        {
            return View();
        }
        private List<SelectListItem> ForumsToSelect()
        {
            var AllForums = appRep.ForumRep.GetAllForums();
            List<SelectListItem> selectForum = new();

            AllForums.ForEach(x =>
            {
                selectForum.Add(new SelectListItem()
                {
                    Value = x.Id.ToString(),
                    Text = x.Title,
                });
            });

            return selectForum;
        }
    }
}
