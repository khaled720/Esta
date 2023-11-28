using ESTA.Areas.Payment.Models;
using System;

namespace ESTA.Areas.Payment.Repository.IRespository
{
    public interface IMempershipOrders
    {
    Task<bool> SavePrepareOrder(MempershipOrder mempershipOrder);

     Task< List<MempershipOrder>>  GetMempershipOrders();
        Task<int> GetOrdersLength();
        Task<int> GetMaxId();
        bool UpdatePrepareOrder(MempershipOrder mempershipOrder);
          Task<MempershipOrder> GetOrder(int orderId);

    }
}
