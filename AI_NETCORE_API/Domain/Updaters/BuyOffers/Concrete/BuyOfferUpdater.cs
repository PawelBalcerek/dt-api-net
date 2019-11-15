using Domain.Infrastructure.Logging.Abstract;
using Domain.Repositories.BuyOfferRepo.Abstract;
using Domain.Updaters.Buyoffers.Abstract;
using Domain.Updaters.BuyOffers.Request.Abstract;
using Domain.Updaters.BuyOffers.Response.Abstract;
using Domain.Updaters.BuyOffers.Response.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Updaters.Buyoffers.Concrete
{
    public class BuyOfferUpdater : IBuyOfferUpdater
    {
        private readonly ILogger _logger;
        private readonly IBuyOfferRepository _buyOffers;

        public BuyOfferUpdater(ILogger logger, IBuyOfferRepository buyOffers)
        {
            _logger = logger;
            _buyOffers = buyOffers;
        }

        public IWithdrawBuyOfferByIdResponse WithdrawBuyOfferById(IWithdrawBuyOfferByIdRequest withdrawBuyOfferByIdRequest)
        {
           
            try
            {
                var result = _buyOffers.WithdrawBuyOffer(withdrawBuyOfferByIdRequest.BuyOfferId);
                return new WithdrawBuyOfferByIdResponse(result);

            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                return new WithdrawBuyOfferByIdResponse();
            }
        }
    }
}
