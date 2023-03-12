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
        private readonly UserManager<User> userManager;

        public UserForumsViewComponent(IUnitOfWork uo, UserManager<User> userManager)
        {
            this.uow = uo;
            this.userManager = userManager;
        }




        public IViewComponentResult Invoke(int userlevelId)
        {

            
   var xyz = uow.ForumRep.GetSpecificUserForums(userlevelId);
            return View("_forums",xyz);
        }


    }
}
