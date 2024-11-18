using System.ComponentModel.DataAnnotations;

namespace ESTA.Areas.Admin.ViewModels
{
    public class CreateRole
    {
        [Required]
        public string RoleName { get; set; }
    }
}
