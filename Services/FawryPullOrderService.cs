using ESTA.Areas.Payment.Data;
using ESTA.Areas.Payment.Models;
using ESTA.Areas.Payment.Repository.IRespository;
using ESTA.Models;
using ESTA.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Security.Claims;

namespace ESTA.Services
{
    public class FawryPullOrderService : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<FawryPullOrderService> _logger;
        private readonly IConfiguration _conf;
        private readonly TimeSpan _interval = TimeSpan.FromMinutes(5);
        //private readonly List<string> OrdersList = new() { "7820b2", "0bc6d4" };

        public FawryPullOrderService(ILogger<FawryPullOrderService> logger, IConfiguration conf, IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _conf = conf;
            _serviceScopeFactory = serviceScopeFactory;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("FawryPullOrderService started. Time: {Time}", DateTime.Now.TimeOfDay);

            using var timer = new PeriodicTimer(_interval);

            while (await timer.WaitForNextTickAsync(stoppingToken))
            {
                if (stoppingToken.IsCancellationRequested)
                    break;

                _logger.LogInformation("Starting DoWorkAsync...");

                try
                {
                    using var scope = _serviceScopeFactory.CreateScope();
                    var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
                    await DoWorkAsync(unitOfWork);
                    _logger.LogInformation("DoWorkAsync completed successfully.");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred during DoWorkAsync execution.");
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
                    }
                }
            }
        }
        private async Task<FawryCallBack?> PostJson(string uri, FawrypayRequest postParameters)
        {
            HttpClient _httpClient = new()
            {
                Timeout = TimeSpan.FromSeconds(3),
            };

            try
            {
                string postData = $"?merchantCode={postParameters.MerchantCode}&merchantRefNumber={postParameters.MerchantRefNum}&signature={postParameters.Signature}";
                _logger.LogInformation(uri + postData);
                HttpResponseMessage httpResponse = await _httpClient.GetAsync(uri + postData);
                httpResponse.EnsureSuccessStatusCode();

                _logger.LogInformation("After Calling FAWRY PULL result. StatusCode: {StatusCode}", httpResponse.StatusCode);

                var strRes = await httpResponse.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<FawryCallBack>(strRes);
            }
            catch (TaskCanceledException)
            {
                _logger.LogInformation("After Calling FAWRY PULL result. Timeout");

                throw;
            }
            catch (Exception e)
            {
                _logger.LogInformation("After Calling FAWRY PULL result. {exception}", e.Message);

                throw;
            }
        }
    }
}
