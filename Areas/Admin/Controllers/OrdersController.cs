using ESTA.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace ESTA.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrdersController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public OrdersController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Courses()
        {
            var result = await unitOfWork.CourseOrdersRep.GetCoursesOrders();
            return View(result);
        }
        public async Task<IActionResult> Memperships()
        {
            var result = await unitOfWork.MempershipOrdersRep.GetMempershipOrders();

            return View(result);
        }


    }
}
