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
                for (int i = 0; i < Size; i++)
                {
                    SellOffer actualSellOffer = SellOffers.FirstOrDefault(x => x.Amount != 0);
                    if (actualSellOffer == null) return null;
                    BuyOffer actualBuyOffer = BuyOffers.FirstOrDefault(x => x.Amount != 0);
                    if (actualBuyOffer == null) return null;

                    if (actualBuyOffer.MaxPrice < actualSellOffer.Price)
                    {
                        return null; // TODO cannot create any transaction
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

                return new ProcessingTransactionWindowResult(SellOffers, BuyOffers, TransactionsToSave);
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
            TransactionsToSave.Add(new CreateTransactionRequest(actualSellOffer.Id, actualBuyOffer.Id,
                actualSellOffer.Price, quantityToTransaction));
            actualBuyOffer.UpdateActualAmount(quantityToTransaction);
        }

        private void ProcessWhenAmountOnBuyOfferIsBigger(BuyOffer actualBuyOffer, SellOffer actualSellOffer)
        {
            int quantityToTransaction =actualSellOffer.Amount;
            TransactionsToSave.Add(new CreateTransactionRequest(actualSellOffer.Id, actualBuyOffer.Id,
                actualSellOffer.Price, quantityToTransaction));
            actualSellOffer.UpdateActualAmount(actualSellOffer.Amount - quantityToTransaction);
        }

        private void ProcessWhenAmountsAreEquals(SellOffer actualSellOffer, BuyOffer actualBuyOffer
            )
        {
            TransactionsToSave.Add(new CreateTransactionRequest(actualSellOffer.Id, actualBuyOffer.Id,
                actualSellOffer.Price, actualSellOffer.Amount));
            actualSellOffer.UpdateActualAmount(0);
            actualBuyOffer.UpdateActualAmount(0);
        }
    }
}
