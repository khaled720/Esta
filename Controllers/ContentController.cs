using ESTA.Models;
using ESTA.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace ESTA.Controllers
{
    public class ContentController : Controller
    {
        private readonly IUnitOfWork appRep;

        public ContentController(IUnitOfWork appRep) 
        {
            this.appRep = appRep;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> About()
        {

            var about = await  appRep.ContentRep.GetContent("about");


            return View(about);
        }
        [HttpPost]
        public async Task<IActionResult> About(Content content)
        {
           

            try
            {
                if (content.Id == 0)
                {
                    content.Type = "about";
                    await appRep.ContentRep.AddContent(content);
                    await appRep.SaveChangesAsync();
                }
                else {

                    await appRep.ContentRep.UpdateContent(content);
                    await appRep.SaveChangesAsync();
                
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", ex.Message);
                return View();  
            }


            return RedirectToAction("Index");
        }




        [HttpGet]
        public async Task<IActionResult> TechnicalAnalysis()
        {
            var ta =  await  appRep.ContentRep.GetContent("ta");


        
            return View(ta);
        }
        [HttpPost]
        public async Task<IActionResult> TechnicalAnalysis(Content content)
        {

            try
            {
                if (content.Id == 0)
                {
                    content.Type = "ta";
                    await appRep.ContentRep.AddContent(content);
                    await appRep.SaveChangesAsync();
                }
                else
                {

                    await appRep.ContentRep.UpdateContent(content);
                    await appRep.SaveChangesAsync();

                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", ex.Message);
                return View();
            }


            return RedirectToAction("Index");
        }







        [HttpGet]
        public async Task<IActionResult> Ethics()
        {
            var ethics = await  appRep.ContentRep.GetContent("ethics");



            return View(ethics);
        }
        [HttpPost]
        public async Task<IActionResult> Ethics(Content content)
        {

            try
            {
                if (content.Id == 0)
                {
                    content.Type = "ethics";
                    await appRep.ContentRep.AddContent(content);
                    await appRep.SaveChangesAsync();
                }
                else
                {

                    await appRep.ContentRep.UpdateContent(content);
                    await appRep.SaveChangesAsync();

                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", ex.Message);
                return View();
            }


            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<IActionResult> Ifta()
        {
            var ifta = await appRep.ContentRep.GetContent("ifta");



            return View(ifta);
        }
        [HttpPost]
        public async Task<IActionResult> Ifta(Content content)
        {

            try
            {
                if (content.Id == 0)
                {
                    content.Type = "ifta";
                    await appRep.ContentRep.AddContent(content);
                    await appRep.SaveChangesAsync();
                }
                else
                {

                    await appRep.ContentRep.UpdateContent(content);
                    await appRep.SaveChangesAsync();

                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", ex.Message);
                return View();
            }


            return RedirectToAction("Index");
        }







        [HttpGet]
        public async Task<IActionResult> Benefits()
        {
            var benefits = await appRep.ContentRep.GetContent("benefits");



            return View(benefits);
        }
        [HttpPost]
        public async Task<IActionResult> Benefits(Content content)
        {

            try
            {
                if (content.Id == 0)
                {
                    content.Type = "benefits";
                    await appRep.ContentRep.AddContent(content);
                    await appRep.SaveChangesAsync();
                }
                else
                {

                    await appRep.ContentRep.UpdateContent(content);
                    await appRep.SaveChangesAsync();

                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", ex.Message);
                return View();
            }


            return RedirectToAction("Index");
        }








    }
}
