using System;
using System.Collections.Generic;
using System.Text;
using Domain.BusinessObject;
using Domain.Infrastructure.TransactionProcessing.Responses.Abstract;

namespace Domain.Infrastructure.TransactionProcessing.Responses.Concrete
{
    public class ProcessingTransactionWindowResult : IProcessingTransactionWindowResult
    {
        public ProcessingTransactionWindowResult(IList<SellOffer> sellOffersToSave, IList<BuyOffer> buyOffersToSave, IList<Transaction> transactionsToSave)
        {
            SellOffersToSave = sellOffersToSave;
            BuyOffersToSave = buyOffersToSave;
            TransactionsToSave = transactionsToSave;
        }

        public ProcessingTransactionWindowResult()
        {
        }

        public IList<SellOffer> SellOffersToSave { get; }
        public IList<BuyOffer> BuyOffersToSave { get; }
        public IList<Transaction> TransactionsToSave { get; }

        public bool SomethingDone => (SellOffersToSave == null || SellOffersToSave.Count == 0) &&
                                     (BuyOffersToSave == null || BuyOffersToSave.Count == 0) &&
                                     (TransactionsToSave == null || TransactionsToSave.Count == 0);
    }
}
