using ESTA.Resources;
using System.ComponentModel.DataAnnotations;

namespace ESTA.ViewModels
{
    public class EditUser
    {
        public string Id { get; set; }
        [Required(
                 ErrorMessageResourceType = typeof(DataAnnotationsResource),
        ErrorMessageResourceName = "required"
             )]
        public IEnumerable<string> RoleId { get; set; }
    }
}
