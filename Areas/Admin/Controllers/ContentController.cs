using ESTA.Models;
using ESTA.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace ESTA.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize("RequireAdminRole")]
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
        public  IActionResult About()
        {

            var about =  appRep.ContentRep.GetContent("about");


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
        public IActionResult TechnicalAnalysis()
        {
            var ta =  appRep.ContentRep.GetContent("ta");



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
                   await  appRep.ContentRep.AddContent(content);
                    await appRep.SaveChangesAsync();
                }
                else
                {

                await     appRep.ContentRep.UpdateContent(content);
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
        public IActionResult Ethics()
        {
            var ethics =  appRep.ContentRep.GetContent("ethics");



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
        public IActionResult Ifta()
        {
            var ifta =  appRep.ContentRep.GetContent("ifta");



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
        public IActionResult Benefits()
        {
            var benefits =  appRep.ContentRep.GetContent("benefits");
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




        [HttpGet]
        public IActionResult Vission()
        {
            var benefits =  appRep.ContentRep.GetContent("vission");
            return View(benefits);
        }
        [HttpPost]
        public async Task<IActionResult> Vission(Content content)
        {

            try
            {
                if (content.Id == 0)
                {
                    content.Type = "vission";
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
        public IActionResult Mission()
        {
            var benefits =  appRep.ContentRep.GetContent("mission");



            return View(benefits);
        }
        [HttpPost]
        public async Task<IActionResult> Mission(Content content)
        {

            try
            {
                if (content.Id == 0)
                {
                    content.Type = "mission";
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
        public IActionResult CETA()
        {
            var benefits = appRep.ContentRep.GetContent("CETA");



            return View(benefits);
        }
        [HttpPost]
        public async Task<IActionResult> CETA(Content content)
        {

            try
            {
                if (content.Id == 0)
                {
                    content.Type = "CETA";
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
