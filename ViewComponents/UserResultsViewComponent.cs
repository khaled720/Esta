using Microsoft.AspNetCore.Mvc;

namespace ESTA.ViewComponents
{
    public class UserResultsViewComponent :ViewComponent
    {

        public IViewComponentResult Invoke() 
        {

            return View("_results");
        }
    }
}
