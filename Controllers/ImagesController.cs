using ESTA.Helpers;
using ESTA.Models;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ESTA.Controllers
{
    public class Image{ public string? imageURL { get; set; } public string? imageName { get; set; } }

    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
       
        private readonly IWebHostEnvironment webHost;

        public ImagesController(IWebHostEnvironment webHost)
        {
        
            this.webHost = webHost;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] string imageURL)
        {
            return Content("Success Get was Called");

        }


        [HttpPost]
        public async Task<string> PostAsync([FromForm] IFormFile file)
        {
            var path = webHost.WebRootPath;
            var saveFile = Path.Combine(path + "/Images/Editor/");
            var ImageName = await FileUpload.SavePhotoAsync(file, file.Name, saveFile);
            var display = "/Images/Editor/" + ImageName;
            return display;

        }



        //[HttpPost]
        //public string PostImg([FromForm] IFormFile file)
        //{
        //    string ImageName = ImageHelper.UploadedFile(file, "editor_images");
        //    var display = "/editor_images/" + ImageName;
        //    return display;

        //}

        [HttpDelete]
        public void Delete([FromForm] string imgPath)
        {
            var path = webHost.WebRootPath;
            imgPath = imgPath.Remove(0,1);
            var saveFile = Path.Combine(path, imgPath);
            FileInfo file = new (saveFile);
            try
            {
                if (file.Exists)//check file exsit or not
                {
                    file.Delete();
                }
            }
            catch (Exception)
            {
                
          
            }

        }



    }
}
