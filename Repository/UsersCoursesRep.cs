using ESTA.Models;
using ESTA.Repository.IRepository;

namespace ESTA.Repository
{
    public class UsersCoursesRep : IUsersCoursesRep
    {
        private readonly AppDbContext appDbContext;

        public UsersCoursesRep(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
    }
}
