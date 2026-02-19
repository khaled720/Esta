namespace ESTA.ViewModels
{
    public class ViewProfile
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string FullNameAr { get; set; }
        public string UserId { get; set; }
        public bool Visible { get; set; }
        public DateTime Birthdate { get; set; }
        public string MobilePhone { get; set; }
        public string Country { get; set; }
        public string Job { get; set; }
        public string? MembershipNumber { get; set; }
        public string Email { get; set; }
        public string ProfilePic { get; set; }
        public bool IsMempershipPaid { get; set; }
        public int MempershipDaysToEnd { get; set; }
        public bool IsModerator { get; set; }
        public int CoursesCount { get; set; }
        public int FinishedCourses { get; set; }
        public int ForumsCount { get; set; }
    }
}
