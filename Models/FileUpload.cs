using System.Runtime.CompilerServices;
using Microsoft.Extensions.Hosting;
using static System.Net.Mime.MediaTypeNames;

namespace ESTA.Models
{
    public class FileUpload
    {
      

        /// <summary>
       ///Upload file or image
        /// </summary>
        /// <param name="imgFile">File to be Uploaded</param>
        /// <param name="ImgName"> New Name for Image </param>
        /// <param name="FullSavePath">Full Path to save image Like "wwwroot../Images/Directors/" note after it image name will be added</param>
        /// <returns> Returns Name Of Uploaded Image As Follows  Image.jpg </returns>
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
