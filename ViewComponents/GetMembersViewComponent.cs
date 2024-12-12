using ESTA.Models;
using ESTA.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Policy;

namespace ESTA.ViewComponents
{
    public class GetMembersViewComponent : ViewComponent
    {
        private readonly IUnitOfWork uow;
        private readonly IHttpContextAccessor contextAccessor;
        private readonly UserManager<User> userManager;

        public GetMembersViewComponent(IUnitOfWork uow, IHttpContextAccessor contextAccessor, UserManager<User> userManager)
        {
            this.uow = uow;
            this.contextAccessor = contextAccessor;
            this.userManager = userManager;
        }

        public IViewComponentResult Invoke()
        {
            var user = userManager.GetUserAsync(contextAccessor.HttpContext!.User).Result;
            var AllMembers = userManager.Users.Where(x => x.Id != user.Id && x.VisibleProfile).ToList();

            return View("_otherMembers", AllMembers);
        }
    }
}
