using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.BusinessObject;
using Domain.Creators.Transaction.Request.Abstract;
using Domain.Creators.Transaction.Request.Concrete;
using Domain.Infrastructure.Logging.Abstract;
using Domain.Infrastructure.TransactionProcessing.Responses.Abstract;
using Domain.Infrastructure.TransactionProcessing.Responses.Concrete;

namespace Domain.Infrastructure.TransactionProcessing
{
    public class TransactionWindow
    {

        public TransactionWindow(IList<BuyOffer> buyOffers, IList<SellOffer> sellOffers, int size)
        {
            BuyOffers = buyOffers;
            SellOffers = sellOffers;
            Size = size;
            TransactionsToSave = new List<ICreateTransactionRequest>();
        }

        private IList<BuyOffer> BuyOffers { get; }
        private IList<SellOffer> SellOffers { get; }
        private IList<ICreateTransactionRequest> TransactionsToSave { get; }
        public int Size { get; }

        public bool IsValid => BuyOffers.Count == Size && SellOffers.Count == Size;


        public IProcessingTransactionWindowResult Process(ILogger logger)
        {
            try
            {
                while (true)
                {
                    SellOffer actualSellOffer = SellOffers.FirstOrDefault(x => x.Amount != 0);
                    if (actualSellOffer == null) return new ProcessingTransactionWindowResult(SellOffers, BuyOffers, TransactionsToSave); ;
                    BuyOffer actualBuyOffer = BuyOffers.FirstOrDefault(x => x.Amount != 0);
                    if (actualBuyOffer == null) return new ProcessingTransactionWindowResult(SellOffers, BuyOffers, TransactionsToSave); ;

                    if (actualBuyOffer.MaxPrice < actualSellOffer.Price)
                    {
                        return new ProcessingTransactionWindowResult(SellOffers, BuyOffers, TransactionsToSave);
                    }

                    bool amountIsTheSame = actualSellOffer.Amount == actualBuyOffer.Amount;

                    if (amountIsTheSame)
                    {
                        ProcessWhenAmountsAreEquals(actualSellOffer, actualBuyOffer);
                    }
                    else
                    {
                        bool sellOfferWillByEntirelyDone = actualSellOffer.Amount < actualBuyOffer.Amount;
                        if (sellOfferWillByEntirelyDone)
                        {
                            ProcessWhenAmountOnBuyOfferIsBigger(actualBuyOffer, actualSellOffer);
                        }
                        else
                        {
                            ProcessWhenAmountOnSellOfferIsBigger(actualSellOffer, actualBuyOffer);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Log(ex);
                return new ProcessingTransactionWindowResult();
            }
        }

        private void ProcessWhenAmountOnSellOfferIsBigger(SellOffer actualSellOffer, BuyOffer actualBuyOffer)
        {
            int quantityToTransaction = actualBuyOffer.Amount;
            MakeChangesOnOffersAndCreateTransaction(actualBuyOffer,actualSellOffer,quantityToTransaction);
        }

        private void ProcessWhenAmountOnBuyOfferIsBigger(BuyOffer actualBuyOffer, SellOffer actualSellOffer)
        {
            int quantityToTransaction =actualSellOffer.Amount;
            MakeChangesOnOffersAndCreateTransaction(actualBuyOffer,actualSellOffer,quantityToTransaction);
        }

        private void MakeChangesOnOffersAndCreateTransaction(BuyOffer actualBuyOffer, SellOffer actualSellOffer,int quantityToTransaction)
        {
            TransactionsToSave.Add(new CreateTransactionRequest(actualSellOffer.Id, actualBuyOffer.Id,
                actualSellOffer.Price, quantityToTransaction));
            actualSellOffer.UpdateActualAmount(actualSellOffer.Amount - quantityToTransaction);
            actualBuyOffer.UpdateActualAmount(actualBuyOffer.Amount - quantityToTransaction);
        }
        private void ProcessWhenAmountsAreEquals(SellOffer actualSellOffer, BuyOffer actualBuyOffer)
        {
            int quantity = actualSellOffer.Amount;
            MakeChangesOnOffersAndCreateTransaction(actualBuyOffer,actualSellOffer,quantity);
        }
    }
}
