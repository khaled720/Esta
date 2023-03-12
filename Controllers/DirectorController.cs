using ESTA.Models;
using ESTA.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace ESTA.Controllers
{
   // [Authorize("RequireAdminRole")]
   
    public class DirectorController : Controller
    {
        private readonly IUnitOfWork uow;
        private readonly IWebHostEnvironment hostEnvironment;


        public DirectorController(IUnitOfWork uow, IWebHostEnvironment hostEnvironment)
        {
            this.uow = uow;
            this.hostEnvironment = hostEnvironment;
        }

        public async Task<IActionResult> BoardofDirectors()
        {
            try
            {
 

            return View(await uow.DirectorRep.GetAllDirectors());
            }
            catch (Exception)
            {
                return View(new List<Director>());

            }
         
        }



    }
}
