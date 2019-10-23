using Domain.BuisnessObject;
using Domain.Infrastructure.Logging.Abstract;
using Domain.Providers.SellOffers.Abstract;
using Domain.Providers.SellOffers.Request.Abstract;
using Domain.Providers.SellOffers.Response.Abstract;
using Domain.Providers.SellOffers.Response.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Providers.SellOffers.Concrete
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
