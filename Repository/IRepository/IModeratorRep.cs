using ESTA.Models;

namespace ESTA.Repository.IRepository
{
    public interface IModeratorRep
    {
        public void NewModeratorForum(ModeratorForum moderator);
        public void RemoveModeratorForum(ModeratorForum moderator);
        public List<ModeratorForum> GetModeratorForumById(string id);
    }
}
