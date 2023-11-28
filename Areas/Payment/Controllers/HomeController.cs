using Microsoft.AspNetCore.Mvc;

namespace ESTA.Areas.Payment.Controllers
{
    [Area("Payment")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {

       
            //return RedirectToAction("receipt", "Payments");
           return View();
        }
    }
}
