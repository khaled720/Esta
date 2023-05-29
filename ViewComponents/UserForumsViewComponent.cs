using System.Security.Claims;
using ESTA.Models;
using ESTA.Repository;
using ESTA.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ESTA.ViewComponents
{
    public class UserForumsViewComponent:ViewComponent
    {
        private readonly IUnitOfWork uow;
        private readonly IHttpContextAccessor contextAccessor;
        private readonly UserManager<User> userManager;

        public UserForumsViewComponent(IUnitOfWork uo, IHttpContextAccessor  contextAccessor, UserManager<User> userManager)
        {
            this.uow = uo;
            this.contextAccessor = contextAccessor;
            this.userManager = userManager;
        }
        public IViewComponentResult Invoke()
        {
           
            var user=   userManager.GetUserAsync(contextAccessor.HttpContext!.User).Result;
            // return forums with same level or lower
            var xyz = uow.ForumRep.GetSpecificUserForums(user.LevelId);
            return View("_forums",xyz);
        }
    }
}
