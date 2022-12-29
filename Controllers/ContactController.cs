using ESTA.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace ESTA.Controllers
{
    public class ContactController : Controller
    {
        private readonly IUnitOfWork uow;

        public ContactController(IUnitOfWork Uow)
        {
            this.uow = Uow;
        }
        public async Task<IActionResult> IndexAsync()
        {
          var contacts=await  uow.ContactRep.GetAllContacts();
            return View(contacts);
        }
    }
}
