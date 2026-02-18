using Microsoft.AspNetCore;
using Serilog;

namespace ESTA.Helpers
{
    public static class ImageHelper
    {
        public static IWebHostEnvironment _webHost;
        public static void Configure(IWebHostEnvironment webHost)
        {
            _webHost = webHost;
        }
        public static string UploadedFile(IFormFile Image, string path)
        {
            string uniqueFileName;

            string uploadsFolder = Path.Combine(_webHost.WebRootPath, path);
            uniqueFileName = Guid.NewGuid().ToString() + "_" + Image.FileName;
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                Image.CopyTo(fileStream);
            }
            return uniqueFileName;
        }
        public static void DeleteFile(string path,string FileName)
        {
            string uploadsFolder = Path.Combine(_webHost.WebRootPath, path);
            string filePath = Path.Combine(uploadsFolder, FileName);
            FileInfo file = new(filePath);
            try
            {
                if (file.Exists)//check file exsit or not
                {
                    file.Delete();
                }
            }
            catch (Exception ex)
            {

                Log.Error("Image Helper: " + ex.Message);
            }
        }
    }
}
