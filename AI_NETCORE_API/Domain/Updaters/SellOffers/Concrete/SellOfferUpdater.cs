using Domain.Infrastructure.Logging.Abstract;
using Domain.Providers.SellOffers.Request.Abstract;
using Domain.Providers.SellOffers.Response.Abstract;
using Domain.Providers.SellOffers.Response.Concrete;
using Domain.Repositories.SellOfferRepo.Abstract;
using Domain.Updaters.SellOffers.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Updaters.SellOffers.Concrete
{
    public class SellOfferUpdater : ISellOfferUpdater
    {
        private readonly ILogger _logger;
        private readonly ISellOfferRepository _sellOffers;

        public SellOfferUpdater(ILogger logger, ISellOfferRepository sellOffers)
        {
            _logger = logger;
            _sellOffers = sellOffers;
        }

        public IWithdrawSellOfferByIdResponse WithdrawSellOfferById(IWithdrawSellOfferByIdRequest withdrawSellOfferByIdRequest)
        {
            try
            {
                var result = _sellOffers.WithdrawSellOffer(withdrawSellOfferByIdRequest.SellOfferId);
                return new WithdrawSellOfferByIdResponse(result);

            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                return new WithdrawSellOfferByIdResponse();
            }
        }
    }
}
