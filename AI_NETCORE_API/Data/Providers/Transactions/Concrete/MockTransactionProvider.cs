using Domain.Providers.Transactions.Response.Concrete;
using Domain.BusinessObject;
using Domain.Infrastructure.Logging.Abstract;
using Domain.Providers.Transactions.Abstract;
using Domain.Providers.Transactions.Request.Abstract;
using Domain.Providers.Transactions.Response.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Providers.Transactions.Concrete
{
    public class MockTransactionProvider : ITransactionsProvider
    {
        private readonly ILogger _logger;
        private readonly IList<Transaction> _transactions;
        public MockTransactionProvider(ILogger logger)
        {
            _logger = logger;
            _transactions = PrepareMockedTransactions();
        }

        public IGetTransactionByIdResponse GetTransactionById(IGetTransactionByIdRequest getTransactionByIdRequest)
        {
            try
            {
                return new GetTransactionByIdResponse(_transactions.ToList().FirstOrDefault(x=> x.Id == getTransactionByIdRequest.Id));
            }
            catch(Exception ex)
            {
                _logger.Log(ex);
                return new GetTransactionByIdResponse();
            }
        }

        public IGetTransactionsResponse GetTransactions()
        {
            try
            {
                return new GetTransactionsResponse(_transactions);
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                return new GetTransactionsResponse();
            }
        }

        private static IList<Transaction> PrepareMockedTransactions()
        {
            return new List<Transaction>
            {
                new Transaction(1,512,1024,DateTime.Now,320,2000),
                new Transaction(2,256,312,DateTime.Now,15,3000),
                new Transaction(3,513,1023,DateTime.Now,40,55)
                
            };
        }
    }
}
