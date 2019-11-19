using System;
using System.Collections.Generic;
using System.Text;
using Domain.Repositories.BaseRepo.Concrete;
using Domain.Repositories.TransactionRepo.Abstract;
using Data.Models;
using Domain.DTOToBOConverting;
using System.Linq;
using System.Diagnostics;
using System.Transactions;
using Domain.Creators.Transaction.Request.Abstract;
using Domain.Repositories.BaseRepo.Response;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Transaction = Data.Models.Transaction;

namespace Domain.Repositories.TransactionRepo.Concrete
{
    public class TransactionRepository: RepositoryBase<Transaction>, ITransactionRepository
    {
        private readonly IDTOToBOConverter _converter;
        

        public TransactionRepository(RepositoryContext repositoryContext, IDTOToBOConverter converter)
            : base(repositoryContext)
        {
            _converter = converter;
        }

        public RepositoryResponse<IEnumerable<BusinessObject.Transaction>> GetTransactionsByUserId(int id)
        {
            using (var databaseContext = new RepositoryContext())
            {
                var timer = Stopwatch.StartNew();
                var transactions = FindByCondition(p => p.SellOffer.Resource.UserId == id).Include(p => p.SellOffer).Include(p => p.SellOffer.Resource).Include(p => p.SellOffer.Resource.Comp).Select(p => _converter.ConvertTransaction(p));
                timer.Stop();
                var time = timer.ElapsedMilliseconds;
                return new RepositoryResponse<IEnumerable<BusinessObject.Transaction>>(transactions, time);
            }
        }

        public long SaveTransactionsAfterProcessing(IList<BusinessObject.SellOffer> sellOffersToSave,
            IList<BusinessObject.BuyOffer> buyOffersToSave, IList<ICreateTransactionRequest> transactionsToSave)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            using (var transactionScope = RepositoryContext.Database.BeginTransaction())
            {
                foreach (ICreateTransactionRequest transactionRequest in transactionsToSave)
                {

                    Transaction transaction = new Transaction
                    {
                        Amount = transactionRequest.Amount,
                        Date = transactionRequest.CreationDate,
                        Price = transactionRequest.Price,
                        BuyOfferId = transactionRequest.BuyOfferId,
                        SellOfferId = transactionRequest.SellOfferId
                    };
                    RepositoryContext.Transactions.Add(transaction);

                    RepositoryContext.SaveChanges();

                    BusinessObject.SellOffer sellOffer =
                        sellOffersToSave.FirstOrDefault(x => x.Id == transactionRequest.SellOfferId);

                    if (sellOffer == null)
                        throw new InvalidOperationException("Cannot create transaction without sellOffer");

                    //SellOffer sellOfferFromDatabase = new SellOffer
                    //{
                    //    Id = sellOffer.Id,
                    //    Amount = sellOffer.Amount,
                    //    Price = sellOffer.Price,
                    //    ResourceId = sellOffer.ResourceId,
                    //    Date = sellOffer.Date,
                    //    IsValid = sellOffer.IsValid,
                    //    StartAmount = sellOffer.StartAmount

                    //};
                    SellOffer sellOfferFromDatabase =
                        RepositoryContext.SellOffers.FirstOrDefault(x => x.Id == transactionRequest.SellOfferId);
                    //RepositoryContext.SellOffers.Attach(sellOfferFromDatabase);
                    if (sellOfferFromDatabase == null)
                        throw new InvalidOperationException("Cannot create transaction without sellOffer - from database not found");

                    sellOfferFromDatabase.Amount = sellOffer.Amount;

                    //RepositoryContext.SellOffers.Attach(sellOfferFromDatabase);

                    RepositoryContext.SaveChanges();

                    Resource sellOfferResource =
                        RepositoryContext.Resources.FirstOrDefault(x => x.Id == sellOffer.ResourceId);



                    if (sellOfferResource == null)
                        throw new InvalidOperationException("Resources from sellOffer not found");

                    sellOfferResource.Amount -= transaction.Amount;
                    //RepositoryContext.Resources.Attach(sellOfferResource);

                    RepositoryContext.SaveChanges();


                    BusinessObject.BuyOffer buyOffer =
                        buyOffersToSave.FirstOrDefault(x => x.Id == transactionRequest.BuyOfferId);

                    if (buyOffer == null)
                        throw new InvalidOperationException("Cannot create transaction without buyOffer");


                    //BuyOffer buyOfferFromDatabase = new BuyOffer
                    //{
                    //    Id = buyOffer.Id,
                    //    Amount = buyOffer.Amount,
                    //    ResourceId = buyOffer.ResourceId,
                    //    StartAmount = buyOffer.StartAmount,
                    //    IsValid = buyOffer.IsValid,
                    //    MaxPrice = buyOffer.MaxPrice,
                    //    Date = buyOffer.Date
                    //};

                    BuyOffer buyOfferFromDatabase =
                        RepositoryContext.BuyOffers.FirstOrDefault(x => x.Id == transactionRequest.BuyOfferId);

                    //RepositoryContext.BuyOffers.Attach(buyOfferFromDatabase);

                    buyOfferFromDatabase.Amount = buyOffer.Amount;

                    //RepositoryContext.BuyOffers.Attach(buyOfferFromDatabase);

                    RepositoryContext.SaveChanges();

                    Resource buyOfferResource =
                        RepositoryContext.Resources.FirstOrDefault(x => x.Id == buyOffer.ResourceId);

                    if (buyOfferResource == null)
                        throw new InvalidOperationException("Resources from buyOffer not found");

                    buyOfferResource.Amount += transaction.Amount;
                    //RepositoryContext.Resources.Attach(buyOfferResource);
                    RepositoryContext.SaveChanges();

                }

                RepositoryContext.SaveChanges();

                transactionScope.Commit();
            }

            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }
    }
}
