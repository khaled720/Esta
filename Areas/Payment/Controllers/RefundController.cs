using System.Security.Claims;
using ESTA.Areas.Payment.Models;
using ESTA.Models;
using ESTA.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESTA.Areas.Payment.Controllers
{
    [Area("Payment")]
    public class RefundController : Controller
    {
        private readonly IUnitOfWork uow;

        public RefundController(IUnitOfWork dbContext)
        {
            this.uow = dbContext;
        }
        [Authorize]
        public async Task<IActionResult> Index()
        {
        var result=    await uow.RefundRep.GetAllRefundRequests();

            return View(result);
        }

        [Authorize]
        [HttpGet]
        public IActionResult Create()
        {
        
            return View();
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(Refund refund)
        {

            refund.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            refund.SerialNumber = (await uow.RefundRep.GetMaxId()+100).ToString();
            refund.Status = RefundStates.Pending.ToString();
            refund.Type=RefundTypes.Course.ToString();


          await uow.RefundRep.AddRefundRequest(refund);
     await       uow.SaveChangesAsync();

           EmailSender.Send_Mail(
                     User.FindFirstValue(ClaimTypes.Email),
                        "you have sent a refund request with<br> Serial Number :<b>"
                        +refund.SerialNumber+"</b> <br> For Order <b>"+refund.OrderNumber
                        +"</b><br> we are working on it",
                        "Esta Refund Request",
                        "Esta"
                    );
         var email=await   uow.UserRep.GetAdminUserEmail();
            EmailSender.Send_Mail(
                email,
                     "refund request has been placed with <br> Serial Number <b>"
                     + refund.SerialNumber + "</b><br> For Order <br> <b>" + refund.OrderNumber
                     + "</b> <br>on "+refund.CreateDate+"<br> by User <b>"+User.FindFirstValue(ClaimTypes.Email)+"</b>",
                     "Esta Refund Request",
                     "Esta"
                 );
            return RedirectToAction("profile","User",new { area=""});
        }






        [Authorize]
        [HttpGet]
        public async Task<IActionResult>  Details(int id)
        {
       
            var result=await     uow.RefundRep.GetRefundRequest(id);
            return View(result);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Details(Refund refund, string newState)
        {
            //////
            ///
            if (refund.Status != RefundStates.Refunded.ToString() ) {
                if (
                    newState == RefundStates.Refunded.ToString()
                    &&
                    refund.Type == RefundTypes.Course.ToString())
                {
                    var courseOrder = await uow.CourseOrdersRep.GetOrderByNumber(refund.OrderNumber, refund.UserId);

                    //4 Means Course state is Refunded
                    await uow.CoursesRep.UpdateCourseState(courseOrder.CourseId, refund.UserId, 4);
                    //update user level

                    uow.UserRep.UpdateUserLevel(refund.UserId);

                    await uow.SaveChangesAsync();
                }

                if (
            newState == RefundStates.Refunded.ToString()
            &&
            refund.Type == RefundTypes.Mempership.ToString())
                {
                    var courseOrder = await uow.UserRep.RevokeMempershipPayment(refund.UserId);
                    //////////////////
                    await uow.SaveChangesAsync();
                }


                //here we edit usercourse 

                await uow.RefundRep.UpdateRefundStatus(refund.Id, newState);
                await uow.SaveChangesAsync();

                var user = await uow.UserRep.GetUser(refund.UserId);

                EmailSender.Send_Mail(
                user.Email,
                      "Your Refund Request State Has been updated  <br>Serial Number <b>"
                      + refund.SerialNumber + "</b><br>For Order <b>" + refund.OrderNumber
                      + "</b><br> State  <b>" + newState + "</b>",
                      "Esta Refund Request",
                      "Esta"
                  );

            }

            return RedirectToAction("index", "Payments", new { area = "Admin" });
        }





    }
}
