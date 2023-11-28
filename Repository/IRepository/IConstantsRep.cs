using ESTA.Models;

namespace ESTA.Repository.IRepository
{
	public interface IConstantsRep
	{


		Task<double> getMempershipFee();
        Task<int> getMempershipExpiryMonth();
        Task<GlobalConstants> getConstants();

        bool UpdateConstants(GlobalConstants constants);
    }
}
