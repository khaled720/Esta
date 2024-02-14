using ESTA.Areas.Payment.Models;
using System;
using ESTA.Areas.Payment.Repository.IRespository;
using ESTA.Models;
using Microsoft.EntityFrameworkCore;

namespace ESTA.Areas.Payment.Repository
{
    public class MempershipOrdersRep :IMempershipOrders
    {
        private readonly AppDbContext appContext;

        public MempershipOrdersRep( AppDbContext appContext) 
        {
            this.appContext = appContext;
        }

        public async Task<int> GetMaxId()
        {
            try
            {
                var result =await appContext.MempershipOrders.MaxAsync(y=>y.Id);
                return result;
            }
            catch (Exception)
            {

                return new Random().Next(2000, 15000);
            }
        }

        public async Task<List<MempershipOrder>> GetMempershipOrders()
        {
            try
            {
                return await appContext.MempershipOrders.ToListAsync(); 
            }
            catch (Exception)
            {
return new List<MempershipOrder>();
            }
        }

        public async Task<MempershipOrder> GetOrder(int orderId)
        {
            try
            {
         return await appContext.MempershipOrders.FindAsync(orderId)??new MempershipOrder();

            }
            catch (Exception)
            {

                throw;
            }
   
        }

        public async Task<int> GetOrdersLength()
        {
            try
            {
                return await appContext.MempershipOrders.CountAsync();
            }
            catch (Exception)
            {
                return 0;

            }
        }

        public async Task<bool> SavePrepareOrder(MempershipOrder mempershipOrder)
        {

          await  appContext.MempershipOrders.AddAsync(mempershipOrder);

            return true;

        }

        public bool UpdatePrepareOrder(MempershipOrder mempershipOrder)
        {
             appContext.MempershipOrders.Update(mempershipOrder);

            return true;
        }
    }
}
