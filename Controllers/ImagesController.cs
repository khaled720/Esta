using Microsoft.AspNetCore.Mvc;

namespace ESTA.Controllers
{
    public class Image { public string? imageURL { get; set; } public string? imageName { get; set; } }

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
        public string PostImg([FromForm] IFormFile file)
        {
            var st = file.OpenReadStream();

            BinaryReader Br = new BinaryReader(st);

            var bt = Br.ReadBytes((int)st.Length);
            //   var base64str= Convert.ToBase64String(bt);
            var path = webHost.WebRootPath;
            var ImageName = file.FileName.Trim();

            var saveFile = Path.Combine(path + "/editor_images", ImageName);
            var display = "/editor_images/" + ImageName;

            System.IO.File.WriteAllBytes(saveFile, bt);


            return display;

        }


        [HttpDelete]
        public void Delete([FromForm] string imgPath)
        {
            var path = webHost.WebRootPath;

            var saveFile = Path.Combine(path, "wwwroot\\" + imgPath);
            FileInfo file = new FileInfo(saveFile);
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
