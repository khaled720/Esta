using ESTA.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace ESTA.Controllers
{
    public class AdminController : Controller
    {
        private readonly IUnitOfWork uow;

        public AdminController(IUnitOfWork Uow)
        {
            this.uow = Uow;
        }
        public IActionResult Index()
        {
            return View();
        }


    
    }
}
