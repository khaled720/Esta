using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ESTA.ViewModels
{
    public class EditModerator
    {
        public string Id { get; set; }

        [Display(Name = "Full Name")]
        [Required(ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource), ErrorMessageResourceName = "required")]
        public string FullName { get; set; }

        public List<int> SelectForum { get; set; }
    }
}
