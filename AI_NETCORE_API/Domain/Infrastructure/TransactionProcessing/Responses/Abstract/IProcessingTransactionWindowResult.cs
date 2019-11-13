using System;
using System.Collections.Generic;
using System.Text;
using Domain.BusinessObject;
using Domain.Creators.Transaction.Request.Abstract;

namespace Domain.Infrastructure.TransactionProcessing.Responses.Abstract
{
    public interface IProcessingTransactionWindowResult
    {
        IList<SellOffer> SellOffersToSave { get; }
        IList<BuyOffer> BuyOffersToSave { get; }
        IList<ICreateTransactionRequest> TransactionsToSave { get; } // TODO change to Transaction Create Request
        bool SomethingDone { get; }
    }
}
