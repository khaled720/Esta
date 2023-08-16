using AutoMapper;
using ESTA.Models;
using ESTA.Repository.IRepository;
using ESTA.StopWords;
using ESTA.ViewModels;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Drawing;

namespace ESTA.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize("AdminOrModerator")]
    public class ForumsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork appRep;
        private readonly UserManager<User> _userManager;

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public ForumsController(IUnitOfWork appRep, IMapper mapper, UserManager<User> userManager)
        {
            this.appRep = appRep;
            _mapper = mapper;
            _userManager = userManager;
        }
        [Authorize]
        public async Task<IActionResult> IndexAsync()
        {
            var lists = appRep.ForumRep.GetAllForums();
            List<GetForum> forumList = _mapper.Map<List<Forum>, List<GetForum>>(lists);
            if (forumList != null)
            {
                var user = await _userManager.GetUserAsync(User);
                var Mod = await _userManager.IsInRoleAsync(user, "Moderator");

                if (Mod)
                {
                    var ModForums = appRep.ModeratorRep.GetModeratorForumById(user.Id);

                    forumList = forumList.Where(f => ModForums.Any(x => x.ForumId == f.Id)).ToList();
                }
            }
            return View(forumList);
        }
        [Authorize]
        public async Task<IActionResult> GetForumAsync(int id)
        {
            var Forum = appRep.ForumRep.GetForum(id, true);

            if (Forum != null)
            {
                Forum.UserForum = appRep.ForumRep.GetComments(id, 0);

                var user = await _userManager.GetUserAsync(User);
                var roles = await _userManager.IsInRoleAsync(user, "Admin");
                var Mod = await _userManager.IsInRoleAsync(user, "Moderator");

                var AllowMod = false;
                if (Mod)
                {
                    var ModForums = appRep.ModeratorRep.GetModeratorForumById(user.Id);

                    AllowMod = ModForums.Any(x => x.ForumId == id);
                }

                if (AllowMod || roles || user.LevelId >= Forum.LevelId || Forum.LevelId == 4)
                {
                    ForumsWithComments ViewForum = _mapper.Map<Forum, ForumsWithComments>(Forum);
                    ViewForum.UserForum.Select(x => x.Banned = appRep.ForumBannedUserRep.IsUserBanned(x.userId, id)).ToList();
                    ViewForum.UserForum.Select(x => x.RepliesCount = appRep.ForumRep.GetRepliesCount(x.Id)).ToList();
                    ViewForum.UserForum.Select(x => x.Replies.Select(y => y.Banned = appRep.ForumBannedUserRep.IsUserBanned(y.userId, id)).ToList()).ToList();

                    ViewBag.CheckMoreComments = appRep.ForumRep.CheckMoreComments(1, ForumId: id);

                    return View(ViewForum);
                }
            }
            return RedirectToAction("Error");
        }
        public IActionResult CheckMoreComments(int forumId, int page)
        {
            bool MoreComments = appRep.ForumRep.CheckMoreComments(page, ForumId: forumId);

            return Json(MoreComments);
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> NewForum()
        {
            ViewBag.LevelsListItem = await GetLevelsAsync();
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> NewForumAsync(AddForum forum)
        {
            var Newforum = new Forum
            {
                Description = forum.Description,
                Title = forum.Title,
                LevelId = forum.levelId
            };
            appRep.ForumRep.AddForum(Newforum);
            await appRep.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> EditForumAsync(int id)
        {
            Forum? Forum = appRep.ForumRep.GetForum(id, true);

            ViewBag.LevelsListItem = await GetLevelsAsync();
            if (Forum != null)
            {
                EditForum editForum = _mapper.Map<Forum, EditForum>(Forum);
                return View(editForum);
            }
            return RedirectToAction("Error");
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> EditForumAsync(EditForum EditForum)
        {
            Forum? Forum = appRep.ForumRep.GetForum(EditForum.Id, false);
            if (Forum != null)
            {
                _mapper.Map(EditForum, Forum);
                await appRep.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Error");
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> DeleteForumAsync(int id)
        {
            Forum? Forum = appRep.ForumRep.GetForum(id, false);
            if (Forum != null)
            {
                appRep.ForumRep.DeleteForum(Forum);
                await appRep.SaveChangesAsync();
                List<UserForum> commentList = appRep.ForumRep.GetComments(id, null);
                appRep.ForumRep.DeleteComment(commentList);
                await appRep.SaveChangesAsync();
                return Json(true);
            }
            return View(false);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddCommentAsync(int forumId, string comment)
        {
            var newComment = new UserForum
            {
                Comment = comment,
                userId = _userManager.GetUserId(User),
                forumId = forumId
            };
            appRep.ForumRep.AddComment(newComment);
            await appRep.SaveChangesAsync();

            newComment = appRep.ForumRep.GetCommentById(newComment.Id);

            if (newComment != null)
            {
                GetUserForums addedComment = _mapper.Map<UserForum, GetUserForums>(newComment);
                var commentList = new List<GetUserForums>
            {
                addedComment
            };
                var renderComment = new RenderComment()
                {
                    showAllLink = true,
                    showReply = false,
                    UserForums = commentList
                };

                return PartialView("_RenderComment", renderComment);
            }
            return RedirectToAction("Error");

        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddReplyAsync(int forumId, string comment, int parentId)
        {
            var newReply = new UserForum
            {
                Comment = comment,
                userId = _userManager.GetUserId(User),
                forumId = forumId,
                ParentId = parentId
            };
            appRep.ForumRep.AddComment(newReply);
            await appRep.SaveChangesAsync();

            return RedirectToAction("GetCommentReplies", new { parentId });
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCommentAsync(int commentId)
        {
            var Reply = appRep.ForumRep.GetReplies(commentId);
            if (Reply != null)
            {
                Reply.Add(appRep.ForumRep.GetCommentById(commentId));
                appRep.ForumRep.DeleteComment(Reply);
                await appRep.SaveChangesAsync();

                return RedirectToAction("GetForum", new { id = Reply[0].forumId });
            }
            return RedirectToAction("Error");

        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteReplyAsync(int commentId)
        {
            var Reply = appRep.ForumRep.GetCommentById(commentId);
            appRep.ForumRep.DeleteReply(Reply);
            await appRep.SaveChangesAsync();

            return Json(true);
        }
        [HttpGet]
        public IActionResult GetForumsComment(int forumId, int page)
        {
            var forumsList = appRep.ForumRep.GetComments(forumId, page);
            List<GetUserForums> getUserForums = _mapper.Map<List<UserForum>, List<GetUserForums>>(forumsList);
            getUserForums.ForEach(x => x.Banned = appRep.ForumBannedUserRep.IsUserBanned(x.userId, forumId));
            getUserForums.ForEach(x => x.Replies.Select(y => y.Banned = appRep.ForumBannedUserRep.IsUserBanned(x.userId, forumId)).ToList());
            getUserForums.ForEach(x => x.RepliesCount = appRep.ForumRep.GetRepliesCount(x.Id));

            var renderComment = new RenderComment()
            {
                showAllLink = true,
                showReply = false,
                UserForums = getUserForums
            };

            return PartialView("_RenderComment", renderComment);
        }
        [HttpGet]
        public IActionResult GetComment(int id)
        {
            var forumsList = appRep.ForumRep.GetCommentById(id);
            GetUserForums getUserForums = _mapper.Map<UserForum, GetUserForums>(forumsList);
            getUserForums.Banned = appRep.ForumBannedUserRep.IsUserBanned(getUserForums.userId, getUserForums.forumId);
            getUserForums.Replies.Select(x => x.Banned = appRep.ForumBannedUserRep.IsUserBanned(getUserForums.userId, getUserForums.forumId)).ToList();

            getUserForums.RepliesCount = appRep.ForumRep.GetRepliesCount(getUserForums.Id);

            return View(getUserForums);
        }
        [Authorize]
        public async Task<IActionResult> SearchCommentAsync(string query)
        {
            var terms = PreparingQuery(query);

            var user = await _userManager.GetUserAsync(User);
            var roles = await _userManager.IsInRoleAsync(user, "Admin");
            var Mod = await _userManager.IsInRoleAsync(user, "Moderator");
            //Moderator
            List<UserForum> list;
            if (roles || Mod)
                list = appRep.ForumRep.SearchComments(terms, 0, 0);
            else
                list = appRep.ForumRep.SearchComments(terms, user.LevelId, 0);

            List<GetUserForums> getComments = _mapper.Map<List<UserForum>, List<GetUserForums>>(list);


            if (Mod)
            {
                var ModForums = appRep.ModeratorRep.GetModeratorForumById(user.Id);

                getComments = getComments.Where(f => ModForums.Any(x => x.ForumId == f.forumId)).ToList();

                getComments.ForEach(x => x.Banned = appRep.ForumBannedUserRep.IsUserBanned(x.userId, x.forumId));
                getComments.ForEach(x => x.Replies.Select(y => y.Banned = appRep.ForumBannedUserRep.IsUserBanned(y.userId, y.forumId)).ToList());
            }

            ViewBag.query = query;

            return View(getComments);
        }
        public async Task<IActionResult> GetNextSearchCommentAsync(string query, int page)
        {
            var terms = PreparingQuery(query);

            var user = await _userManager.GetUserAsync(User);
            var roles = await _userManager.IsInRoleAsync(user, "Admin");
            List<UserForum> list;
            if (roles)
                list = appRep.ForumRep.SearchComments(terms, 0, page);
            else
                list = appRep.ForumRep.SearchComments(terms, user.LevelId, page);

            List<GetUserForums> getComments = _mapper.Map<List<UserForum>, List<GetUserForums>>(list);
            getComments.ForEach(x => x.Banned = appRep.ForumBannedUserRep.IsUserBanned(x.userId, x.forumId));
            getComments.ForEach(x => x.Replies.Select(y => y.Banned = appRep.ForumBannedUserRep.IsUserBanned(y.userId, y.forumId)).ToList());

            var renderComment = new RenderComment()
            {
                showAllLink = true,
                showReply = false,
                UserForums = getComments
            };
            return PartialView("_RenderComment", renderComment);
        }
        [HttpGet]
        public IActionResult GetCommentReplies(int parentId, int page = 0)
        {
            var forumsList = appRep.ForumRep.GetReplies(parentId, page);
            List<GetUserForums> getUserForums = _mapper.Map<List<UserForum>, List<GetUserForums>>(forumsList);
            getUserForums.Select(x => x.Banned = appRep.ForumBannedUserRep.IsUserBanned(x.userId, x.forumId)).ToList();

            return Json(getUserForums);
        }
        [HttpGet]
        public IActionResult CheckCommentReplies(int parentId, int page = 0)
        {
            bool CheckReplies = appRep.ForumRep.CheckMoreComments(page, parentId);

            return Json(CheckReplies);
        }
        [HttpGet]
        public IActionResult GetForumStatistics(int? id)
        {
            //count of comment/ count of replies/ count of engaging users.
            ForumStatisticsObj statisticsObj = new();
            DateTime todayDate = DateTime.Now.Date;
            DateTime yesterday = DateTime.Now.AddDays(-1).Date;
            DateTime threeMonths = DateTime.Now.AddMonths(-3).Date;

            statisticsObj.Today = new()
            {
                NoComment = appRep.ForumRep.GetCommentCountByDate(id, todayDate),
                NoReplies = appRep.ForumRep.GetReplyCountByDate(id, todayDate),
                NoUser = appRep.ForumRep.GetUsersCountByDate(id, todayDate)
            };
            statisticsObj.Yesterday = new()
            {
                NoComment = appRep.ForumRep.GetCommentCountByDate(id, yesterday),
                NoReplies = appRep.ForumRep.GetReplyCountByDate(id, yesterday),
                NoUser = appRep.ForumRep.GetUsersCountByDate(id, yesterday)
            };
            statisticsObj.LastThreeMonths = new()
            {
                NoComment = appRep.ForumRep.GetCommentCountByDate(id, threeMonths, false),
                NoReplies = appRep.ForumRep.GetReplyCountByDate(id, threeMonths, false),
                NoUser = appRep.ForumRep.GetUsersCountByDate(id, threeMonths, false)
            };
            statisticsObj.Total = new()
            {
                NoComment = appRep.ForumRep.GetCommentCountByDate(id),
                NoReplies = appRep.ForumRep.GetReplyCountByDate(id),
                NoUser = appRep.ForumRep.GetUsersCountByDate(id)
            };

            return PartialView("_ForumStatistics", statisticsObj);
        }

        public async Task<IActionResult> BanUser(string UserId, int ForumId, string reason)
        {
            var ModId = _userManager.GetUserId(User);

            appRep.ForumBannedUserRep.NewBannedUser(new ForumBannedUser()
            {
                UserId = UserId,
                ForumId = ForumId,
                Reason = reason,
                ModId = ModId,
                Date = DateTime.Now,
            });

            await appRep.SaveChangesAsync();

            return Json(true);
        }

        [NonAction]
        private async Task<IEnumerable<SelectListItem>> GetLevelsAsync()
        {
            IEnumerable<Level> level = await appRep.LevelRep.GetAllLevels();
            IEnumerable<SelectListItem> selectList = new List<SelectListItem>();
            level.ToList().ForEach(l =>
            {
                selectList = selectList.Append(new SelectListItem
                {
                    Value = l.Id.ToString(),
                    Text = l.TypeName
                });
            });
            return selectList;
        }

        [NonAction]
        private static string[] PreparingQuery(string str)
        {
            var charsToRemove = new string[] { "@", ",", ".", ";", "?", "!" };
            foreach (var c in charsToRemove)
            {
                str = str.Replace(c, string.Empty);
            }
            str = StopWordsExtension.RemoveStopWords(str, "en");
            var terms = str.Split(' ');

            return terms;
        }
    }
}
