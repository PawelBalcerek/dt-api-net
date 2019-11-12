using Domain.BusinessObject;
using Domain.Infrastructure.Logging.Abstract;
using Domain.Providers.BuyOffers.Abstract;
using Domain.Providers.BuyOffers.Request.Abstract;
using Domain.Providers.BuyOffers.Response.Abstract;
using Domain.Providers.BuyOffers.Response.Concrete;
using Domain.Repositories.BuyOfferRepo.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Providers.BuyOffers.Concrete
{
    public class BuyOffersProvider : IBuyOffersProvider
    {
        private readonly ILogger _logger;
        private readonly IBuyOfferRepository _buyOffers;

        public BuyOffersProvider(ILogger logger, IBuyOfferRepository buyOffers)
        {
            _logger = logger;
            _buyOffers = buyOffers;
        }
        public IGetBuyOfferByIdResponse GetBuyOfferById(IGetBuyOfferByIdRequest getBuyOfferByIdRequest)
        {
            try
            {
                return new GetBuyOfferByIdResponse(_buyOffers.GetBuyOfferById(getBuyOfferByIdRequest.BuyOfferId));
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                return new GetBuyOfferByIdResponse();
            }
        }

        public IGetBuyOffersResponse GetBuyOffers()
        {
            try
            {
                return new GetBuyOffersResponse(_buyOffers.GetAllBuyOffers().ToList());
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                return new GetBuyOffersResponse();
            }
        }

        IGetBuyOffersByUserIdResponse GetBuyOffersByUserId(IGetBuyOffersByUserIdRequest getBuyOffersByUserIdRequest)
        {
            try
            {
                var result = _buyOffers.GetBuyOffersByUserId(getBuyOffersByUserIdRequest.UserId);
                return new GetBuyOffersByUserIdResponse(result.Object.ToList(), result.DatabaseTime);

            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                return new GetBuyOffersByUserIdResponse();
            }
        }
    }
}
