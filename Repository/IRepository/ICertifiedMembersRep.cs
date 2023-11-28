using ESTA.Areas.Admin.Models;

namespace ESTA.Repository.IRepository
{
    public interface ICertifiedMembersRep
    {

       /// <summary>
       /// 
       /// </summary>
       /// <param name="x">Member to Be Added</param>
       /// <returns>true if added,false if not</returns>
       Task<bool>  AddMember(CertifiedMember x);

      Task<bool>  IsCertifiedMember(string name);

       Task<bool> DeleteMember(int Id);

        Task<bool> UpdateMember(CertifiedMember x);

        Task<List<CertifiedMember>> GetAllMembers();

        Task<CertifiedMember> GetMember(int id);
    }
}
