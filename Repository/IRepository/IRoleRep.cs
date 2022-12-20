using ESTA.Models;
using Microsoft.AspNetCore.Identity;

namespace ESTA.Repository.IRepository
{
    public interface IRoleRep
    {


        public Task<IEnumerable<IdentityRole>> GetAllRoles();
        public Task<IdentityRole> GetRole(int id);

        public Task<bool> AddRole(String RoleName );

        public Task<bool> EditRole(IdentityRole role);
        public bool DeleteRole(int id);

    }
}
