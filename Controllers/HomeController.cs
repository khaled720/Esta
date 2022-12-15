﻿using System.Diagnostics;
using ESTA.Models;
using ESTA.Repository;
using ESTA.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace ESTA.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAppRep appRep;
        private readonly IWebHostEnvironment hostEnvironment;
        public HomeController(ILogger<HomeController> logger,IAppRep appRep, IWebHostEnvironment hostEnvironment)
        {
            _logger = logger;
            this.appRep = appRep;
            this.hostEnvironment = hostEnvironment;
            
        }

        public async Task<IActionResult> Index()
        {
            List<Course> courses = (List<Course>)await appRep.CoursesRep.GetAllCourses();
            return View(courses);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}