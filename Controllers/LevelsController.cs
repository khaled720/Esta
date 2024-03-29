﻿using ESTA.Models;
using ESTA.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace ESTA.Controllers
{
    public class LevelsController : Controller
    {
        private readonly IUnitOfWork appRep;

        public LevelsController(IUnitOfWork appRep)
        {
            this.appRep = appRep;
        }
        public async Task<IActionResult> Index()
        {
          var levels =await appRep.LevelRep.GetAllLevels();
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



    }
}
