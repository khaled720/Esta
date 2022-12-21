using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace ESTA.Models
{
    public class User :IdentityUser
    {
      

        [Required]
        public string FullName { get; set; }

        [Required]
        public bool IsMempershipPaid { get; set; }


        //[ForeignKey("ForumLevelFrnKey")]
        //public int ForumLevelId { get; set; }

        
        public DateTime JoinDate { get; set; } = DateTime.Now;

    

        [ForeignKey("LevelId")]
        public virtual Level level { get; set; }

        
        public int LevelId { get; set; }

        public IEnumerable<UserCourse>? Courses { get; set; }
        public IEnumerable<UserForum>? Forums { get; set; }
    }
}
