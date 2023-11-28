using ESTA.Areas.Admin.Models;
using ESTA.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ESTA.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize(Roles ="Admin" )]
    public class CertifiedMembersController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public CertifiedMembersController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        // GET: CertifiedMembersController
        public async Task<ActionResult> Index()
        {
            var members= await unitOfWork.CertifiedMempersRep.GetAllMembers();

            return View(members);
        }

        // GET: CertifiedMembersController/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}





        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Add(string name)
        {
            try
            {
                if (! await unitOfWork.CertifiedMempersRep.IsCertifiedMember(name))
                {
                    await unitOfWork.CertifiedMempersRep.AddMember(new CertifiedMember() { Name = name });
                    await unitOfWork.SaveChangesAsync();
                }

                return RedirectToAction("Index","Users",new { area="Admin"});
            }
            catch (Exception e)
            {
                return RedirectToAction("Index", "Users", new { area = "Admin" });
            }
        }





        // GET: CertifiedMembersController/Create
        public ActionResult Create()
        {
            return View();
        }
                // POST: CertifiedMembersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CertifiedMember certifiedMember)
        {
            try
            {

           await     unitOfWork.CertifiedMempersRep.AddMember(certifiedMember);
            await    unitOfWork.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch(Exception e)
            {
                return View();
            }
        }

        // GET: CertifiedMembersController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
              var member= await   unitOfWork.CertifiedMempersRep.GetMember(id);
              if(member==null)return RedirectToAction(nameof(Index));


            return View(member);
        }

        // POST: CertifiedMembersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult>  Edit(CertifiedMember certifiedMember)
        {
            try
            {

              await   unitOfWork.CertifiedMempersRep.UpdateMember(certifiedMember);
              await   unitOfWork.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CertifiedMembersController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await unitOfWork.CertifiedMempersRep.DeleteMember(id);

                await unitOfWork.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(Index));
            }
        }

        
        }
    
}
