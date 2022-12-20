using ESTA.Models;
using ESTA.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ESTA.Repository
{
    public class RoleRep : IRoleRep
    {
        private readonly AppDbContext appContext;
        private readonly RoleManager<IdentityRole> roleManager;

        public RoleRep(AppDbContext appContext , RoleManager<IdentityRole> roleManager)
        {
            this.appContext = appContext;
            this.roleManager = roleManager;
        }
        public async Task<bool> AddRole(String RoleName)
        {
           var result= await roleManager.CreateAsync(new IdentityRole(RoleName));
            if (result.Succeeded)
            {
                return true;
            }
            return false;
        }

        public bool DeleteRole(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> EditRole(IdentityRole role)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<IdentityRole>> GetAllRoles()
        {
        var roles= await  roleManager.Roles.ToListAsync();
            return roles;   
        
        }

        public Task<IdentityRole> GetRole(int id)
        {
            throw new NotImplementedException();
        }
    }
}
