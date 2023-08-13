using ESTA.Models;
using ESTA.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ESTA.ViewComponents
{
    public class UserResultsViewComponent :ViewComponent
    {
        private readonly IUnitOfWork uow;
        private readonly IHttpContextAccessor contextAccessor;
        private readonly UserManager<User> userManager;

        public UserResultsViewComponent(IUnitOfWork uow, IHttpContextAccessor contextAccessor, UserManager<User> userManager)
        {
            this.uow = uow;
            this.contextAccessor = contextAccessor;
            this.userManager = userManager;
        }

        public  IViewComponentResult Invoke() 
        {
            var user =  userManager.GetUserAsync(contextAccessor.HttpContext!.User).Result;
          var UserResults=  uow.UsersCoursesRep.GetUserCoursesResults(user.Id).Result;
            return View("_results",UserResults);
        }
    }
}
