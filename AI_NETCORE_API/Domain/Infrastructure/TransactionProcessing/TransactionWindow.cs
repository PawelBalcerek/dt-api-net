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
        public TransactionWindow(IList<BuyOffer> buyOffers, IList<SellOffer> sellOffers,int size)
        {
            BuyOffers = buyOffers;
            SellOffers = sellOffers;
            Size = size;
        }

        public IList<BuyOffer> BuyOffers { get; }
        public IList<SellOffer> SellOffers { get; }
        public int Size { get; }

        public bool IsValid => BuyOffers.Count == Size && SellOffers.Count == Size;


        public IProcessingTransactionWindowResult Process(ILogger logger)
        {
            try
            {
                IList<SellOffer> sellOffersToDelete = new List<SellOffer>();
                IList<BuyOffer> buyOffersToDelete = new List<BuyOffer>();
                IList<ICreateTransactionRequest>transactionsToSave = new List<ICreateTransactionRequest>(); // TODO MUST BE : Change this to transactionCreateRequest
                for (int i = 0; i < Size; i++)
                {
                    SellOffer actualSellOffer = SellOffers.FirstOrDefault();
                    if (actualSellOffer == null) return null;
                    BuyOffer actualBuyOffer = BuyOffers.FirstOrDefault();
                    if (actualBuyOffer == null) return null;

                    if (actualBuyOffer.MaxPrice < actualSellOffer.Price)
                    {
                        return null; // TODO cannot create any transaction
                    }

                    bool amountIsTheSame = actualSellOffer.Amount == actualBuyOffer.Amount;

                    if (amountIsTheSame)
                    {
                        ProcessWhenAmountsAreEquals(transactionsToSave, actualSellOffer, actualBuyOffer, sellOffersToDelete, buyOffersToDelete);
                    }

                    bool sellOfferWillByEntirelyDone = actualSellOffer.Amount < actualBuyOffer.Amount;
                    if (sellOfferWillByEntirelyDone)
                    {
                        ProcessWhenAmountOnBuyOfferIsBigger(actualBuyOffer, actualSellOffer, transactionsToSave);
                    }
                    else
                    {
                        ProcessWhenAmountOnSellOfferIsBigger(actualSellOffer, actualBuyOffer, transactionsToSave);
                    }
                }

                return new ProcessingTransactionWindowResult(sellOffersToDelete, buyOffersToDelete, transactionsToSave);
            }
            catch (Exception ex)
            {
                logger.Log(ex);
                return new ProcessingTransactionWindowResult();
            }
        }

        private void ProcessWhenAmountOnSellOfferIsBigger(SellOffer actualSellOffer, BuyOffer actualBuyOffer,
            IList<ICreateTransactionRequest> transactionsToSave)
        {
            int quantityToTransaction = actualSellOffer.Amount - actualBuyOffer.Amount;
            transactionsToSave.Add(new CreateTransactionRequest(actualSellOffer.Id, actualBuyOffer.Id,
                actualSellOffer.Price, quantityToTransaction));
            actualBuyOffer.UpdateActualAmount(quantityToTransaction);
            BuyOffers.Remove(actualBuyOffer);
        }

        private void ProcessWhenAmountOnBuyOfferIsBigger(BuyOffer actualBuyOffer, SellOffer actualSellOffer,
            IList<ICreateTransactionRequest> transactionsToSave)
        {
            int quantityToTransaction = actualBuyOffer.Amount - actualSellOffer.Amount;
            transactionsToSave.Add(new CreateTransactionRequest(actualSellOffer.Id, actualBuyOffer.Id,
                actualSellOffer.Price, quantityToTransaction));
            actualSellOffer.UpdateActualAmount(actualSellOffer.Amount - quantityToTransaction);
            SellOffers.Remove(actualSellOffer);
        }

        private void ProcessWhenAmountsAreEquals(IList<ICreateTransactionRequest> transactionsToSave, SellOffer actualSellOffer, BuyOffer actualBuyOffer,
            IList<SellOffer> sellOffersToDelete, IList<BuyOffer> buyOffersToDelete)
        {
            transactionsToSave.Add(new CreateTransactionRequest(actualSellOffer.Id, actualBuyOffer.Id,
                actualSellOffer.Price, actualSellOffer.Amount));
            sellOffersToDelete.Add(actualSellOffer);
            SellOffers.Remove(actualSellOffer);
            buyOffersToDelete.Add(actualBuyOffer);
            BuyOffers.Remove(actualBuyOffer);
        }
    }
}
