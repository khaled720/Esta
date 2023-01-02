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


        public async Task<IActionResult> UsersApproval()
        {
            var users=await uow.UserRep.GetAllUsers();




            return View(users);
        }
        public async Task<IActionResult> EditApproval(string id,bool isApproved)
        {
           await uow.UserRep.EditUserApproval(id,isApproved);
          await  uow.SaveChangesAsync();



            return RedirectToAction("Index");
        }

    }
}
