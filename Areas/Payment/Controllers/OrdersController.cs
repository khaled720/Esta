using ESTA.Areas.Payment.Models;
using ESTA.Areas.Payment.Repository.IRespository;
using ESTA.Helpers;
using ESTA.Models;
using ESTA.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Security.Claims;
using System.Text;
using System.Web;
namespace ESTA.Areas.Payment.Controllers
{
    [Area("Payment")]
    public class OrdersController : Controller
    {
        private readonly IUnitOfWork appRep;
        private readonly IHostEnvironment hostEnvironment;
        private readonly IConfiguration configuration;
        private readonly UserManager<User> userManager;

        public OrdersController(IUnitOfWork appRep,IHostEnvironment hostEnvironment,IConfiguration configuration,UserManager<User> userManager)
        {
            this.appRep = appRep;
            this.hostEnvironment = hostEnvironment;
            this.configuration = configuration;
            this.userManager = userManager;
        }



        public IActionResult MakeOrder()
        {
            return View();
        }




        public IActionResult ConfirmCourseOrder()//string orderNum,string orderDesc,double amount  )
        {
            try
            {
       var courseOrder  =  JsonConvert.DeserializeObject<CourseOrder>(  TempData["myData"].ToString()  );
  
            //   return View(new CourseOrder() { Amount=amount,OrderNumber=orderNum,OrderDescription=orderDesc});
            return View(courseOrder);
            }
            catch (Exception)
            {

                return RedirectToAction("Index","Home",new {area=""} );
            }
     

        }
        [HttpPost]
        public async Task<IActionResult>  AcceptCourseOrder(CourseOrder order)
        {
            try
            {
                ClassLibrary1.Interact PaymentManager = new ClassLibrary1.Interact();

                var returnUrl = configuration.GetValue<string>("ClassLibrary1_Config:returnUrl");

                var response = PaymentManager.perpareOrder(order.OrderNumber,
                    (decimal)order.Amount, order.Currency, order.OrderDescription, returnUrl);
                order.SetDataAfterPrepareOrder(response);
                new LogManager(hostEnvironment).WriteInLogFile(JsonConvert
                    .SerializeObject("PrepareOrderResponse : "+response));
                //save to db
                await appRep.CourseOrdersRep.SavePrepareOrder(order);
                await appRep.SaveChangesAsync();


                string response2 = PaymentManager.postOrder(response);
                try
                {
                    new LogManager(hostEnvironment).WriteInLogFile(JsonConvert
                        .SerializeObject("PostOrderReponse: " + response2));
            
                }
                catch (Exception ex)
                {

                }
                order.SetDataAfterPostOrder(response2);

                //sesion id succesindi
                appRep.CourseOrdersRep.UpdatePrepareOrder(order);
                await appRep.SaveChangesAsync();

                HttpContext.Session.SetString("SuccessIndicator", order.SuccessIndicator);
                HttpContext.Session.SetString("OrderNumber", order.OrderNumber);
                HttpContext.Session.SetString("OrderDbId", order.Id.ToString());
                HttpContext.Session.SetString("SessionId", order.SessionId);

               // if (User != null && User.Identity.IsAuthenticated)
               // {
               //     try
               //     {


               //   //      await appRep.UserRep.EnrollCourse(1, order.CourseId, User.FindFirstValue(ClaimTypes.NameIdentifier), false);
               //         //if (order.Course.LevelId < 4)
               //         //{
               //         //    var usr = await userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
               //         //    if (usr.LevelId < order.Course.LevelId)
               //         //    {
               //         //        usr.LevelId = order.Course.LevelId??0;
               //         //        await userManager.UpdateAsync(usr);
               //         //    }
               //         //}

               ////         await appRep.SaveChangesAsync();



               //     }
               //     catch (Exception ex)
               //     {


               //     }

               //     //   return RedirectToAction("Courses");
               // }


            }
            catch (Exception e) 
            {
            
            }

            return RedirectToAction("Pay","Orders",new { area="Payment"});
        }




