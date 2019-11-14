using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Domain.Repositories.BaseRepo.Concrete;
using Domain.Repositories.BuyOfferRepo.Abstract;
using Data.Models;
using System.Linq;
using Domain.DTOToBOConverting;
using Domain.Repositories.BaseRepo.Response;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace Domain.Repositories.BuyOfferRepo.Concrete
{
    public class BuyOfferRepository: RepositoryBase<BuyOffer>, IBuyOfferRepository
    {
        private readonly IDTOToBOConverter _converter;
        public BuyOfferRepository(RepositoryContext repositoryContext, IDTOToBOConverter converter)
            : base(repositoryContext)
        {
            _converter = converter;
        }

        public RepositoryResponse<IEnumerable<BusinessObject.BuyOffer>> GetBuyOffersByUserId(int id)
        {
            var timer = Stopwatch.StartNew();
            var buyOffers = FindByCondition(p => p.Resource.UserId == id && p.IsValid == true).Include(p => p.Resource).Include(p => p.Resource.Comp).Select(p => _converter.ConvertBuyOffer(p));
            timer.Stop();
            var time = timer.ElapsedMilliseconds;
            return new RepositoryResponse<IEnumerable<BusinessObject.BuyOffer>>(buyOffers, time);
        }

        public long CreateBuyOffer(int companyId, int amount, double price)
        {
            var timer = Stopwatch.StartNew();
            RepositoryContext.BuyOffers.Add(new BuyOffer
            {
                Amount = amount,
                StartAmount = amount,
                IsValid = true,
                MaxPrice = price,
                Resource = new Resource()
                {
                    CompId = companyId
                },
                Date = DateTime.Now
            });
            RepositoryContext.SaveChanges(true);
            timer.Stop();
            var time = timer.ElapsedMilliseconds;
            return time;
        }

        public long WithdrawBuyOffer(int buyOfferId)
        {
            var timer = Stopwatch.StartNew();
            using (var dbContext = new RepositoryContext())
            {
                BuyOffer offer = dbContext.BuyOffers.Where(p => p.Id == buyOfferId).First();
                offer.IsValid = false;

            }
            RepositoryContext.SaveChanges(true);
            timer.Stop();
            var time = timer.ElapsedMilliseconds;
            return time;
        }

        
        public RepositoryResponse<IEnumerable<BusinessObject.BuyOffer>> GetSellOfferToStockExecute(int quantity,int companyId)
        {
            Stopwatch timer = Stopwatch.StartNew();
            IList<BuyOffer> buyOffers = RepositoryContext.BuyOffers.Include(p => p.Resource).Include(p => p.Resource.Comp).OrderByDescending(x => x.MaxPrice).Where(bo => bo.Resource.Comp.Id == companyId).Take(quantity).ToList();
            timer.Stop();
            long time = timer.ElapsedMilliseconds;
            return new RepositoryResponse<IEnumerable<BusinessObject.BuyOffer>>(buyOffers.Select(x => _converter.ConvertBuyOffer(x)), time);

        }
    }
}
