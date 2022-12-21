using System.ComponentModel.DataAnnotations.Schema;

namespace ESTA.Models
{
    public class UserForum
    {
     


        public virtual Forum forum { get; set; }
      [ForeignKey("ForumId")]
        public int ForumId { get; set; }
     
        public virtual User user { get; set; }

         [ForeignKey("UserId")]
        public string UserId { get; set; }

        public string Comment { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
