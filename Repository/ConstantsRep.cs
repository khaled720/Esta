using ESTA.Models;
using ESTA.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace ESTA.Repository
{
	public class ConstantsRep : IConstantsRep
	{
		private readonly AppDbContext appDbContext;

		public ConstantsRep(AppDbContext appDbContext)
		{
			this.appDbContext = appDbContext;
		}

		public async Task<GlobalConstants> getConstants()
		{
			try
			{

				var cons = await appDbContext.Constants.FirstAsync();
				return cons;

			}
			catch (Exception)
			{
				return null;
			}
		}

		public async Task<int> getMempershipExpiryMonth()
		{
			try
			{
			
			var result =await appDbContext.Constants.FirstAsync();
			return result.MempershipExpiryMonth;

			}
			catch (Exception ex)
			{

				throw;
			}
		}

		public  async Task<double> getMempershipFee()
		{
			try
			{
			var constant = await	appDbContext.Constants.FirstAsync();
			return constant.MempershipFee;
			}
			catch (Exception)
			{

				throw;
			}

		}

		public bool UpdateConstants(GlobalConstants constants)
		{
			try
			{
				appDbContext.Constants.Update(constants);
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}
	}
}
