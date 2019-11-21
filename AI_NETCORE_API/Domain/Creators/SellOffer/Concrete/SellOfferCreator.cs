using Domain.Creators.SellOffer.Abstract;
using Domain.Creators.SellOffer.Request.Abstract;
using Domain.Creators.SellOffer.Response.Abstract;
using Domain.Creators.SellOffer.Response.Concrete;
using Domain.Infrastructure.Logging.Abstract;
using Domain.Repositories.SellOfferRepo.Abstract;
using System;
using Domain.Infrastructure.OffersToTransactionsCalculating.Abstract;
using Domain.Infrastructure.OffersToTransactionsCalculating.Request.Abstract;
using Domain.Infrastructure.OffersToTransactionsCalculating.Request.Concrete;
using Domain.Infrastructure.OffersToTransactionsCalculating.Response.Abstract;
using Domain.Repositories.BaseRepo.Response;
using Domain.Repositories.ResourceRepo.Abstract;

namespace Domain.Creators.SellOffer.Concrete
{
    public class SellOfferCreator : ISellOfferCreator
    {
        private readonly ILogger _logger;
        private readonly ISellOfferRepository _sellOfferRepository;
        private readonly IResourceRepository _resourceRepository;
        private readonly IStockExchanger _stockExchanger;


        public SellOfferCreator(ILogger logger, ISellOfferRepository sellOfferRepository, IResourceRepository resourceRepository, IStockExchanger stockExchanger)
        {
            _logger = logger;
            _sellOfferRepository = sellOfferRepository;
            _resourceRepository = resourceRepository;
            _stockExchanger = stockExchanger;
        }

        public ISellOfferCreateResponse CreateSellOffer(ISellOfferCreateRequest sellOfferCreateRequest)
        {
            try
            {
                long databaseExecutionTime = 0;
                RepositoryResponse<bool> createSellOfferResult = _sellOfferRepository.CreateSellOffer(sellOfferCreateRequest.ResourceId, sellOfferCreateRequest.Amount, sellOfferCreateRequest.Price, sellOfferCreateRequest.UserId);
                databaseExecutionTime += createSellOfferResult.DatabaseTime;

                RepositoryResponse<int?> getCompanyIdByResourceId = _resourceRepository.GetCompanyIdByResourceId(sellOfferCreateRequest.ResourceId);
                databaseExecutionTime += getCompanyIdByResourceId.DatabaseTime;
                
                if(!getCompanyIdByResourceId.Object.HasValue) throw new InvalidProgramException("CompanyId by resourceId not found");

                IStockExchangeRequest stockExchangeRequest = new StockExchangeRequest(getCompanyIdByResourceId.Object.Value);

                IStockExchangeResponse stockExchangeResponse = _stockExchanger.StockExchange(stockExchangeRequest);

                databaseExecutionTime += stockExchangeResponse.DatabaseTime;

                _logger.Log($"Offer processing result : {stockExchangeResponse.Result.ToString()} in {stockExchangeResponse}ms");

                return new SellOfferCreateResponse(createSellOfferResult.Object, databaseExecutionTime);
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                return new SellOfferCreateResponse();
            }
        }
    }
}
