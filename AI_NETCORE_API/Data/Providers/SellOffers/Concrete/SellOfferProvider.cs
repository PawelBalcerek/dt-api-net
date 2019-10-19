using Data.BuisnessObject;
using Data.Infrastructure.Logging.Abstract;
using Data.Providers.SellOffers.Abstract;
using Data.Providers.SellOffers.Request.Abstract;
using Data.Providers.SellOffers.Response.Abstract;
using Data.Providers.SellOffers.Response.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Providers.SellOffers.Concrete
{
    public class SellOfferProvider : ISellOfferProvider
    {
        private readonly ILogger _logger;
        private readonly IList<SellOffer> _sellOffers;

        public SellOfferProvider(ILogger logger)
        {
            _logger = logger;
            _sellOffers = PrepareSellOffers();
        }

        private IList<SellOffer> PrepareSellOffers()
        {
            return new List<SellOffer>
            {
                new SellOffer(1,1,20,DateTime.Now,true,200),
                new SellOffer(2,2,20,DateTime.Now,true,200),
                new SellOffer(3,3,20,DateTime.Now,true,200)
            };
        }

        public IGetSellOfferByIdResponse GetSellOfferById(IGetSellOfferByIdRequest getSellOfferByIdRequest)
        {
            try
            {
                return new GetSellOfferByIdResponse(_sellOffers.ToList().FirstOrDefault(x => x.Id == getSellOfferByIdRequest.SellOfferId));
            }
            catch(Exception ex)
            {
                _logger.Log(ex);
                return new GetSellOfferByIdResponse();
            }
        }

        public IGetSellOffersResponse GetSellOffers()
        {
            try
            {
                return new GetSellOffersResponse(_sellOffers);
            }
            catch(Exception ex)
            {
                _logger.Log(ex);
                return new GetSellOffersResponse();
            }
        }
    }
}
