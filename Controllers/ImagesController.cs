using ESTA.Models;
using Microsoft.AspNetCore.Mvc;

namespace TestMvc2.Controllers
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
        public async Task<string> PostAsync( [FromForm]IFormFile file) 
        {
            var path = webHost.WebRootPath;
            var saveFile= Path.Combine(path+ "/Images/Editor/");
         var ImageName=   await FileUpload.SavePhotoAsync(file, file.Name, saveFile);
            var display = "../Images/Editor/" + ImageName;
            return display;

        }


        [HttpDelete]
        public void Delete(string ImgPath)
        {
            try
            {
                System.IO.File.Delete(ImgPath);
            }
            catch (Exception)
            {
                
          
            }

        }



    }
}
