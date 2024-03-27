using AutoMapper.Configuration.Conventions;
using ESTA.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESTA.Areas.Admin.Controllers
{
	[Area("Admin")]
    [Authorize("RequireAdminRole")]
    public class PaymentsController : Controller
	{
		private readonly IUnitOfWork uow;

		public PaymentsController(IUnitOfWork uow)
		{
			this.uow = uow;
		}
		public IActionResult Index()
		{

			return View();
		}

            public async Task<IActionResult> Courses()
		{
            var result =await  uow.CoursePaymentsRep.GetCoursesPayments();
			return View(result);
		}
        public async Task<IActionResult> Memperships()
        {
		var result=await	uow.MempershipPaymentsRep.GetMempershipPayments();

            return View(result);
        }


///		type       0 means course   1 means mempership
		public async Task<IActionResult> orderDetails(int id,int type) 
		{
			switch (type)
			{

				case 0:


				var order=	await uow.CourseOrdersRep.GetOrder(id);
					return View(order);

				
				case   1 :

                    var memorder = await uow.MempershipOrdersRep.GetOrder(id);
                    return View(memorder);

					
			}
			return RedirectToAction("index","payments");
		}
        






    }
}
