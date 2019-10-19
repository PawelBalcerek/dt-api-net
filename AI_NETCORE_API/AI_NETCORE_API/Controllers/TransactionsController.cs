using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AI_NETCORE_API.Infrastructure.BuisnessObjectToModelsConverting.Abstract;
using AI_NETCORE_API.Models.Objects;
using Data.BuisnessObject;
using Data.Infrastructure.Logging.Abstract;
using Data.Providers.Transactions.Abstract;
using Data.Providers.Transactions.Request.Abstract;
using Data.Providers.Transactions.Request.Concrete;
using Data.Providers.Transactions.Response.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AI_NETCORE_API.Controllers
{
    [Route("api/[controller]")]
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

        [HttpGet("{id:int}")]
        [ProducesResponseType(200, Type = typeof(TransactionModel))]
        [ProducesResponseType(500)]
        [ProducesResponseType(404)]
        public ActionResult<TransactionModel> GetTransactionById(int id)
        {
            try
            {
                IGetTransactionByIdRequest getTransactionByIdRequest = new GetTransactionByIdRequest(id);
                IGetTransactionByIdResponse getTransactionByIdResponse = _transactionsProvider.GetTransactionById(getTransactionByIdRequest);
                return PrepareResponseAfterGetTransactionById(getTransactionByIdResponse);
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                return StatusCode(500);
            }
        }

        [HttpGet("")]
        [ProducesResponseType(200, Type = typeof(IList<TransactionModel>))]
        [ProducesResponseType(500)]
        public ActionResult<IList<TransactionModel>> GetTransactions()
        {
            try
            {
                IGetTransactionsResponse getTransactionsResponse = _transactionsProvider.GetTransactions();
                return PrepareResponseAfterGetTransactions(getTransactionsResponse);
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                return StatusCode(500);
            }
        }

        private ActionResult<IList<TransactionModel>> PrepareResponseAfterGetTransactions(IGetTransactionsResponse getTransactionsResponse)
        {
            switch (getTransactionsResponse.ProvideResult)
            {
                case Data.Providers.Common.Enum.ProvideEnumResult.Exception:
                    return StatusCode(500);
                case Data.Providers.Common.Enum.ProvideEnumResult.Success:
                    return Ok(getTransactionsResponse.Transactions.ToList()
                        .Select(x => _businessObjectToModelsConverter.ConvertTransaction(x)));
                case Data.Providers.Common.Enum.ProvideEnumResult.NotFound:
                    return StatusCode(404);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private ActionResult<TransactionModel> PrepareResponseAfterGetTransactionById(IGetTransactionByIdResponse getTransactionByIdResponse)
        {
            switch (getTransactionByIdResponse.ProvideResult)
            {
                case Data.Providers.Common.Enum.ProvideEnumResult.Exception:
                    return StatusCode(500);
                case Data.Providers.Common.Enum.ProvideEnumResult.Success:

                    return Ok(_businessObjectToModelsConverter.ConvertTransaction(getTransactionByIdResponse.Transaction));                 case Data.Providers.Common.Enum.ProvideEnumResult.NotFound:
                    return StatusCode(404);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}