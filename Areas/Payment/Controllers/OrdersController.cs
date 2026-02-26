using ESTA.Areas.Payment.Data;
using ESTA.Areas.Payment.Models;
using ESTA.Areas.Payment.Repository.IRespository;
using ESTA.Helpers;
using ESTA.Models;
using ESTA.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OfficeOpenXml.Style;
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
        private readonly LogManager<OrdersController> logger;
        private readonly IConfiguration configuration;
        private readonly UserManager<User> userManager;

        public OrdersController(IUnitOfWork appRep, LogManager<OrdersController> _logger, IConfiguration configuration, UserManager<User> userManager)
        {
            this.appRep = appRep;
            this.configuration = configuration;
            this.userManager = userManager;
            logger = _logger;
        }

        public IActionResult MakeOrder()
        {
            return View();
        }

        public IActionResult ConfirmCourseOrder()//string orderNum,string orderDesc,double amount  )
        {
            try
            {
                var courseOrder = JsonConvert.DeserializeObject<CourseOrder>(TempData["myData"].ToString());

                //   return View(new CourseOrder() { Amount=amount,OrderNumber=orderNum,OrderDescription=orderDesc});
                return View(courseOrder);
            }
            catch (Exception)
            {

                return RedirectToAction("Index", "Home", new { area = "" });
            }


        }
        [HttpPost]
        public async Task<IActionResult> AcceptCourseOrder(CourseOrder order)
        {
            try
            {
                if (order.PaymentMethod.ToUpper() == "Bank".ToUpper())
                {
                    ClassLibrary1.Interact PaymentManager = new ClassLibrary1.Interact();

                    var returnUrl = configuration.GetValue<string>("ClassLibrary1_Config:returnUrl");

                    var response = PaymentManager.perpareOrder(order.OrderNumber,
                        (decimal)order.Amount, order.Currency, order.OrderDescription, returnUrl);
                    order.SetDataAfterPrepareOrder(response);
                    logger.WriteInfo(JsonConvert
                        .SerializeObject("PrepareOrderResponse : " + response));
                    //save to db
                    await appRep.CourseOrdersRep.SavePrepareOrder(order);
                    await appRep.SaveChangesAsync();

                    string response2 = PaymentManager.postOrder(response);

                    logger.WriteInfo(JsonConvert
                        .SerializeObject("PostOrderReponse: " + response2));

                    order.SetDataAfterPostOrder(response2);

                    //sesion id succesindi
                    appRep.CourseOrdersRep.UpdatePrepareOrder(order);
                    await appRep.SaveChangesAsync();

                    HttpContext.Session.SetString("SuccessIndicator", order.SuccessIndicator);
                    HttpContext.Session.SetString("OrderNumber", order.OrderNumber);
                    HttpContext.Session.SetString("OrderDbId", order.Id.ToString());
                    HttpContext.Session.SetString("SessionId", order.SessionId);
                }
                else
                {
                    //Pay with FAWRY..
                    //Insert to db.
                    order.TransactionReference = "Fawry_Transaction";
                    await appRep.CourseOrdersRep.SavePrepareOrder(order);
                    await appRep.SaveChangesAsync();

                    var currentUser = await userManager.GetUserAsync(User);
                    string SecureKey = configuration.GetValue<string>("Fawry:SecureKey");
                    //create order.
                    var FawryObj = new FawryObject()
                    {
                        MerchantCode = configuration.GetValue<string>("Fawry:MerchantCode"),
                        MerchantRefNum = order.OrderNumber,
                        CustomerEmail = currentUser.Email,
                        CustomerMobile = currentUser.MobilePhone,
                        CustomerName = currentUser.FullName,
                        CustomerProfileId = currentUser.Id,
                        //PaymentMethod = "PayAtFawry",
                        PaymentExpiry = DateTime.Now.AddDays(1).ToUniversalTime().Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds,
                        //AuthCaptureModePayment = true,
                        ReturnUrl = configuration.GetValue<string>("Fawry:CourseReturnUrl") + order.Id.ToString(),
                    };
                    FawryObj.ChargeItems.Add(new ChargeItem { Price = string.Format("{0:0.00}", order.Amount), ItemId = order.CourseId, Quantity = 1 });
                    FawryObj.CreateFawrySignature(SecureKey);

                    return View("PayFawry", FawryObj);
                }
            }
            catch (Exception e)
            {
                logger.WriteError("Exception in  AcceptCourseOrder: " + e.Message);
            }

            return RedirectToAction("Pay", "Orders", new { area = "Payment" });
        }

        public async Task<IActionResult> ConfirmMembershipOrderAsync()
        {
            var mempershipFee = await GetTotalMembershipAsync();

            return View(mempershipFee);
        }

        [HttpPost]
        public async Task<IActionResult> AcceptMembershipOrder(MembershipFeeDetails mempershipFee)
        {
            try
            {
                //var mempershipFee = await appRep.ConstantsRep.getMempershipFee();
                MempershipOrder mempershipOrder = new();
                Random random = new Random();
                int length = await appRep.MempershipOrdersRep.GetMaxId();
                var OrderNumber = "Mem-" + (length + 100);

                mempershipOrder.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                mempershipOrder.OrderNumber = OrderNumber;
                mempershipOrder.Amount = mempershipFee.Total;
                mempershipOrder.OrderDescription = "Mempership Payment Order ";

                if (mempershipFee.PaymentMethod.ToUpper() == "Bank".ToUpper())
                {
                    ClassLibrary1.Interact PaymentManager = new ClassLibrary1.Interact();
                    var returnUrl = configuration.GetValue<string>("ClassLibrary1_Config:MembershipreturnUrl");

                    var response = PaymentManager.perpareOrder(mempershipOrder.OrderNumber,
                      Convert.ToDecimal(mempershipFee), "EGP", mempershipOrder.OrderDescription,
                      returnUrl);
                    //   mempershipOrder.BuildOrder(response);

                    mempershipOrder.SetDataAfterPrepareOrder(response);
                    logger.WriteInfo(JsonConvert
                    .SerializeObject("AcceptMembershipOrder PrepareOrderResponse : " + response));
                    //save to db
                    await appRep.MempershipOrdersRep.SavePrepareOrder(mempershipOrder);
                    await appRep.SaveChangesAsync();
                    logger.WriteInfo("MempershipOrder Object = " + JsonConvert
                    .SerializeObject(mempershipOrder));

                    string response2 = PaymentManager.postOrder(response);

                    if (!String.IsNullOrEmpty(response2))
                    {
                        logger.WriteInfo("AcceptMembershipOrder PostOrderReponse : -->  ");

                        logger.WriteInfo(JsonConvert
                            .SerializeObject(response2));
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
                        logger.WriteInfo("AcceptMembershipOrder PrepareOrderResponse : Response Is Null ####  ");

                    }

                    return RedirectToAction("Pay", "Orders", new { area = "Payment" });
                }
                else
                {
                    //Pay with FAWRY..
                    //Insert to db.
                    mempershipOrder.TransactionReference = "Fawry_Transaction";
                    await appRep.MempershipOrdersRep.SavePrepareOrder(mempershipOrder);
                    await appRep.SaveChangesAsync();

                    var currentUser = await userManager.GetUserAsync(User);
                    string SecureKey = configuration.GetValue<string>("Fawry:SecureKey");
                    //create order.
                    var FawryObj = new FawryObject()
                    {
                        MerchantCode = configuration.GetValue<string>("Fawry:MerchantCode"),
                        MerchantRefNum = mempershipOrder.OrderNumber,
                        CustomerEmail = currentUser.Email,
                        CustomerMobile = currentUser.MobilePhone,
                        CustomerName = currentUser.FullName,
                        CustomerProfileId = currentUser.Id,
                        //PaymentMethod = "PayAtFawry",
                        PaymentExpiry = DateTime.Now.AddDays(1).ToUniversalTime().Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds,
                        //AuthCaptureModePayment = true,
                        ReturnUrl = configuration.GetValue<string>("Fawry:MembershipReturnUrl") + mempershipOrder.Id.ToString(),
                    };
                    FawryObj.ChargeItems.Add(new ChargeItem { Price = string.Format("{0:0.00}", mempershipFee.Total), ItemId = 0, Quantity = 1 });
                    FawryObj.CreateFawrySignature(SecureKey);

                    return View("PayFawry", FawryObj);
                }
            }
            catch (Exception e)
            {
                logger.WriteError("Exception in  AcceptMembershipOrder: " + e.Message);
            }

            return RedirectToAction("Pay", "Orders", new { area = "Payment" });
        }
        private async Task<MembershipFeeDetails> GetTotalMembershipAsync()
        {
            var FeesDetails = new MembershipFeeDetails();
            var ConstantsFees = await appRep.ConstantsRep.getConstants();
            var LoggedInuser = await userManager.GetUserAsync(User);
            FeesDetails.MembershipFee = ConstantsFees.MempershipFee;
            var TotalFee = ConstantsFees.MempershipFee;

            if (LoggedInuser == null)
            {
                return FeesDetails;
            }
            else
            {
                //currentMonth < Expiry.
                //currentMonth > Expiry and < penalty.
                //currentMonth > penalty.
                //currentMonth >= ConstantsFees.PenaltyMonth && currentMonth < ConstantsFees.MempershipExpiryMonth
                //currentMonth < ConstantsFees.PenaltyMonth && currentMonth >= ConstantsFees.MempershipExpiryMonth

                if (!string.IsNullOrEmpty(LoggedInuser.MembershipNumber))
                {
                    FeesDetails.RenewalFee = ConstantsFees.RenewalFee;
                    TotalFee += ConstantsFees.RenewalFee;

                    var CurrentDate = DateTime.Now.Date;
                    var CurrentYear = CurrentDate.Month > ConstantsFees.MempershipExpiryMonth ?
                        CurrentDate.Year : CurrentDate.Year - 1;
                    var PenaltyDate = new DateTime(CurrentYear, ConstantsFees.PenaltyMonth, 1);

                    var LatenessYear = Math.Max(0, CurrentYear - LoggedInuser.MembershipYear);
                    int penaltyYears = 0;

                    if (LatenessYear >= 1 && CurrentDate >= PenaltyDate)
                    {
                        penaltyYears = LatenessYear;
                    }
                    else if (LatenessYear > 1 && CurrentDate < PenaltyDate)
                    {
                        penaltyYears = LatenessYear - 1;
                    }

                    var penalty = ConstantsFees.LatePenalty * penaltyYears;

                    FeesDetails.LatePenalty = penalty;
                    TotalFee += penalty;

                }
                else
                {
                    FeesDetails.NewMempershipFee = ConstantsFees.NewMempershipFee;
                    TotalFee += ConstantsFees.NewMempershipFee;
                }
            }
            FeesDetails.Total = TotalFee;

            return FeesDetails;
        }

        public async Task<IActionResult> Pay()
        {

            await Task.Delay(200);
            try
            {
                logger.WriteInfo("OrdersController  Pay  SessionID =" + HttpContext.Session.GetString("SessionId").ToString());

                object sessionId = HttpContext.Session.GetString("SessionId");

                return View(sessionId);
            }
            catch (Exception ex)
            {
                logger.WriteError("OrdersController  Pay  Exception" + ex.Message.ToString());

                return View("Pay", "Can not complete payment now. Try Again Later! ");
            }

        }

        //type=ChargeResponse&referenceNumber=775623104&merchantRefNumber=0bc6d4&orderAmount=20.01&paymentAmount=20.01&fawryFees=0&orderStatus=UNPAID&paymentMethod=PayAtFawry&expirationTime=1734274148565&customerName=FullName&customerProfileId=&signature=af3d1961f0c60dc3ded691424df4e573c5951cd1a832be694070339c0fd982db&taxes=0&statusCode=200&statusDescription=Operation%20done%20successfully&basketPayment=false
        public async Task<IActionResult> FawryMemberShipResultAsync(int id)
        {
            var currentUser = await userManager.GetUserAsync(User);
            var order = await appRep.MempershipOrdersRep.GetOrder(id);
            order.PostOrderJsonResponse = HttpContext.Request.Query["statusDescription"];
            //var val = HttpContext.Request.QueryString.Value;
            try
            {
                logger.WriteInfo("OrdersController  FawryMemberShipResult: " + HttpContext.Request.Query["statusDescription"]);

                if (HttpContext.Request.Query["statusCode"].ToString() == "200")
                {
                    order.OrderResult = HttpContext.Request.Query["orderStatus"];
                    order.OrderReference = HttpContext.Request.Query["referenceNumber"];
                    await appRep.SaveChangesAsync();

                    //IF PAID ADD TO PAYMENT..
                    if (HttpContext.Request.Query["orderStatus"] == "PAID")
                    {
                        MempershipPayment mempershipPayment = new()
                        {
                            OrderId = id,
                            UserId = currentUser.Id,
                            CardType = HttpContext.Request.Query["paymentMethod"],
                            TotalCapturedAmount = double.Parse(HttpContext.Request.Query["paymentAmount"]),
                        };

                        await appRep.MempershipPaymentsRep.SaveGetOrder(mempershipPayment);
                        await appRep.UserRep.PayMempership(User.FindFirstValue(ClaimTypes.NameIdentifier));

                        await appRep.SaveChangesAsync();
                    }
                    return RedirectToAction("receipt", "Payments", new
                    {
                        area = "Payment",
                        status = HttpContext.Request.Query["orderStatus"],
                        orderNum = HttpContext.Request.Query["merchantRefNumber"],
                        orderId = HttpContext.Request.Query["referenceNumber"],
                        amount = HttpContext.Request.Query["orderAmount"],
                        isFailed = false,
                        ErrorMsg = HttpContext.Request.Query["statusDescription"]
                    });
                }
                else
                {
                    order.OrderResult = "FAILED";
                    await appRep.SaveChangesAsync();

                    return RedirectToAction("receipt", "Payments", new
                    {
                        area = "Payment",
                        orderNum = order.OrderNumber,
                        amount = order.Amount,
                        status = "FAILED",
                        isFailed = true,
                        ErrorMsg = HttpContext.Request.Query["statusDescription"]
                    });
                }
            }
            catch (Exception ex)
            {
                logger.WriteError("OrdersController  FawryMemberShipResult  Exception" + ex.Message.ToString());

                return RedirectToAction("receipt", "Payments", new
                {
                    area = "Payment",
                    orderNum = order.OrderNumber,
                    amount = order.Amount,
                    status = "FAILED",
                    isFailed = true,
                    ErrorMsg = "Can not complete payment now. Check with adminstrator"
                });
            }
        }
        public async Task<IActionResult> FawryCourseResultAsync(int id)
        {
            var currentUser = await userManager.GetUserAsync(User);
            var courseOrder = await appRep.CourseOrdersRep.GetOrder(int.Parse(id.ToString()));
            courseOrder.PostOrderJsonResponse = HttpContext.Request.Query["statusDescription"];
            //var val = HttpContext.Request.QueryString.Value;
            try
            {
                logger.WriteInfo("OrdersController  FawryCourseResult: " + HttpContext.Request.Query["statusDescription"]);

                if (HttpContext.Request.Query["statusCode"].ToString() == "200")
                {
                    courseOrder.OrderResult = HttpContext.Request.Query["orderStatus"];
                    courseOrder.OrderReference = HttpContext.Request.Query["referenceNumber"];
                    await appRep.SaveChangesAsync();

                    //IF PAID ADD TO PAYMENT..
                    if (HttpContext.Request.Query["orderStatus"] == "PAID")
                    {
                        CoursePayment coursePayment = new()
                        {
                            CourseId = courseOrder.CourseId,
                            OrderId = id,
                            UserId = currentUser.Id,
                            CardType = HttpContext.Request.Query["paymentMethod"],
                            TotalCapturedAmount = double.Parse(HttpContext.Request.Query["paymentAmount"]),
                        };

                        await appRep.CoursePaymentsRep.SaveGetOrder(coursePayment);
                        await appRep.UserRep.EnrollCourse(1, courseOrder.CourseId, currentUser.Id, true); //1 state means Enrolled
                        await appRep.SaveChangesAsync();

                        await appRep.UserRep.UpdateUserLevel(currentUser.Id);
                        await appRep.SaveChangesAsync();
                    }
                    return RedirectToAction("receipt", "Payments", new
                    {
                        area = "Payment",
                        status = HttpContext.Request.Query["orderStatus"],
                        orderNum = HttpContext.Request.Query["merchantRefNumber"],
                        orderId = HttpContext.Request.Query["referenceNumber"],
                        amount = HttpContext.Request.Query["orderAmount"],
                        isFailed = false,
                        ErrorMsg = HttpContext.Request.Query["statusDescription"]
                    });
                }
                else
                {
                    courseOrder.OrderResult = "FAILED";
                    await appRep.SaveChangesAsync();

                    return RedirectToAction("receipt", "Payments", new
                    {
                        area = "Payment",
                        orderNum = courseOrder.OrderNumber,
                        amount = courseOrder.Amount,
                        status = "FAILED",
                        isFailed = true,
                        ErrorMsg = HttpContext.Request.Query["statusDescription"]
                    });
                }

            }
            catch (Exception ex)
            {
                logger.WriteError("OrdersController  FawryMemberShipResult  Exception" + ex.Message.ToString());

                return RedirectToAction("receipt", "Payments", new
                {
                    area = "Payment",
                    orderNum = courseOrder.OrderNumber,
                    amount = courseOrder.Amount,
                    status = "FAILED",
                    isFailed = true,
                    ErrorMsg = "Can not complete payment now. Check with adminstrator"
                });
            }
        }
    }
}
