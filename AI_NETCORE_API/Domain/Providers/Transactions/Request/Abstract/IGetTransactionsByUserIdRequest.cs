using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Providers.Transactions.Request.Abstract
{
    public interface IGetTransactionsByUserIdRequest
    {
        int UserId { get; }
    }
}
