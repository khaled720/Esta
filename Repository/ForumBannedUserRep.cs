using ESTA.Models;
using ESTA.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace ESTA.Repository
{
    public class ForumBannedUserRep : IForumBannedUserRep
    {
        private readonly AppDbContext context;

        public ForumBannedUserRep(AppDbContext context)
        {
            this.context = context;
        }

        public List<ForumBannedUser> GetAllBannedUser()
        {
            return context.ForumBannedUser
                .Where(x => x.Active == 1)
                .Include(x => x.User)
                .Include(x => x.Mod)
                .Include(x => x.Forum)
                .ToList();
        }

        public List<ForumBannedUser> GetAllUnBannedUser()
        {
            return context.ForumBannedUser
                .Where(x => x.Active == 0)
                .Include(x => x.User)
                .Include(x => x.Mod)
                .Include(x => x.Forum)
                .ToList();
        }

        public ForumBannedUser? GetBannedUserById(int id)
        {
            return context.ForumBannedUser
                .Where(x => x.Id == id)
                .Include(x => x.User)
                .Include(x => x.Mod)
                .Include(x => x.Forum)
                .SingleOrDefault();
        }

        public List<ForumBannedUser> GetForumsByUserId(string UserId)
        {
            return context.ForumBannedUser
                .Where(x => x.UserId == UserId && x.Active == 1)
                .ToList();
        }

        public bool IsUserBanned(string UserId, int ForumId)
        {
            return context.ForumBannedUser
                .Where(x => x.UserId == UserId && x.ForumId == ForumId && x.Active == 1)
                .Any();
        }

        public void NewBannedUser(ForumBannedUser bannedUser)
        {
            context.ForumBannedUser.Add(bannedUser);
        }

        public void RemoveBannedUser(ForumBannedUser bannedUser)
        {
            context.ForumBannedUser.Remove(bannedUser);
        }
    }
}
