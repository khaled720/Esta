using ESTA.Areas.Payment.Data;
using ESTA.Areas.Payment.Models;
using ESTA.Areas.Payment.Repository.IRespository;
using ESTA.Helpers;
using ESTA.Models;
using ESTA.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System.Net;
using System.Security.Authentication;
using System.Security.Claims;

namespace ESTA.Services
{
    public class FawryPullOrderService : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly LogManager<FawryPullOrderService> logger;
        private readonly IConfiguration _conf;
        private readonly TimeSpan _interval = TimeSpan.FromMinutes(30);

        public FawryPullOrderService(IConfiguration conf, LogManager<FawryPullOrderService> _logger, IServiceScopeFactory serviceScopeFactory)
        {
            _conf = conf;
            _serviceScopeFactory = serviceScopeFactory;
            logger = _logger;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            logger.WriteInfo("Starting FawryPullOrderService...");

            using var timer = new PeriodicTimer(_interval);

            while (await timer.WaitForNextTickAsync(stoppingToken))
            {
                if (stoppingToken.IsCancellationRequested)
                    break;

                logger.WriteInfo("Starting DoWorkAsync...");

                try
                {
                    using var scope = _serviceScopeFactory.CreateScope();
                    var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
                    await DoWorkAsync(unitOfWork);
                    logger.WriteInfo("DoWorkAsync completed successfully.");
                }
                catch (Exception ex)
                {
                    logger.WriteError("An error occurred during DoWorkAsync execution." + ex.Message);
                    logger.WriteError("An error occurred during DoWorkAsync execution." + ex.InnerException);
                }
            }
        }
        // Could also be a async method, that can be awaited in ExecuteAsync above
        private async Task DoWorkAsync(IUnitOfWork _appRep)
        {
            string SecureKey = _conf.GetValue<string>("Fawry:SecureKey");
            string FawryUri = _conf.GetValue<string>("Fawry:PullUpdates");
            string MerchCode = _conf.GetValue<string>("Fawry:MerchantCode");

            var OrdersList = await _appRep.MempershipOrdersRep.GetUnpaidMempershipFawryOrders();
            foreach (var order in OrdersList)
            {
                var Req = new FawrypayRequest
                {
                    MerchantCode = MerchCode,
                    MerchantRefNum = order.OrderNumber,
                };
                Req.CreateFawrySignature(SecureKey);
                var ResultData = await PostJson(FawryUri,
                    Req);
                //ResultData.orderStatus == "PAID"
                //Update the db with ResultData.orderStatus
                if (ResultData != null)
                {
                    order.OrderResult = ResultData.orderStatus;
                    await _appRep.SaveChangesAsync();

                    if (ResultData.orderStatus == "PAID")
                    {
                        MempershipPayment mempershipPayment = new()
                        {
                            OrderId = order.Id,
                            UserId = order.UserId,
                            CardType = ResultData.paymentMethod,
                            TotalCapturedAmount = ResultData.paymentAmount,
                        };

                        await _appRep.MempershipPaymentsRep.SaveGetOrder(mempershipPayment);
                        await _appRep.UserRep.PayMempership(order.UserId);

                        await _appRep.SaveChangesAsync();

                        SendEmail(ResultData.customerMail, string.Empty, string.Empty, order.OrderNumber, order.Amount.ToString(), ResultData.orderStatus);
                    }
                }
            }

            var CoursesOrdersList = await _appRep.CourseOrdersRep.GetUnpaidCoursesFawryOrders();
            foreach (var order in CoursesOrdersList)
            {
                var Req = new FawrypayRequest
                {
                    MerchantCode = MerchCode,
                    MerchantRefNum = order.OrderNumber,
                };
                Req.CreateFawrySignature(SecureKey);
                var ResultData = await PostJson(FawryUri,
                    Req);
                //ResultData.orderStatus == "PAID"
                //Update the db with ResultData.orderStatus
                if (ResultData != null)
                {
                    order.OrderResult = ResultData.orderStatus;
                    await _appRep.SaveChangesAsync();

                    if (ResultData.orderStatus == "PAID")
                    {
                        CoursePayment coursePayment = new()
                        {
                            CourseId = order.CourseId,
                            OrderId = order.Id,
                            UserId = order.UserId,
                            CardType = ResultData.paymentMethod,
                            TotalCapturedAmount = ResultData.paymentAmount,
                        };

                        await _appRep.CoursePaymentsRep.SaveGetOrder(coursePayment);
                        await _appRep.UserRep.EnrollCourse(1, order.CourseId, order.UserId, true); //1 state means Enrolled
                        await _appRep.SaveChangesAsync();

                        await _appRep.UserRep.UpdateUserLevel(order.UserId);
                        await _appRep.SaveChangesAsync();

                        var course = await _appRep.CoursesRep.GetCourse(order.CourseId);

                        SendEmail(ResultData.customerMail, course.Title, course.level.TypeName, order.OrderNumber, order.Amount.ToString(), ResultData.orderStatus);
                    }
                }
            }
        }
        private void SendEmail(string Email, string CourseName, string CourseLevelName, string orderNum, string amount, string status)
        {
            if (!string.IsNullOrEmpty(CourseName) && !string.IsNullOrEmpty(CourseLevelName))
            {
                EmailSender.Send_Mail(Email
                    , "<h3>Course Name :<p>" + CourseName + "</p></h3>"
                    + "<h3>Course Level :<p>" + CourseLevelName + "</p></h3>"
                    + "<h3>Order Number :<p>" + orderNum + "</p></h3>"
                    + "<h3>Amount :<p>" + amount + "</p></h3>"
                    + "<h3>Status:<p>" + status + "</p></h3>"
                    , "Esta Payment Receipt", "ESTA");
            }
            else
            {
                EmailSender.Send_Mail(Email
              , "<h3>Order Number :<p>" + orderNum + "</p></h3>"
              + "<h3>Amount :<p>" + amount + "</p></h3>"
              + "<h3>Status:<p>" + status + "</p></h3>"
              , "Esta Payment Receipt", "ESTA");
            }
        }
        private async Task<FawryCallBack?> PostJson(string uri, FawrypayRequest postParameters)
        {            
            // First create a proxy object
            var proxy = new WebProxy
            {
                Address = new Uri("http://10.50.77.77:8080"),
                BypassProxyOnLocal = false,
                //UseDefaultCredentials = false,

                // *** These creds are given to the proxy server, not the web server ***
                //Credentials = new NetworkCredential(
                //    userName: proxyUserName,
                //    password: proxyPassword)
            };
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var handler = new HttpClientHandler
            {
                Proxy = proxy,
                UseProxy = true,
                SslProtocols = SslProtocols.Tls12,
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) =>
                {
                    logger.WriteInfo($"Certificate: {cert}");
                    logger.WriteInfo($"Certificate: {cert.Version}");
                    logger.WriteInfo($"Errors: {sslPolicyErrors}");
                    return sslPolicyErrors == System.Net.Security.SslPolicyErrors.None;
                }
            };

            var _httpClient = new HttpClient(handler)
            {
                Timeout = TimeSpan.FromSeconds(10),
            };

            try
            {
                string postData = $"?merchantCode={postParameters.MerchantCode}&merchantRefNumber={postParameters.MerchantRefNum}&signature={postParameters.Signature}";
                logger.WriteInfo($"After Calling FAWRY PULL result. request: {uri + postData}");
                HttpResponseMessage httpResponse = await _httpClient.GetAsync(uri + postData);

                logger.WriteInfo($"After Calling FAWRY PULL result. StatusCode: {httpResponse.StatusCode}");

                httpResponse.EnsureSuccessStatusCode();

                var strRes = await httpResponse.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<FawryCallBack>(strRes);
            }
            catch (TaskCanceledException)
            {
                logger.WriteError("After Calling FAWRY PULL result. Timeout");

                throw;
            }
            catch (Exception e)
            {
                logger.WriteError($"After Calling FAWRY PULL result. {e.Message}");
                logger.WriteError($"After Calling FAWRY PULL result. {e.InnerException}");

                throw;
            }
        }
    }
}
