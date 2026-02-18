using System.Configuration;
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
        private readonly IConfiguration _configuration;

        public RefundController(IUnitOfWork dbContext,
            IConfiguration configuration)
        {
            this.uow = dbContext;
            _configuration = configuration;
        }
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var result = await uow.RefundRep.GetAllRefundRequests();

            return View(result);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> CreateAsync(int CourseId)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var Course = await uow.CoursesRep.GetCourse(CourseId);
            var Order = uow.CourseOrdersRep.GetUserCourseOrderNumber(CourseId, userId);

            var Refund = new Refund
            {
                UserId = userId,
                OrderNumber = Order != null ? Order.OrderNumber : "0",
                RequestedAmount = Course.Price,
                Status = RefundStates.Pending.ToString(),
                Type = RefundTypes.Course.ToString(),
            };

            return View(Refund);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(Refund refund)
        {
            refund.SerialNumber = (await uow.RefundRep.GetMaxId() + 100).ToString();


            await uow.RefundRep.AddRefundRequest(refund);
            await uow.SaveChangesAsync();

            EmailSender.Send_Mail(
                      User.FindFirstValue(ClaimTypes.Email),
                         "You have submitted a refund request with the following details:<br>"
                            + "Serial Number: <b>" + refund.SerialNumber + "</b><br>"
                            + "Order Number: <b>" + refund.OrderNumber + "</b><br>  "
                            + "We are currently processing your request.",
                         "Esta Refund Request",
                         "ESTA"
                     );
            //var email=await   uow.UserRep.GetAdminUserEmail();
            var email = _configuration.GetValue<string>("Mail:AdminMail");
            EmailSender.Send_Mail(
                email,
                     "A new refund request has been submitted with the following details:<br><br>"
                        + "Serial Number: <b>" + refund.SerialNumber + "</b><br>"
                        + "Order Number: <b>" + refund.OrderNumber + "</b><br>  "
                        + "Submission Date: <b>" + refund.CreateDate + "</b><br>"
                        + "Submitted By: <b>" + User.FindFirstValue(ClaimTypes.Email) + "</b>",
                     "Esta Refund Request",
                     "ESTA"
                 );
            return RedirectToAction("profile", "User", new { area = "" });
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {

            var result = await uow.RefundRep.GetRefundRequest(id);
            return View(result);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Details(Refund refund, string newState)
        {
            //////
            ///
            if (refund.Status != RefundStates.Refunded.ToString())
            {
                if (
                    newState == RefundStates.Refunded.ToString()
                    &&
                    refund.Type == RefundTypes.Course.ToString())
                {
                    var courseOrder = await uow.CourseOrdersRep.GetOrderByNumber(refund.OrderNumber, refund.UserId);

                    //4 Means Course state is Refunded
                    await uow.CoursesRep.UpdateCourseState(courseOrder.CourseId, refund.UserId, 4);
                    //update user level

                    await uow.UserRep.UpdateUserLevel(refund.UserId);

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
                user.Email, "Hello,<br><br>We wanted to inform you that the status of your refund request has been updated.<br><br>" +
                    "Serial Number: <b> " + refund.SerialNumber + " </b><br>" +
                    "Order Number: <b> " + refund.OrderNumber + " </b><br>" +
                    "Updated Status: <b> " + newState + " </b><br><br>" +
                    "Thank you for using ESTA.",
                    "ESTA Refund Request",
                    "ESTA"
                  );

            }

            return RedirectToAction("index", "Payments", new { area = "Admin" });
        }
    }
}
