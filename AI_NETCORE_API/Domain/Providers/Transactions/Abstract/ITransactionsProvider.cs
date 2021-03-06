﻿using Domain.Providers.Transactions.Request.Abstract;
using Domain.Providers.Transactions.Response.Abstract;

namespace Domain.Providers.Transactions.Abstract
{
    public interface ITransactionsProvider
    {
        IGetTransactionsByUserIdResponse GetTransactionsByUserId(IGetTransactionsByUserIdRequest getTransactionsByUserIdRequest);
    }
}
