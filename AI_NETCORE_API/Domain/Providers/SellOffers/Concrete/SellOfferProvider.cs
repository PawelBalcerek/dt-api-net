using Domain.BusinessObject;
using Domain.Infrastructure.Logging.Abstract;
using Domain.Providers.SellOffers.Abstract;
using Domain.Providers.SellOffers.Request.Abstract;
using Domain.Providers.SellOffers.Response.Abstract;
using Domain.Providers.SellOffers.Response.Concrete;
using Domain.Repositories.SellOfferRepo.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Providers.SellOffers.Concrete
{
    public class SellOfferProvider : ISellOfferProvider
    {
        private readonly ILogger _logger;
        private readonly ISellOfferRepository _sellOffers;

        public SellOfferProvider(ILogger logger, ISellOfferRepository sellOffers)
        {
            _logger = logger;
            _sellOffers = sellOffers;
        }

        public IGetSellOfferByIdResponse GetSellOfferById(IGetSellOfferByIdRequest getSellOfferByIdRequest)
        {
            try
            {
                return new GetSellOfferByIdResponse(_sellOffers.GetSellOfferById(getSellOfferByIdRequest.SellOfferId));
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                return new GetSellOfferByIdResponse();
            }
        }

        public IGetSellOffersResponse GetSellOffers()
        {
            try
            {
                return new GetSellOffersResponse(_sellOffers.GetAllSellOffers().ToList());
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                return new GetSellOffersResponse();
            }
        }

        public IGetSellOffersByUserIdResponse GetSellOffersByUserId(IGetSellOffersByUserIdRequest getSellOffersByUserIdRequest)
        {
            try
            {
                return new GetSellOffersByUserIdResponse(_sellOffers.GetSellOffersByUserId(getSellOffersByUserIdRequest.UserId));
                
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                return new GetSellOffersByUserIdResponse();
            }
        }
    }
}
