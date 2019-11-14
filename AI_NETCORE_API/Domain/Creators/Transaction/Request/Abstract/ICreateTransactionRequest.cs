using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Creators.Transaction.Request.Abstract
{
    public interface ICreateTransactionRequest
    {
        int SellOfferId { get; }
        int BuyOfferId { get; }
        DateTime CreationDate { get; }
        double Price { get; }
        int Amount { get; }

    }

}
