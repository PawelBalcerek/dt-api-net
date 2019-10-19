using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Providers.Transactions.Request.Abstract
{
    public interface IGetTransactionByIdRequest
    {
        int Id { get; }
    }
}
