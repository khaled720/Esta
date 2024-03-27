using ESTA.Models;
using ESTA.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace ESTA.Areas.Admin.Controllers
{
	[Area("Admin")]
    [Authorize("RequireAdminRole")]
    public class ConstantsController : Controller
	{
		private readonly IUnitOfWork appContext;

		public ConstantsController(IUnitOfWork uniteOfWork)
		{
			this.appContext = uniteOfWork;
		}
		public async Task<IActionResult> EditMempership()
		{

			var constantsrow=await appContext.ConstantsRep.getConstants();

			return View(constantsrow);
		}
		[HttpPost]
        public async Task<IActionResult>  EditMempership(GlobalConstants constants)
        {
			try
			{
            var constantsrow =  appContext.ConstantsRep.UpdateConstants(constants);
			await	appContext.SaveChangesAsync();
			return RedirectToAction("Index","Home",new {area="Admin"});
			}
			catch (Exception)
			{
                return RedirectToAction("EditMempershipFee", " Constants", new { area = "Admin" });

            }

        }


    }
}
