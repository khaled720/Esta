using ESTA.Models;
using ESTA.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace ESTA.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ContactController : Controller
    {
        private readonly IUnitOfWork uow;

        public ContactController(IUnitOfWork Uow)
        {
            uow = Uow;
        }
        public async Task<IActionResult> Index()
        {
            var contacts = await uow.ContactRep.GetAllContacts();
            return View(contacts);
        }


        [HttpGet]
        public IActionResult AddContact()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddContact(Contact contact)
        {
            try
            {
                await uow.ContactRep.AddContact(contact);
                await uow.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return View(contact);

            }


        }


        [HttpGet]
        public async Task<IActionResult> EditContact(int id)
        {
            var contact = await uow.ContactRep.GetContact(id);

            return View(contact);
        }
        [HttpPost]
        public async Task<IActionResult> EditContact(Contact contact)
        {
            try
            {
                await uow.ContactRep.EditContact(contact);
                await uow.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return View(contact);

            }


        }

        [HttpGet]
        public IActionResult DeleteContact(int id)
        {


            uow.ContactRep.DeleteContact(id);

            return RedirectToAction("Index");






        }



    }
}
