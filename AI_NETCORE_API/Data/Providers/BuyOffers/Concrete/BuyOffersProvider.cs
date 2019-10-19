using Data.BuisnessObject;
using Data.Infrastructure.Logging.Abstract;
using Data.Providers.BuyOffers.Abstract;
using Data.Providers.BuyOffers.Request.Abstract;
using Data.Providers.BuyOffers.Response.Abstract;
using Data.Providers.BuyOffers.Response.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Providers.BuyOffers.Concrete
{
    public class BuyOffersProvider : IBuyOffersProvider
    {
        private readonly ILogger _logger;
        private readonly IList<BuyOffer> _buyOffers;

        public BuyOffersProvider(ILogger logger)
        {
            _logger = logger;
            _buyOffers = PrepareBuyOfferList();
        }
        private IList<BuyOffer> PrepareBuyOfferList()
        {
            return new List<BuyOffer>
            {
                new BuyOffer(1,1,20,DateTime.Now,true,20),
                new BuyOffer(2,2,20,DateTime.Now,true,30),
                new BuyOffer(3,1,30,DateTime.Now,true,10)
            };
        }
        public IGetBuyOfferByIdResponse GetBuyOfferById(IGetBuyOfferByIdRequest getBuyOfferByIdRequest)
        {
            try
            {
                return new GetBuyOfferByIdResponse(_buyOffers.ToList().FirstOrDefault(x => x.Id == getBuyOfferByIdRequest.BuyOfferId));
            }
            catch(Exception ex)
            {
                _logger.Log(ex);
                return new GetBuyOfferByIdResponse();
            }
        }

        public IGetBuyOffersResponse GetBuyOffers()
        {
            try
            {
                return new GetBuyOffersResponse(_buyOffers); 
            }
            catch(Exception ex)
            {
                _logger.Log(ex);
                return new GetBuyOffersResponse();
            }
        }
    }
}
