using ESTA.Models;
using ESTA.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace ESTA.Repository
{
    public class ForumRepository : IForumRepository
    {
        private AppDbContext appContext;

        public ForumRepository(AppDbContext appContext)
        {
            this.appContext = appContext;
        }

        public void AddForum(Forum forum)
        {
            appContext.Forums.Add(forum);
        }

        public List<Forum> GetAllForums()
        {
            return appContext.Forums.Include(f => f.level).ToList();
        }

        public void DeleteForum(Forum forum)
        {
            appContext.Forums.Remove(forum);
        }

        public Forum? GetForum(int id, bool levels, bool comments = false)
        {
            if (levels && comments)
            {
                return appContext.Forums
                    .Where(x => x.Id == id)
                    .Include(x => x.level)
                    .Include(x => x.UserForum)
                    .SingleOrDefault();
            }
            else if (levels && !comments)
            {
                return appContext.Forums
                    .Where(f => f.Id == id)
                    .Include(x => x.level)
                    .SingleOrDefault();
            }
            return appContext.Forums.Where(f => f.Id == id).SingleOrDefault();
        }

        public List<UserForum> GetComments(int forumId, int? page, int count = 3)
        {
            if (!page.HasValue)
            {
                return appContext.UsersForums
                    .Where(x => x.forumId == forumId && x.ParentId == null)
                    .Include(c => c.Replies)
                    .ToList();
            }
            var list = appContext.UsersForums
                .Where(x => x.forumId == forumId && x.ParentId == null)
                .Include(c => c.Replies)
                .Include(x => x.user)
                .OrderByDescending(x => x.Id)
                .Skip(count * page.Value)
                .Take(count)
                .ToList();
            foreach (var comment in list)
            {
                comment.Replies = comment.Replies.OrderByDescending(x => x.Id).Take(count).ToList();
            }
            return list;
        }

        public int GetRepliesCount(int ParentId)
        {
            var Comments = appContext.UsersForums.Where(x => x.ParentId == ParentId).ToList();
            return Comments.Count;
        }

        public UserForum? GetCommentById(int commentId)
        {
            return appContext.UsersForums
                .Where(x => x.Id == commentId)
                .Include(x => x.user)
                .SingleOrDefault();
        }

        public void AddComment(UserForum userForum)
        {
            appContext.UsersForums.Add(userForum);
        }

        public List<UserForum> GetReplies(int parentId, int? page, int count = 3)
        {
            if (!page.HasValue)
            {
                return appContext.UsersForums
                    .Where(x => x.ParentId == parentId)
                    .Include(x => x.user)
                    .ToList();
            }
            else
            {
                return appContext.UsersForums
                    .Where(x => x.ParentId == parentId)
                    .Include(x => x.user)
                    .OrderByDescending(x => x.Id)
                    .Skip(page.Value * count)
                    .Take(count)
                    .ToList();
            }
        }

        public void DeleteComment(List<UserForum> userForum)
        {
            appContext.RemoveRange(userForum);
        }

        public void DeleteReply(UserForum userForum)
        {
            appContext.Remove(userForum);
        }

        public int GetReplyCountByDate(int? id, DateTime? date, bool Equal = true)
        {
            if (!id.HasValue)
            {
                if (date.HasValue && Equal)
                {
                    return appContext.UsersForums
                        .Where(x => x.ParentId != null && x.CreatedDate.Date == date)
                        .Count();
                }
                else if (date.HasValue && !Equal)
                {
                    return appContext.UsersForums
                        .Where(x => x.ParentId != null && x.CreatedDate.Date >= date)
                        .Count();
                }
                else
                    return appContext.UsersForums.Where(x => x.ParentId != null).Count();
            }
            else
            {
                if (date.HasValue && Equal)
                {
                    return appContext.UsersForums
                        .Where(
                            x => x.ParentId != null && x.CreatedDate.Date == date && x.forumId == id
                        )
                        .Count();
                }
                else if (date.HasValue && !Equal)
                {
                    return appContext.UsersForums
                        .Where(
                            x => x.ParentId != null && x.CreatedDate.Date >= date && x.forumId == id
                        )
                        .Count();
                }
                else
                    return appContext.UsersForums
                        .Where(x => x.ParentId != null && x.forumId == id)
                        .Count();
            }
        }

        public int GetCommentCountByDate(int? id, DateTime? date, bool Equal = true)
        {
            if (!id.HasValue)
            {
                if (date.HasValue && Equal)
                {
                    return appContext.UsersForums
                        .Where(x => x.ParentId == null && x.CreatedDate.Date == date.Value)
                        .Count();
                }
                else if (date.HasValue && !Equal)
                {
                    return appContext.UsersForums
                        .Where(x => x.ParentId == null && x.CreatedDate.Date >= date.Value)
                        .Count();
                }
                else
                    return appContext.UsersForums.Where(x => x.ParentId == null).Count();
            }
            else
            {
                if (date.HasValue && Equal)
                {
                    return appContext.UsersForums
                        .Where(
                            x =>
                                x.ParentId == null
                                && x.CreatedDate.Date == date.Value
                                && x.forumId == id
                        )
                        .Count();
                }
                else if (date.HasValue && !Equal)
                {
                    return appContext.UsersForums
                        .Where(
                            x =>
                                x.ParentId == null
                                && x.CreatedDate.Date >= date.Value
                                && x.forumId == id
                        )
                        .Count();
                }
                else
                    return appContext.UsersForums
                        .Where(x => x.ParentId == null && x.forumId == id)
                        .Count();
            }
        }

        public int GetUsersCountByDate(int? id, DateTime? date, bool Equal = true)
        {
            if (!id.HasValue)
            {
                if (date.HasValue && Equal)
                {
                    return appContext.UsersForums
                        .Where(x => x.CreatedDate.Date == date)
                        .Select(x => x.userId)
                        .Distinct()
                        .Count();
                }
                else if (date.HasValue && !Equal)
                {
                    return appContext.UsersForums
                        .Where(x => x.CreatedDate.Date >= date)
                        .Select(x => x.userId)
                        .Distinct()
                        .Count();
                }
                else
                    return appContext.UsersForums.Select(x => x.userId).Distinct().Count();
            }
            else
            {
                if (date.HasValue && Equal)
                {
                    return appContext.UsersForums
                        .Where(x => x.CreatedDate.Date == date && x.forumId == id)
                        .Select(x => x.userId)
                        .Distinct()
                        .Count();
                }
                else if (date.HasValue && !Equal)
                {
                    return appContext.UsersForums
                        .Where(x => x.CreatedDate.Date >= date && x.forumId == id)
                        .Select(x => x.userId)
                        .Distinct()
                        .Count();
                }
                else
                    return appContext.UsersForums
                        .Where(x => x.forumId == id)
                        .Select(x => x.userId)
                        .Distinct()
                        .Count();
            }
        }

        public List<UserForum> SearchComments(
            string[] query,
            int userLevel,
            int page,
            int count = 3
        )
        {
            IEnumerable<UserForum> list = new List<UserForum>();

            if (userLevel == 0)
            {
                foreach (string term in query)
                {
                    list = list.Concat(
                            (IEnumerable<UserForum>)
                                appContext.UsersForums
                                    .Include(x => x.forum)
                                    .Include(c => c.Replies)
                                    .Include(x => x.user)
                                    .Where(x => x.Comment.Contains(term) && x.ParentId == null)
                        )
                        .ToList();
                }
            }
            else
            {
                foreach (string term in query)
                {
                    list = list.Concat(
                            (IEnumerable<UserForum>)
                                appContext.UsersForums
                                    .Include(x => x.forum)
                                    .Include(c => c.Replies)
                                    .Include(x => x.user)
                                    .Where(
                                        x =>
                                            x.Comment.Contains(term)
                                            && x.ParentId == null
                                            && x.forum.LevelId <= userLevel
                                    )
                        )
                        .ToList();
                }
            }
            list = list.Distinct().OrderByDescending(x => x.Id).Skip(count * page).Take(count);

            foreach (var comment in list)
            {
                comment.Replies = comment.Replies.OrderByDescending(x => x.Id).Take(count).ToList();
            }

            return list.ToList();
        }

        public bool CheckMoreComments(
            int page,
            int? parentId = null,
            int? ForumId = null,
            int count = 3
        )
        {
            if (parentId.HasValue)
            {
                return appContext.UsersForums
                    .Where(u => u.ParentId == parentId.Value)
                    .Skip(page * count)
                    .Take(count)
                    .Any();
            }
            else if (ForumId.HasValue && parentId == null)
            {
                return appContext.UsersForums
                    .Where(u => u.forumId == ForumId.Value && u.ParentId == null)
                    .Skip(page * count)
                    .Take(count)
                    .Any();
            }

            return false;
        }

        ////////////////////////// added by me




        public List<Forum> GetSpecificForumByLevelId(int levelId)
        {
            try
            {
                
                if (levelId==4)
                {
                    return appContext.Forums
           .Where(y =>  y.LevelId == 4)
           .Include(y => y.level)
           .ToList();
                }
                else
                {
            var x=     appContext.Forums
       .Where(y => /*y.LevelId ==4 ||*/ y.LevelId   <= levelId )
       .Include(y => y.level)
       .ToList();
                    return x;
                }


            }
            catch (Exception)
            {
                return new List<Forum>();
            }
        }
    }
}
