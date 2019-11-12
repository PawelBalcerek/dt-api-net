using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AI_NETCORE_API.Infrastructure.BuisnessObjectToModelsConverting.Abstract;
using AI_NETCORE_API.Models.Objects;
using AI_NETCORE_API.Models.Response.ExecutingTimes;
using AI_NETCORE_API.Models.Response.Transactions;
using Domain.BusinessObject;
using Domain.Infrastructure.Logging.Abstract;
using Domain.Providers.Transactions.Abstract;
using Domain.Providers.Transactions.Request.Abstract;
using Domain.Providers.Transactions.Request.Concrete;
using Domain.Providers.Transactions.Response.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AI_NETCORE_API.Controllers
{
    [Route("api/")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly ITransactionsProvider _transactionsProvider;
        private readonly IBusinessObjectToModelsConverter _businessObjectToModelsConverter;

        public TransactionsController(ILogger logger, ITransactionsProvider transactionsProvider, IBusinessObjectToModelsConverter businessObjectToModelsConverter)
        {
            _logger = logger;
            _transactionsProvider = transactionsProvider;
            _businessObjectToModelsConverter = businessObjectToModelsConverter;
        }

        /// <summary>
        /// Method to get user's transactions
        /// </summary>
        /// <returns>Transactions of user.</returns>
        [ProducesResponseType(200, Type = typeof(GetTransactionsByUserIdResponseModel))]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        [HttpGet("users/transactions")]
        [Authorize("Bearer")]
        public ActionResult<IList<TransactionModel>> GetSellOffersByUserId()
        {
            try
            {
                Stopwatch timer = Stopwatch.StartNew();
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                var userId = int.Parse(identity.Claims.Where(c => c.Type == "Id").FirstOrDefault().Value);
                IGetTransactionsByUserIdRequest request = new GetTransactionsByUserIdRequest(userId);
                IGetTransactionsByUserIdResponse getTransactionsByUserIdResponse = _transactionsProvider.GetTransactionsByUserId(request);
                return PrepareResponseAfterGetTransactionsByUserId(getTransactionsByUserIdResponse, timer);
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                return StatusCode(500);
            }
        }

        private ActionResult<IList<TransactionModel>> PrepareResponseAfterGetTransactionsByUserId(IGetTransactionsByUserIdResponse getUserByIdResponse, Stopwatch timer)
        {
            switch (getUserByIdResponse.ProvideResult)
            {
                case Domain.Providers.Common.Enum.ProvideEnumResult.Exception:
                    return StatusCode(500);
                case Domain.Providers.Common.Enum.ProvideEnumResult.Success:
                    GetTransactionsByUserIdResponseModel response = PrepareSuccessResponseAfterGetTransactionsByUserId(getUserByIdResponse, timer);
                    return Ok(response);
                case Domain.Providers.Common.Enum.ProvideEnumResult.NotFound:
                    return StatusCode(404);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private GetTransactionsByUserIdResponseModel PrepareSuccessResponseAfterGetTransactionsByUserId(IGetTransactionsByUserIdResponse getUserByIdResponse, Stopwatch timer)
        {
            IList<TransactionModel> resourceModelsList = getUserByIdResponse.Transactions
                .Select(x => _businessObjectToModelsConverter.ConvertTransaction(x)).ToList();
            timer.Stop();

            GetTransactionsByUserIdResponseModel response = new GetTransactionsByUserIdResponseModel
            {
                Transactions = resourceModelsList,
                ExecDetails = new ExecutionDetails
                {
                    DbTime = getUserByIdResponse.DatabaseExecutionTime,
                    ExecTime = timer.ElapsedMilliseconds
                }
            };
            return response;
        }
    }
}