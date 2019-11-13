using Domain.Providers.Transactions.Request.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Providers.Transactions.Request.Concrete
{
    public class GetTransactionsByUserIdRequest : IGetTransactionsByUserIdRequest
    {
        public GetTransactionsByUserIdRequest(int userId)
        {
            UserId = userId;
        }

        public int UserId { get; }
    }
}
