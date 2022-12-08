using System.ComponentModel.DataAnnotations.Schema;

namespace ESTA.Models
{
    public class UserForum
    {
        public int Id { get; set; }


        public virtual Forum forum { get; set; }
      //[ForeignKey("ForumIDFrnKey")]
      //  public int ForumId { get; set; }
     
        public virtual User user { get; set; }

        // [ForeignKey("UserIDFrnKey")]
        //public int UserId { get; set; }

        public string Comment { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
