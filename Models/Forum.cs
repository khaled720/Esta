using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ESTA.Models
{
    public class Forum
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }

        public virtual Level level { get; set; }
        //[ForeignKey("ForumLevelFrnKey")]
        //public int ForumLevelId { get; set; }

        public string Description { get; set; }

       // public string DescriptionAr { get; set; }
    
    }
}
