using Microsoft.AspNetCore;

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
    }
}
