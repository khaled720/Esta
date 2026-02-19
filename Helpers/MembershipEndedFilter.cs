using ESTA.Models;
using ESTA.Repository;
using ESTA.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ESTA.Helpers
{
    public class MembershipEndedFilter : Attribute, IAsyncAuthorizationFilter
    {
        private readonly IUnitOfWork uow;
        private readonly UserManager<User> userManager;

        public MembershipEndedFilter(IUnitOfWork uow, UserManager<User> userManager)
        {
            this.uow = uow;
            this.userManager = userManager;
        }
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;
            var CurrentUser = await userManager.GetUserAsync(user);
            if (!(await uow.UserRep.IsUserMempershipPaid(CurrentUser.Id)))
                context.Result = new RedirectToActionResult("profile", "user", new { returnUrl = context.HttpContext.Request.Path });

        }
    }
}
