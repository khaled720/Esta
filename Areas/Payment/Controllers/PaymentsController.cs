using System.Security.Claims;
using System.Text;
using ESTA.Areas.Payment.Models;
using ESTA.Areas.Payment.Repository.IRespository;
using ESTA.Helpers;
using ESTA.Models;
using ESTA.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace ESTA.Areas.Payment.Controllers
{

    [Area("Payment")]
    [Authorize]

    public class PaymentsController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly IUnitOfWork appContext;
        private readonly IHostEnvironment hostEnvironment;

        public PaymentsController(UserManager<User> userManager,IUnitOfWork appContext,IHostEnvironment hostEnvironment)
        {
            this.userManager = userManager;
            this.appContext = appContext;
            this.hostEnvironment = hostEnvironment;
        }
        public IActionResult Index()
        {
          //return  RedirectToAction("Index2", new { id = 45, gg = 789 });
          return View();
        }


        public IActionResult Index2(int id,int gg) 
        {


            return View();
        } 




        public async Task<IActionResult> SaveCoursePayment()
        {  
            CoursePayment coursePayment = new CoursePayment(); 
            var orderNumber = HttpContext.Session.GetString("OrderNumber");
                var orderId = HttpContext.Session.GetString("OrderDbId");
            try
            {
                ClassLibrary1.Interact PaymentManager = new ClassLibrary1.Interact();
            
                var courseOrder = await appContext.CourseOrdersRep.GetOrder(int.Parse(orderId.ToString()));
                var response = PaymentManager.getOrder(orderNumber);
             
                coursePayment.UserId = courseOrder.UserId;
                coursePayment.CourseId = courseOrder.CourseId;
                coursePayment.OrderId = courseOrder.Id;
                coursePayment.BuildPayment(response);

                var resultIndicator = HttpContext.Request.Query["resultIndicator"];
                var sucessIndicator = HttpContext.Session.GetString("SuccessIndicator");
                // insert Payment to database (Complet json & separated items)
                await appContext.CoursePaymentsRep.SaveGetOrder(coursePayment);

                if (resultIndicator == sucessIndicator && coursePayment.Status == "CAPTURED" &&
                    coursePayment.TotalCapturedAmount == coursePayment.TotalAuthorizedAmount
                    && coursePayment.TotalRefundedAmount == 0)    // payment is OK
                {

                    new LogManager(hostEnvironment).WriteInLogFile("Payment  ok Payment Id = " + coursePayment.OrderId);

                    //   Log("getOrderStatus", "resultIndicator:" + resultIndicator + "///sucessIndicator:" + sucessIndicator);


                    var course = await appContext.CoursesRep.GetCourse(coursePayment.CourseId);  

                               var usr = await userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
                    await appContext.UserRep.EnrollCourse(1, course.Id, usr.Id, true); //1 state means Enrolled



                    await userManager.UpdateAsync(usr);
                    if (course.LevelId < 4)
                    {
             
                        if (usr.LevelId < course.LevelId)
                        {

                            new LogManager(hostEnvironment).WriteInLogFile("---Updating User  " + usr.FullName + "  Level  to " + course.LevelId);
                            usr.LevelId = course.LevelId ?? usr.LevelId;

                    
                        }
                    }

                    //       Response.Redirect("receiptPage.aspx?a=" + amount + "&c=" + currency + "&s=" + status + "&t=" + lastUpdatedTime, false);
                 //   return View("receipt", coursePayment);

                    return RedirectToAction("receipt", new
                    {
                        status = coursePayment.Status,
                        orderNum = orderNumber,
                        orderId =orderId,
                        lastupdatedate = coursePayment.LastUpdateTime,
                        amount = course.Price,
                        isFailed = false,
                        course=course
                    });



                }

                else
                {
                    //StringBuilder str = new StringBuilder();
                    //str.Append("OrderID: " + OrderID + "||" + "resultIndicator: " + resultIndicator + "||" + "sucessIndicator: " + sucessIndicator + "||");
                    //str.Append("status: " + jObj["status"].ToString() + "||" + "totalAuthorizedAmount: " + Convert.ToDecimal(jObj["totalAuthorizedAmount"]).ToString() + "||" + "totalCapturedAmount: " + Convert.ToDecimal(jObj["totalCapturedAmount"]).ToString() + "||" + "totalRefundedAmount: " + Convert.ToDecimal(jObj["totalRefundedAmount"]).ToString());

                    ////log str
                    //Response.Write(str);


                    //if payment not okay

                    new LogManager(hostEnvironment).WriteInLogFile("Payment not ok  orderId="+coursePayment.OrderId);

                    return RedirectToAction("receipt", new
                    {
                        status = coursePayment.Status,
           
                        lastupdatedate = coursePayment.LastUpdateTime,
                     //   amount = course.,
                        isFailed = true,
                        orderNum = orderNumber,
                        orderId = orderId,
                        ErrorMsg = "Couldn't Complete Payment Proccess"
                 
                    });
                }



            }
            catch (Exception ex )
            {

                new LogManager(hostEnvironment).WriteInLogFile(
                    "Exception: Course Payment Failed  " + ex.Message);
                return RedirectToAction("receipt", new
                {
                    status = coursePayment.Status,
                    orderId = coursePayment.OrderId,
                    lastupdatedate = coursePayment.LastUpdateTime,
     //               amount = courseOrder.Amount,
                    isFailed = true,
                    ErrorMsg = "Couldn't Complete Payment Proccess."
                });
            }
       



        }




             public async Task<IActionResult> SaveMempershipPayment()
        {
                         MempershipPayment mempershipPayment = new();
                           var mempershipOrder=new MempershipOrder(); 
            
            
                var orderNumber = HttpContext.Session.GetString("OrderNumber");
                var orderId = HttpContext.Session.GetString("OrderDbId");
            try
            {
                ClassLibrary1.Interact PaymentManager = new ClassLibrary1.Interact();
             
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                new LogManager(hostEnvironment).WriteInLogFile("SavaMempershipPayment Was Called orderNumber= " + orderNumber);
                 mempershipOrder = await appContext.MempershipOrdersRep.GetOrder(int.Parse(orderId.ToString()));

                var response = PaymentManager.getOrder(orderNumber);

                new LogManager(hostEnvironment).WriteInLogFile("Get Order Was called ordernumber= " + orderNumber + " RESPONCE= " + response);

             
                mempershipPayment.UserId = mempershipOrder.UserId;

                mempershipPayment.OrderId = mempershipOrder.Id;
                mempershipPayment.BuildPayment(response);

                var resultIndicator = HttpContext.Request.Query["resultIndicator"];
                var sucessIndicator = HttpContext.Session.GetString("SuccessIndicator");

          /*      mempershipPayment.SuccessIndicator = sucessIndicator;
                mempershipPayment.OrderNumber = mempershipOrder.OrderNumber;
                mempershipPayment.PrepareJsonResponse = mempershipOrder.PrepareJsonResponse;
                mempershipPayment.PostOrderJsonResponse = mempershipOrder.PostOrderJsonResponse;
                mempershipPayment.SessionId = mempershipOrder.SessionId;
                mempershipPayment.OrderResult= mempershipOrder.OrderResult;
                mempershipPayment.OrderReference = mempershipOrder.OrderReference;
                mempershipPayment.TransactionReference = mempershipOrder.TransactionReference;
                mempershipPayment.OrderDescription = mempershipOrder.OrderDescription;*/

                // insert Payment to database (Complet json & separated items)
                await appContext.MempershipPaymentsRep.SaveGetOrder(mempershipPayment);
                await appContext.SaveChangesAsync();

                if (resultIndicator == sucessIndicator && mempershipPayment.Status == "CAPTURED" &&
                    mempershipPayment.TotalCapturedAmount == mempershipPayment.TotalAuthorizedAmount
                    && mempershipPayment.TotalRefundedAmount == 0)    // payment is OK
                {

                    new LogManager(hostEnvironment).WriteInLogFile("Payment  ok Payment Id = " + mempershipPayment.OrderId);

                    //   Log("getOrderStatus", "resultIndicator:" + resultIndicator + "///sucessIndicator:" + sucessIndicator);

                    try
                    {
                        await appContext.UserRep.PayMempership(User.FindFirstValue(ClaimTypes.NameIdentifier));

                        await appContext.SaveChangesAsync();

                        //       Response.Redirect("receiptPage.aspx?a=" + amount + "&c=" + currency + "&s=" + status + "&t=" + lastUpdatedTime, false);
                  //      return View("receipt", mempershipPayment);



                     return   RedirectToAction("receipt",new {
                         status=mempershipPayment.Status,
                         orderNum = orderNumber,
                         orderId =orderId,
                         lastupdatedate = mempershipPayment.LastUpdateTime,
                         amount = mempershipOrder.Amount,
                         isFailed= false
                     });

                    }
                    catch (Exception ex)
                    {
                //        return View("_info", new Info("Mempership Payment Ok But Not Saved ",
                //"Couldn't Complete Payment Proccess"
                //)
                //    );
                        return RedirectToAction("receipt", new
                        {
                            status = mempershipPayment.Status,
                            orderNum = orderNumber,
                            orderId = orderId,
                            lastupdatedate = mempershipPayment.LastUpdateTime,
                            amount = mempershipOrder.Amount,
                            isFailed = true,
                            ErrorMsg= "Couldn't Complete Payment Proccess.Payment Done But Not Saved"
                        });



                    }



                }

                else
                {
                    //StringBuilder str = new StringBuilder();
                    //str.Append("OrderID: " + OrderID + "||" + "resultIndicator: " + resultIndicator + "||" + "sucessIndicator: " + sucessIndicator + "||");
                    //str.Append("status: " + jObj["status"].ToString() + "||" + "totalAuthorizedAmount: " + Convert.ToDecimal(jObj["totalAuthorizedAmount"]).ToString() + "||" + "totalCapturedAmount: " + Convert.ToDecimal(jObj["totalCapturedAmount"]).ToString() + "||" + "totalRefundedAmount: " + Convert.ToDecimal(jObj["totalRefundedAmount"]).ToString());

                    ////log str
                    //Response.Write(str);


                    //if payment not okay

                    new LogManager(hostEnvironment).WriteInLogFile("Payment NOT OK  Payment Id= " +
                        mempershipPayment.OrderId);
                    return RedirectToAction("receipt", new
                    {
                        status = mempershipPayment.Status,
                        orderNum = orderNumber,
                        orderId = orderId,
                        lastupdatedate = mempershipPayment.LastUpdateTime,
                        amount = mempershipOrder.Amount,
                        isFailed = true,
                        ErrorMsg = "Couldn't Complete Payment Proccess.Payment Failed"
                    });
                }



            }
            catch (Exception ex)
            {

                new LogManager(hostEnvironment).WriteInLogFile("Exception: Mempership Payment Failed  "+ex.Message);

       

                return RedirectToAction("receipt", new
                {
                    status = mempershipPayment.Status,
                    orderNum = orderNumber,
                    orderId = orderId,
                    lastupdatedate = mempershipPayment.LastUpdateTime,
                    amount = mempershipOrder.Amount,
                    isFailed = true,
                    ErrorMsg = "Couldn't Complete Payment Proccess.Mempership Payment Failed "
                });
            }

          



     //       return RedirectToAction("Profile", "User", new { area = "" });

        }




        public IActionResult receipt(
                string?    status,
                 string?  orderNum,
                 string? lastupdatedate,
               string? amount ,
               Boolean isFailed,
               string? orderId,
               string? ErrorMsg
            ,Course? course

            ) 
        {
            var obj = new PaymentReceipt() { Status=status,OrderNumber=orderNum,
                OrderId=orderId
                ,LastUpdateDate=lastupdatedate,Amount=amount,IsFailed= isFailed,
                ErrorMsg=ErrorMsg,course=course
            };
            ///send email with order number 
            ///making a refund form that admin can view
            if (course!=null) {
                EmailSender.Send_Mail(User.FindFirstValue(ClaimTypes.Email)
                    , "<h3>Course Name :<p>" + course.Title
                    + "<h3>Course Level :<p>" + course.level.TypeName
                    + "<h3>Order Number :<p>" + orderNum +
                    "</p></h3>" + "<h3>Amount :<p>" + amount + "</p></h3>" +
                    "<h3> Status:<p>" + status + "</p></h3>"
                    , "Esta Payment Receipt", "Esta");
            }
            else
            {
                EmailSender.Send_Mail(User.FindFirstValue(ClaimTypes.Email)
              ,  "<h3>Order Number :<p>" + orderNum +
              "</p></h3>" + "<h3>Amount :<p>" + amount + "</p></h3>" +
              "<h3> Status:<p>" + status + "</p></h3>"
              , "Esta Payment Receipt", "Esta");
            }
            return View(obj);
        }

        
    }


}
