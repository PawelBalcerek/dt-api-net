using Domain.Providers.Transactions.Response.Concrete;
using Domain.BusinessObject;
using Domain.Infrastructure.Logging.Abstract;
using Domain.Providers.Transactions.Abstract;
using Domain.Providers.Transactions.Request.Abstract;
using Domain.Providers.Transactions.Response.Abstract;
using Domain.Repositories.TransactionRepo.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Providers.Transactions.Concrete
{
    public class TransactionProvider : ITransactionsProvider
    {
        private readonly ILogger _logger;
        private readonly ITransactionRepository _transactions;
        public TransactionProvider(ILogger logger, ITransactionRepository transactions)
        {
            _logger = logger;
            _transactions = transactions;
        }

        public IGetTransactionByIdResponse GetTransactionById(IGetTransactionByIdRequest getTransactionByIdRequest)
        {
            try
            {
                return new GetTransactionByIdResponse(_transactions.GetTransactionById(getTransactionByIdRequest.Id));
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                return new GetTransactionByIdResponse();
            }
        }

        public IGetTransactionsResponse GetTransactions()
        {
            try
            {
                return new GetTransactionsResponse(_transactions.GetAllTransactions().ToList());
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                return new GetTransactionsResponse();
            }
        }

        public IGetTransactionsByUserIdResponse GetTransactionsByUserId(IGetTransactionsByUserIdRequest getTransactionsByUserIdRequest)
        {
            try
            {
                var result = _transactions.GetTransactionsByUserId(getTransactionsByUserIdRequest.UserId);
                return new GetTransactionsByUserIdResponse(result.Object.ToList(), result.DatabaseTime);

            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                return new GetTransactionsByUserIdResponse();
            }
        }
    }
}
