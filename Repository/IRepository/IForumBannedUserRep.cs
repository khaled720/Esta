using ESTA.Models;

namespace ESTA.Repository.IRepository
{
    public interface IForumBannedUserRep
    {
        public List<ForumBannedUser> GetAllBannedUser();
        public List<ForumBannedUser> GetAllUnBannedUser();
        public List<ForumBannedUser> GetForumsByUserId(string UserId); //forums that user banned from
        public bool IsUserBanned(string UserId, int ForumId);
        public ForumBannedUser? GetBannedUserById(int id);
        public void NewBannedUser(ForumBannedUser bannedUser);
        public void RemoveBannedUser(ForumBannedUser bannedUser);
    }
}
