using ESTA.Models;
using ESTA.Repository.IRepository;

namespace ESTA.Repository
{
    public class ModeratorRep : IModeratorRep
    {
        private readonly AppDbContext context;

        public ModeratorRep(AppDbContext context)
        {
            this.context = context;
        }

        public List<ModeratorForum> GetModeratorForumById(string id)
        {
            return context.ModeratorForums.Where(x=>x.UserId == id).ToList();
        }

        public void NewModeratorForum(ModeratorForum moderator)
        {
            context.ModeratorForums.Add(moderator);
        }

        public void RemoveModeratorForum(ModeratorForum moderator)
        {
            context.ModeratorForums.Remove(moderator);
        }
    }
}
