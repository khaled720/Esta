using ESTA.Models;
using ESTA.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace ESTA.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize("RequireAdminRole")]
    public class LevelsController : Controller
	{
        private readonly IUnitOfWork appRep;

        public LevelsController(IUnitOfWork appRep)
        {
            this.appRep = appRep;
        }
        public async Task<IActionResult> Index()
        {
            var levels = await appRep.LevelRep.GetAllLevels();
            return View(levels);
        }


        [HttpGet]
        public IActionResult AddLevel()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddLevel(Level level)
        {
            await appRep.LevelRep.AddLevel(level);
            var isAdded = await appRep.SaveChangesAsync();
            if (isAdded)
            {

                return RedirectToAction("Index");

            }
            else
            {
                return View(level);
            }

        }


        [HttpGet]
        public IActionResult EditLevel(int id)
        {

           var level= appRep.LevelRep.GetLevel(id); 

            return View(level);
        }

        [HttpPost]
        public async Task<IActionResult> EditLevel(Level level)
        {
           
         await   appRep.LevelRep.EditLevel(level);
         await   appRep.SaveChangesAsync();

            return RedirectToAction("Index");
          

        }


    }
}