        [HttpGet]
        public async Task<IActionResult> AcceptMembershipOrder()
        {
            try
            {
                ClassLibrary1.Interact PaymentManager = new ClassLibrary1.Interact();

                var returnUrl = configuration.GetValue<string>("ClassLibrary1_Config:MembershipreturnUrl");

             var mempershipFee=await   appRep.ConstantsRep.getMempershipFee();
                
                MempershipOrder mempershipOrder = new();
                Random random = new Random();
                int length =await appRep.MempershipOrdersRep.GetMaxId();
                var OrderNumber = "Mem-" + (length + 100); 

                mempershipOrder.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                mempershipOrder.OrderNumber = OrderNumber;
                mempershipOrder.Amount = mempershipFee;
                mempershipOrder.OrderDescription = "Mempership Payment Order ";
                var response = PaymentManager.perpareOrder(mempershipOrder.OrderNumber,
                  Convert.ToDecimal(mempershipFee), "EGP",mempershipOrder.OrderDescription,
                  returnUrl);
                //   mempershipOrder.BuildOrder(response);
           

               
                mempershipOrder.SetDataAfterPrepareOrder(response);
                new LogManager(hostEnvironment).WriteInLogFile(JsonConvert
                .SerializeObject("AcceptMembershipOrder PrepareOrderResponse : " + response));
                //save to db
                await appRep.MempershipOrdersRep.SavePrepareOrder(mempershipOrder);
                await appRep.SaveChangesAsync();
                new LogManager(hostEnvironment).WriteInLogFile("MempershipOrder Object = "+JsonConvert
                .SerializeObject(mempershipOrder));

                string response2 = PaymentManager.postOrder(response);

                if (!String.IsNullOrEmpty(response2)) 
                {
                   try
                {

                    new LogManager(hostEnvironment)
                        .WriteInLogFile("AcceptMembershipOrder PostOrderReponse : -->  ");


                    new LogManager(hostEnvironment).WriteInLogFile(JsonConvert
                        .SerializeObject(response2));
                }
                catch (Exception ex)
                {

                }
                mempershipOrder.SetDataAfterPostOrder(response2);
                //sesion id succesindi
                appRep.MempershipOrdersRep.UpdatePrepareOrder(mempershipOrder);
                await appRep.SaveChangesAsync();

                HttpContext.Session.SetString("SuccessIndicator", mempershipOrder.SuccessIndicator); // add the order data in session to verify the order validity in the callback. return url 
                HttpContext.Session.SetString("OrderNumber", mempershipOrder.OrderNumber);
                HttpContext.Session.SetString("OrderDbId", mempershipOrder.Id.ToString());
                HttpContext.Session.SetString("SessionId", mempershipOrder.SessionId);
                }

                else
                {
                    new LogManager(hostEnvironment)
                    .WriteInLogFile("AcceptMembershipOrder PrepareOrderResponse : Response Is Null ####  ");

                }
                //if (User != null && User.Identity.IsAuthenticated)
                //{
                //    try
                //    {



                //        //await appRep.UserRep.EnrollCourse(1, order.CourseId, User.FindFirstValue(ClaimTypes.NameIdentifier), false);

                //        //if (order.Course.LevelId < 4)
                //        //{
                //        //    var usr = await userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
                //        //    if (usr.LevelId < order.Course.LevelId)
                //        //    {
                //        //        usr.LevelId = order.Course.LevelId??0;
                //        //        await userManager.UpdateAsync(usr);
                //        //    }
                //        //}






                //    }
                //    catch (Exception ex)
                //    {


                //    }

                //    //   return RedirectToAction("Courses");
                //}


            }
            catch (Exception e)
            {

            }

            return RedirectToAction("Pay", "Orders", new { area = "Payment" });
        }





        public async Task<IActionResult> Pay() 
        {

           await  Task.Delay(200);
            try
            {
  new LogManager(hostEnvironment)
                    .WriteInLogFile("OrdersController  Pay  SessionID =" + HttpContext.Session.GetString("SessionId").ToString());

            object sessionId = HttpContext.Session.GetString("SessionId");

                return View(sessionId);
            }
            catch (Exception ex )
            {
                new LogManager(hostEnvironment)
                  .WriteInLogFile("OrdersController  Pay  Exception" +ex.Message.ToString() );

                return View("Pay","Can not complete payment now. Try Again Later! ");
            }


        }





    }
}
