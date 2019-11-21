using Domain.Creators.BuyOffer.Abstract;
using Domain.Creators.BuyOffer.Request.Abstract;
using Domain.Creators.BuyOffer.Response.Abstract;
using Domain.Creators.BuyOffer.Response.Concrete;
using Domain.Infrastructure.Logging.Abstract;
using Domain.Repositories.BuyOfferRepo.Abstract;
using System;
using Domain.Infrastructure.OffersToTransactionsCalculating.Abstract;
using Domain.Infrastructure.OffersToTransactionsCalculating.Request.Abstract;
using Domain.Infrastructure.OffersToTransactionsCalculating.Request.Concrete;
using Domain.Infrastructure.OffersToTransactionsCalculating.Response.Abstract;

namespace Domain.Creators.BuyOffer.Concrete
{
    public class BuyOfferCreator : IBuyOfferCreator
    {
        private readonly ILogger _logger;
        private readonly IBuyOfferRepository _buyOfferRepository;
        private readonly IStockExchanger _stockExchanger;

        public BuyOfferCreator(ILogger logger, IBuyOfferRepository buyOfferRepository, IStockExchanger stockExchanger)
        {
            _logger = logger;
            _buyOfferRepository = buyOfferRepository;
            _stockExchanger = stockExchanger;
        }

        public IBuyOfferCreateResponse CreateBuyOffer(IBuyOfferCreateRequest buyOfferCreateRequest)
        {
            try
            {
                long databaseExecutionTime = 0;
                long createBuyOfferTime = _buyOfferRepository.CreateBuyOffer(buyOfferCreateRequest.CompanyId, buyOfferCreateRequest.Amount, buyOfferCreateRequest.Price, buyOfferCreateRequest.UserId);

                databaseExecutionTime += createBuyOfferTime;

                IStockExchangeRequest stockExchangeRequest = new StockExchangeRequest(buyOfferCreateRequest.CompanyId);

                IStockExchangeResponse stockExchangeResponse = _stockExchanger.StockExchange(stockExchangeRequest);

                databaseExecutionTime += stockExchangeResponse.DatabaseTime;

                _logger.Log($"Offer processing result : {stockExchangeResponse.Result.ToString()} in {stockExchangeResponse}ms");

                return new BuyOfferCreateResponse(true, databaseExecutionTime);
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                return new BuyOfferCreateResponse();
            }
        }
    }
}
