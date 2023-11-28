using ESTA.Areas.Admin.Models;
using ESTA.Models;
using ESTA.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace ESTA.Repository
{
    public class CertifiedMembersRep :ICertifiedMembersRep
    {
        private readonly AppDbContext appContext;

        public CertifiedMembersRep(AppDbContext appContext )
        {
            this.appContext = appContext;
        }

        public async Task<bool> AddMember(CertifiedMember x)
        {

            try
            {
await appContext.CertifiedMembers.AddAsync(x);
                return true;
            }
            catch (Exception)
            {
                return false;
            }


        }

        public async Task<bool> DeleteMember(int Id)
        {
            try
            {
              appContext.CertifiedMembers.Remove(
                  await appContext.CertifiedMembers.AsQueryable().Where(y => y.Id == Id).FirstAsync()
                  );
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<CertifiedMember>> GetAllMembers()
        {
            try
            {
            return   await  appContext.CertifiedMembers.ToListAsync();
            }
            catch (Exception)
            {
                return new List<CertifiedMember>();
            }
        }

        public async Task<CertifiedMember> GetMember(int id)
        {
            try
            {
                return await appContext.CertifiedMembers.AsQueryable().Where(y => y.Id == id).FirstAsync();

            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> IsCertifiedMember(string name)
        {
            try
            {
       return await         appContext.CertifiedMembers.AsQueryable()
                    .Where(y => y.Name.Trim() == name.Trim()).AnyAsync();
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdateMember(CertifiedMember x)
        {
            try
            {
           var user= await appContext.CertifiedMembers.AsQueryable().Where(y => y.Id == x.Id).AsTracking().FirstAsync();

                user.Name = x.Name;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
