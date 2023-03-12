


using ESTA.Repository.IRepository;

using Microsoft.AspNetCore.Mvc;

namespace ESTA.ViewComponents
{
    [ViewComponent]
    public class UserCoursesViewComponent : ViewComponent
    {
        private readonly IUnitOfWork uow;


        public UserCoursesViewComponent(IUnitOfWork Uow)
        {
            this.uow = Uow;
  
        }

        public IViewComponentResult Invoke(string id)
        {

            var courses =  uow.UserRep.GetMyCourses(id).GetAwaiter().GetResult();

            return View("_courses", courses);
        }
    }

}