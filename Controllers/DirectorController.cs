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




        public async Task<IActionResult> BoardofDirectors()
        {
            var directors=await uow.DirectorRep.GetAllDirectors();

            return View(directors);
        }



    }
}
