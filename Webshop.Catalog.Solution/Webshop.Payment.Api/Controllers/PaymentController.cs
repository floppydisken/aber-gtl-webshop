using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Webshop.Domain.Common;
using Webshop.Payment.Api.Models;
using Webshop.Payment.Api.Repository;
using Webshop.Payment.Api.Services;

namespace Webshop.Payment.Api.Controllers
{
    [Route("api/payment")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private IMemoryRepository transactionRepository;
        private IThrottleService throttleService;
        private ILogger<PaymentController> logger;
        public PaymentController(
            IMemoryRepository transactionRepository,
            IThrottleService throttleService,
            ILogger<PaymentController> logger
        )
        {
            this.throttleService = throttleService;
            this.transactionRepository = transactionRepository;
            this.logger = logger;
        }

        [HttpPost]
        [Route("process")]
        public Result<Transaction> ProcessPayment(PaymentRequest request)
        {
            //check for throttling
            if (!this.throttleService.CanExecute())
            {
                return Result.Fail<Transaction>("The Payment service is not ready");
            }
            //simulate process
            Result<Models.Payment> paymentResult = Models.Payment.Create(request.CardNumber, request.ExpirationDate, request.CVC);
            if (paymentResult.Success)
            {
                Result<Transaction> transactionResult = Transaction.Create(request.Amount, paymentResult.Value);
                if (transactionResult.Success)
                {
                    Result result = this.transactionRepository.AddTransaction(transactionResult.Value);
                    if (result.Failure)
                    {
                        return Result.Fail<Transaction>(result.Error);
                    }
                }
                return transactionResult;
            }
            else
            {
                logger.LogWarning("Unable to create new Payment object with the following error: {error}", new { error = paymentResult.Error });
                return Result.Fail<Transaction>(paymentResult.Error);
            }
        }
    }
}
