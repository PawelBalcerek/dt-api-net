using Data.Providers.Transactions.Request.Abstract;
using Data.Providers.Transactions.Response.Abstract;

namespace Data.Providers.Transactions.Abstract
{
    public interface ITransactionsProvider
    {
        IGetTransactionByIdResponse GetTransactionById(IGetTransactionByIdRequest getTransactionByIdRequest);
        IGetTransactionsResponse GetTransactions();
    }
}
