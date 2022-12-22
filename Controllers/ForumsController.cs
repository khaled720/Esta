using AutoMapper;
using ESTA.Models;
using ESTA.Repository.IRepository;
using ESTA.StopWords;
using ESTA.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using System.Diagnostics;

namespace ESTA.Controllers
{
    public class ForumsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IAppRep appRep;
        private readonly UserManager<User> _userManager;

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public ForumsController(IAppRep appRep, IMapper mapper, UserManager<User> userManager)
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
                var roles = await _userManager.IsInRoleAsync(user, "Admin");

                if (!roles)
                    forumList = forumList.Where(f => f.levelId <= user.LevelId).ToList();
            }
            return View(forumList);
        }
        [Authorize]
        public async Task<IActionResult> GetForumAsync(int id)
        {
            var Forum = appRep.ForumRep.GetForum(id, true);
            Forum.UserForum = appRep.ForumRep.GetComments(id, 0);
            if (Forum != null)
            {
                var user = await _userManager.GetUserAsync(User);
                var roles = await _userManager.IsInRoleAsync(user, "Admin");

                if (roles || user.LevelId >= Forum.LevelId)
                {
                    ForumsWithComments ViewForum = _mapper.Map<Forum, ForumsWithComments>(Forum);
                    ViewForum.UserForum.Select(x => x.RepliesCount = appRep.ForumRep.GetRepliesCount(x.Id)).ToList();
                    return View(ViewForum);
                }
            }
            return RedirectToAction("Error");
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult NewForum()
        {
            ViewBag.LevelsListItem = GetLevelsAsync();
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
        public IActionResult EditForum(int id)
        {
            Forum? Forum = appRep.ForumRep.GetForum(id, true);

            ViewBag.LevelsListItem = GetLevelsAsync();
            if (Forum != null)
            {
                EditForum editForum = _mapper.Map<Forum, EditForum>(Forum);
                return View(editForum);
            }
            return View("Error");
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> EditForumAsync(EditForum EditForum)
        {
            Forum Forum = appRep.ForumRep.GetForum(EditForum.Id, false);
            if (Forum != null)
            {
                _mapper.Map(EditForum, Forum);
                await appRep.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View("Error");
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> DeleteForumAsync(int id)
        {
            Forum Forum = appRep.ForumRep.GetForum(id, false);
            if (Forum != null)
            {
                appRep.ForumRep.DeleteForum(Forum);
                appRep.SaveChangesAsync();
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
            GetUserForums addedComment = _mapper.Map<UserForum, GetUserForums>(newComment);

            return Json(new { data = addedComment });
        }
        [HttpPost]
        [Authorize]
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
            var Reply = appRep.ForumRep.GetReplies(commentId, null);
            Reply.Add(appRep.ForumRep.GetCommentById(commentId));
            appRep.ForumRep.DeleteComment(Reply);
            await appRep.SaveChangesAsync();

            return Json(true);
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
            getUserForums.ForEach(x => x.RepliesCount = appRep.ForumRep.GetRepliesCount(x.Id));

            return Json(getUserForums);
        }
        [HttpGet]
        public IActionResult GetComment(int id)
        {
            var forumsList = appRep.ForumRep.GetCommentById(id);
            GetUserForums getUserForums = _mapper.Map<UserForum, GetUserForums>(forumsList);
            getUserForums.RepliesCount = appRep.ForumRep.GetRepliesCount(getUserForums.Id);

            return View(getUserForums);
        }
        [Authorize]
        public async Task<IActionResult> SearchCommentAsync(string query)
        {
            var terms = PreparingQuery(query);

            var user = await _userManager.GetUserAsync(User);
            var roles = await _userManager.IsInRoleAsync(user, "Admin");
            List<UserForum> list;
            if (roles)
                list = appRep.ForumRep.SearchComments(terms, 0, 0);
            else
                list = appRep.ForumRep.SearchComments(terms, user.LevelId, 0);

            List<GetUserForums> getComments = _mapper.Map<List<UserForum>, List<GetUserForums>>(list);
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

            return Json(getComments);
        }
        [HttpGet]
        public IActionResult GetCommentReplies(int parentId, int? page = 0)
        {
            var forumsList = appRep.ForumRep.GetReplies(parentId, page);
            List<GetUserForums> getUserForums = _mapper.Map<List<UserForum>, List<GetUserForums>>(forumsList);

            return Json(getUserForums);
        }
        [HttpGet]
        public IActionResult GetForumStatistics(int? id)
        {
            //count of comment/ count of replies/ count of engaging users.
            ForumStatisticsObj statisticsObj = new ForumStatisticsObj();
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
        [NonAction]
        private async Task<List<SelectListItem>> GetLevelsAsync()
        {
            IEnumerable<Level> level = await appRep.LevelRep.GetAllLevels();
            List<SelectListItem> selectList = new();
            level.ToList().ForEach(l =>
            {
                selectList.Add(new SelectListItem
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
