using Domain.Creators.SellOffer.Abstract;
using Domain.Creators.SellOffer.Request.Abstract;
using Domain.Creators.SellOffer.Response.Abstract;
using Domain.Creators.SellOffer.Response.Concrete;
using Domain.Infrastructure.Logging.Abstract;
using Domain.Repositories.SellOfferRepo.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Creators.SellOffer.Concrete
{
    public class SellOfferCreator : ISellOfferCreator
    {
        private readonly ILogger _logger;
        private readonly ISellOfferRepository _sellOfferRepository;

        public SellOfferCreator(ILogger logger, ISellOfferRepository sellOfferRepository)
        {
            _logger = logger;
            _sellOfferRepository = sellOfferRepository;
        }

        public ISellOfferCreateResponse CreateSellOffer(ISellOfferCreateRequest sellOfferCreateRequest)
        {
            try
            {
                long time = _sellOfferRepository.CreateSellOffer(sellOfferCreateRequest.ResourceId, sellOfferCreateRequest.Amount, sellOfferCreateRequest.Price);

                return new SellOfferCreateResponse(true, time);
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                return new SellOfferCreateResponse();
            }
        }
    }
}
