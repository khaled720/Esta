
using System.ComponentModel.DataAnnotations;

namespace ESTA.Models
{
    public class ImageType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public List<UserImage> Images { get; set; }
    }
}
