using System.ComponentModel.DataAnnotations;
using System.Security.AccessControl;
using System.Xml.Linq;

namespace ESTA.Models
{
    public class Content
    {
        public int Id { get; set; }

        public string Type { get; set; }

        [Display(ResourceType = typeof(ESTA.Resources.DataAnnotationsResource), Name = "descar")]
        public string DescriptionAr { get; set; }

        [Display(ResourceType = typeof(ESTA.Resources.DataAnnotationsResource), Name = "descen")]
        public string DescriptionEn { get; set; }
    }
}
