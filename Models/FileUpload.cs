using System.Runtime.CompilerServices;
using static System.Net.Mime.MediaTypeNames;

namespace ESTA.Models
{
    public class FileUpload
    {
      

        static string SavePhoto(IFormFile imgFile)
        {

    
 

            File.WriteAllBytesAsync("", new BinaryReader(imgFile.OpenReadStream()).ReadBytes((int)new BinaryReader(imgFile.OpenReadStream()).BaseStream.Length));
            return "~/Images/Courses/";
        }
    }
}
