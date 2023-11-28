using System;
using ESTA.Areas.Payment.Models;
using ESTA.Areas.Payment.Repository.IRespository;
using ESTA.Models;
using Microsoft.EntityFrameworkCore;

namespace ESTA.Areas.Payment.Repository
{
    public class MempershipPaymentsRep : IMempershipPayments

    {
        private readonly AppDbContext appContext;

        public MempershipPaymentsRep(AppDbContext appContext)
        {
            this.appContext = appContext;
        }

        public async Task<List<MempershipPayment>> GetMempershipPayments()
        {
            try
            {
                return await appContext.MempershipPayments.Include(y=>y.User).ToListAsync();
            }
            catch (Exception)
            {
                return new List<MempershipPayment>();
            }
        }

        public Task<bool> SaveGetOrder(MempershipPayment membershipPayment)
        {
            appContext.MempershipPayments.Add(membershipPayment);

            return Task.FromResult(true);
        }
    }
}
