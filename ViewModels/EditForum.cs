using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ESTA.ViewModels
{
    public class EditForum
    {
        public int Id { get; set; }
        [Required(
                  ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
                  ErrorMessageResourceName = "required"
              )]
        [Display(ResourceType = typeof(ESTA.Resources.DataAnnotationsResource), Name = "forumtitle")]

        [StringLength(250)]
        public string Title { get; set; }
        [Required(
                  ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
                  ErrorMessageResourceName = "required"
              )]
        [Display(ResourceType = typeof(ESTA.Resources.DataAnnotationsResource), Name = "forumlevel")]

        public int levelId { get; set; }
        [StringLength(250)]
        [Required(
            ErrorMessageResourceType = typeof(ESTA.Resources.DataAnnotationsResource),
            ErrorMessageResourceName = "required"
        )]
        [Display(ResourceType = typeof(ESTA.Resources.DataAnnotationsResource), Name = "forumdesc")]

        public string Description { get; set; }
    }
}
