using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public TransactionsController(ILogger logger, ITransactionsProvider transactionsProvider)
        {
            _logger = logger;
            _transactionsProvider = transactionsProvider;
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
                //return PrepareResponseAfterGetUserById(getUserByIdResponse);
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
        public ActionResult<IList<TransactionModel>> GetTransaction()
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
                    return Ok(getTransactionsResponse.Transactions.ToList().Select(x => new TransactionModel {
                         Id = x.Id,
                         Amount = x.Amount,
                         BuyOfferId = x.BuyOfferId,
                         Date = x.Date,
                         Price = x.Price,
                         SellOfferId = x.SellOfferId
                    }));
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
                    
                    return Ok(new TransactionModel
                    {
                        Id = getTransactionByIdResponse.Transaction.Id,
                        Amount = getTransactionByIdResponse.Transaction.Amount,
                        BuyOfferId = getTransactionByIdResponse.Transaction.BuyOfferId,
                        Date = getTransactionByIdResponse.Transaction.Date,
                        Price = getTransactionByIdResponse.Transaction.Price,
                        SellOfferId = getTransactionByIdResponse.Transaction.SellOfferId
                    });
                case Data.Providers.Common.Enum.ProvideEnumResult.NotFound:
                    return StatusCode(404);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}