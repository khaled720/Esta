using ESTA.Areas.Payment.Models;
using ESTA.Areas.Payment.Repository.IRespository;
using ESTA.Models;
using ESTA.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace ESTA.Areas.Payment.Repository
{
    public class RefundRep:IRefundRep
    {
        private readonly AppDbContext appRep;

        public RefundRep(AppDbContext dbContext)
        {
            this.appRep = dbContext;
        }

        public async Task<bool> AddRefundRequest(Refund refundrequest)
        {
            try
            {
       //         return await appRep.RefundRep.AddRefundRequest(refundrequest);

        await     appRep.RefundRequests.AddAsync(refundrequest);
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<Refund>> GetAllRefundRequests()
        {

            return await appRep.RefundRequests.ToListAsync();

        
        }

        public async Task<int> GetMaxId()
        {
            try
            {
               return await  appRep.RefundRequests.MaxAsync(y=>y.Id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Refund> GetRefundRequest(int Id)
        {
            try
            {

                return await appRep.RefundRequests.Include(y=>y.User)
                    .Where(y => y.Id == Id).FirstAsync();
            }

            catch (Exception)
            {
                return null;
            }
        }
        //migration
        public async Task<List<Refund>> GetSpecificUserRefundRequests(string UserId)
        {
        return await    appRep.RefundRequests.Where(y=>y.UserId==UserId).ToListAsync();
        }

        public async Task<bool> UpdateRefundStatus(int RefundRequestId, string newState)
        {
            try
            {
             var result=await   appRep.RefundRequests.FindAsync(RefundRequestId); 
            result.Status = newState;
                appRep.RefundRequests.Update(result);
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

   
    }
}
