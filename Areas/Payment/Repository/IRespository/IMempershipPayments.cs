using ESTA.Areas.Payment.Models;

namespace ESTA.Areas.Payment.Repository.IRespository
{
    public interface IMempershipPayments
    {
        Task<bool> SaveGetOrder(MempershipPayment membershipPayment);

       Task<List<MempershipPayment>> GetMempershipPayments();
    }
}
