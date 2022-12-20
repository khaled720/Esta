using ESTA.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ESTA.ViewModels
{
    public class CourseLevelsViewModel
    {

        public Course  course { get; set; }


        public List<Level>? Levels { get; set; }


        public IFormFile? ImgFile { get; set; }
        
    }
}
