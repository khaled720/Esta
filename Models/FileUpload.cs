using System.Runtime.CompilerServices;
using Microsoft.Extensions.Hosting;
using static System.Net.Mime.MediaTypeNames;

namespace ESTA.Models
{
    public class FileUpload
    {
      

        public static async Task<string> SavePhotoAsync(IFormFile imgFile,string ImgName,string FullSavePath)
        {

            try
            {
    var SavePath = FullSavePath;
            var id = Guid.NewGuid().ToString();
            var ImageName = ImgName + "  " + id + ".jpg";
            await System.IO.File.WriteAllBytesAsync(SavePath + ImageName, new BinaryReader(imgFile.OpenReadStream()).ReadBytes((int)new BinaryReader(imgFile.OpenReadStream()).BaseStream.Length));
            return ImageName;
            }
            catch (Exception)
            {

                return "";
            }
        
        }
    }
}
