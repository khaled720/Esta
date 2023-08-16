namespace ESTA.Models
{
    public class ModeratorForum
    {
        public int ForumId { get; set; }
        public Forum Forum { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
