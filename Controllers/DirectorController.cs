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

        public DirectorController(IUnitOfWork Uow, IWebHostEnvironment hostEnvironment)
        {
            uow = Uow;
            this.hostEnvironment = hostEnvironment;
        }




   public async Task<IActionResult> BoardofDirectors()
        {
            var directors=await uow.DirectorRep.GetAllDirectors();

            return View(directors);
        }


        public async Task<IActionResult> Index()
        {
            var directors=await uow.DirectorRep.GetAllDirectors();

            return View(directors);
        }





        public IActionResult AddDirector()
        {
            return View(new Director());
        }
         
        [HttpPost]
        public async Task<IActionResult> AddDirector(Director director)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (director.Photo != null)
                    {
                        var SavePath = hostEnvironment.WebRootPath + "/Images/Directors/";
                        var PhotoName = await FileUpload.SavePhotoAsync(
                            director.Photo,
                            director.NameEn,
                            SavePath
                        );
                        if (!String.IsNullOrEmpty(PhotoName))
                        {
                            director.PhotoPath = "/Images/Directors/" + PhotoName;
                        }
                        else
                        {
                            director.PhotoPath = "/Images/Directors/person.jpg";
                        }

                    }
                    else {
                        director.PhotoPath = "/Images/Directors/person.jpg";
                    }


                    await uow.DirectorRep.AddDirector(director);
                    await uow.SaveChangesAsync();

                }
                catch (Exception)
                {
                   // ModelState.AddModelError(,)
                    return View(director);
                }

                return RedirectToAction("Index");

            }
            else
            {
                return View(director);
            }
        }



        public async Task<IActionResult> DeleteDirector(int id)
        {
            if (uow.DirectorRep.DeleteDirector(id)) 
            {  
                await uow.SaveChangesAsync();
            return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }




        public async Task<IActionResult> EditDirector(int id)
        {
            try
            {
           var director =await uow.DirectorRep.GetDirector(id);
                return View(director);
            }
            catch (Exception)
            {

                return NotFound();
            }


           
      
        }

        [HttpPost]
        public async Task<IActionResult> EditDirector(Director director)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (director.Photo != null)
                    {
                        var SavePath = hostEnvironment.WebRootPath + "/Images/Directors/";
                        var PhotoName = await FileUpload.SavePhotoAsync(
                            director.Photo,
                            director.NameEn,
                            SavePath
                        );
                        if (!String.IsNullOrEmpty(PhotoName))
                        {
                              director.PhotoPath = "/Images/Directors/" + PhotoName;

                        }

                    }

                    if (await uow.DirectorRep.EditDirector(director))
                    {
                        await uow.SaveChangesAsync();
                    }
                    else {
                        ModelState.AddModelError("Can not Edit", "Cant not Update this Director");
                        return View(director);
                    }
                  

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Can not Edit", ex.Message);
                    return View(director);
                }

                return RedirectToAction("Index");

            }
            else
            {
                return View(director);
            }
        }



    }
}
