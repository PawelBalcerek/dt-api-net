using Domain.Creators.BuyOffer.Abstract;
using Domain.Creators.BuyOffer.Request.Abstract;
using Domain.Creators.BuyOffer.Response.Abstract;
using Domain.Creators.BuyOffer.Response.Concrete;
using Domain.Infrastructure.Logging.Abstract;
using Domain.Repositories.BuyOfferRepo.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Creators.BuyOffer.Concrete
{
    public class BuyOfferCreator : IBuyOfferCreator
    {
        private readonly ILogger _logger;
        private readonly IBuyOfferRepository _buyOfferRepository;

        public BuyOfferCreator(ILogger logger, IBuyOfferRepository buyOfferRepository)
        {
            _logger = logger;
            _buyOfferRepository = buyOfferRepository;
        }

        public IBuyOfferCreateResponse CreateBuyOffer(IBuyOfferCreateRequest buyOfferCreateRequest)
        {
            try
            {
                long time = _buyOfferRepository.CreateBuyOffer(buyOfferCreateRequest.CompanyId, buyOfferCreateRequest.Amount, buyOfferCreateRequest.Price, buyOfferCreateRequest.UserId);

                return new BuyOfferCreateResponse(true, time);
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                return new BuyOfferCreateResponse();
            }
        }
    }
}
