namespace ESTA.ViewModels
{
    public class GetUserForums
    {
        public int Id { get; set; }
        public int forumId { get; set; }
        public string userName { get; set; }
        public string userId { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedDate { get; set; }
        public ICollection<GetUserForums> Replies { get; set; }
        public int RepliesCount { get; set; }
        public bool Banned { get; set; } = false;
    }
}
