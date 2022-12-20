using System.ComponentModel.DataAnnotations;

namespace ESTA.Models
{
    public class Level
    {


        [Key]
        public int Id { get; set; }
        public string TypeName { get; set; }
        public virtual ICollection<User> user { get; set; }
        public virtual ICollection<Forum> forum { get; set; }

    }
}
