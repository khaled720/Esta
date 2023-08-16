using System.ComponentModel.DataAnnotations;

namespace ESTA.Models
{
    public class ForumBannedUser
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public int ForumId { get; set; }
        public Forum Forum { get; set; }
        public DateTime Date { get; set; }
        public string ModId { get; set; }
        public User Mod { get; set; }
        [StringLength(500)]
        public string Reason { get; set; }
        public byte Active { get; set; } = 1;       // 1->Active, 0-> not Active.
    }
}
