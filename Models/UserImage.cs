using System.ComponentModel.DataAnnotations;

namespace ESTA.Models
{
    public class UserImage
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public string Path { get; set; }

        [Required]
        public int TypeId { get; set; }
        public ImageType Type { get; set; }




        public string UserId { get; set; }
        public User User { get; set; }





    }
}
