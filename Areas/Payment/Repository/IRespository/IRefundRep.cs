using ESTA.Areas.Payment.Models;

namespace ESTA.Areas.Payment.Repository.IRespository
{
    public interface IRefundRep
    {


     Task<List<Refund>>  GetAllRefundRequests();
     Task<List<Refund>>   GetSpecificUserRefundRequests(string UserId);

     Task<Refund>  GetRefundRequest(int Id);

        Task<int> GetMaxId();

     Task<bool> AddRefundRequest(Refund refundrequest);

    Task< bool>   UpdateRefundStatus(int RefundRequestId,string newState);

    }
}
