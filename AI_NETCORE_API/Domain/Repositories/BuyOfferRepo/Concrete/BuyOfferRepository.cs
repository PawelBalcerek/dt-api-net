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

        public BusinessObject.BuyOffer GetBuyOfferById(int id)
        {
            BuyOffer buyOffer = FindByCondition(buyOfferExpr => buyOfferExpr.Id == id).FirstOrDefault();
            return _converter.ConvertBuyOffer(buyOffer);
        }

        public IEnumerable<BusinessObject.BuyOffer> GetAllBuyOffers()
        {
            return FindAll().Select(b => _converter.ConvertBuyOffer(b));
        }

        public RepositoryResponse<IEnumerable<BusinessObject.BuyOffer>> GetSellOfferToStockExecute(int quantity)
        {
            Stopwatch timer = Stopwatch.StartNew();
            IList<BuyOffer> buyOffers = RepositoryContext.BuyOffers.OrderByDescending(x => x.MaxPrice).Take(quantity).ToList();
            timer.Stop();
            long time = timer.ElapsedMilliseconds;
            return new RepositoryResponse<IEnumerable<BusinessObject.BuyOffer>>(buyOffers.Select(x => _converter.ConvertBuyOffer(x)), time);

        }
    }
}
