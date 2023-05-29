


using System.Security.Claims;
using ESTA.Repository.IRepository;

using Microsoft.AspNetCore.Mvc;

namespace ESTA.ViewComponents
{
    [ViewComponent]
    public class UserCoursesViewComponent : ViewComponent
    {
        private readonly IUnitOfWork uow;
        private readonly IHttpContextAccessor contextAccessor;

        public UserCoursesViewComponent(IUnitOfWork Uow, IHttpContextAccessor contextAccessor)
        {
            this.uow = Uow;
            this.contextAccessor = contextAccessor;
        }

        public IViewComponentResult Invoke(string userId)
        {
            if (userId==null || userId =="")
            {
               userId= contextAccessor.HttpContext!.User
                .FindFirstValue(ClaimTypes.NameIdentifier);
            }

           
            var courses =  
                uow.UserRep
                .GetMyCourses(userId)
                .GetAwaiter().GetResult();

         

            return View("_courses", courses);
        }
    }

}