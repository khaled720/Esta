using ESTA.Models;

namespace ESTA.ViewModels
{
	public class AdminCourseInfoViewModel
	{


		public Course course { get; set; }

		public List<UserCourse> Users { get; set; }
	}
}
