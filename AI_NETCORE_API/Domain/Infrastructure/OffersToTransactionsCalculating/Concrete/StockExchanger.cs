using System;
using Domain.BusinessObject;
using Domain.Infrastructure.Logging.Abstract;
using Domain.Infrastructure.OffersToTransactionsCalculating.Abstract;
using Domain.Infrastructure.OffersToTransactionsCalculating.Request.Abstract;
using Domain.Infrastructure.OffersToTransactionsCalculating.Response.Abstract;
using Domain.Infrastructure.OffersToTransactionsCalculating.Response.Concrete;
using Domain.Infrastructure.TransactionProcessing;
using Domain.Infrastructure.TransactionProcessing.Responses.Abstract;
using Domain.Infrastructure.TransactionProcessing.Responses.Const;
using Domain.Providers.BuyOffers.Abstract;
using Domain.Providers.BuyOffers.Request.Abstract;
using Domain.Providers.BuyOffers.Request.Concrete;
using Domain.Providers.BuyOffers.Response.Abstract;
using Domain.Providers.Common.Enum;
using Domain.Providers.Configurations.Abstract;
using Domain.Providers.Configurations.Request.Concrete;
using Domain.Providers.Configurations.Response.Abstract;
using Domain.Providers.Configurations.Response.Concrete;
using Domain.Providers.SellOffers.Abstract;
using Domain.Providers.SellOffers.Request.Abstract;
using Domain.Providers.SellOffers.Request.Concrete;
using Domain.Providers.SellOffers.Response.Abstract;
using Domain.Repositories.TransactionRepo.Abstract;

namespace Domain.Infrastructure.OffersToTransactionsCalculating.Concrete
{
    public class StockExchanger : IStockExchanger
    {
        private readonly ISellOfferProvider _sellOfferProvider;
        private readonly IBuyOffersProvider _buyOffersProvider;
        private readonly ILogger _logger;
        private readonly IConfigurationsProvider _configurationsProvider;
        private readonly ITransactionRepository _transactionRepository;


        public StockExchanger(ISellOfferProvider sellOfferProvider, IBuyOffersProvider buyOffersProvider, ILogger logger, IConfigurationsProvider configurationsProvider, ITransactionRepository transactionRepository)
        {
            _sellOfferProvider = sellOfferProvider;
            _buyOffersProvider = buyOffersProvider;
            _logger = logger;
            _configurationsProvider = configurationsProvider;
            _transactionRepository = transactionRepository;
        }

        public IStockExchangeResponse StockExchange(IStockExchangeRequest stockExchangeRequest)
        {
            long databaseTime = 0;
            try
            {
                
                int companyId = stockExchangeRequest.CompanyId;

                IGetConfigurationResponse quantityFromConfiguration = _configurationsProvider.GetConfiguration(new GetConfigurationRequest("tableSize"));


                databaseTime += quantityFromConfiguration.DatabaseExecutionTime;
                if (quantityFromConfiguration.ProvideResult != ProvideEnumResult.Success)
                {
                    _logger.Log($"Configuration by key {"tableSize"} not found. Actual size is default size from code : 3");
                    quantityFromConfiguration = new GetConfigurationResponse(new Configuration("tableSize",3),0);
                    //return new StockExchangeResponse(StockExchangeResultEnum.GetConfigurationWindowSizeFail,quantityFromConfiguration.DatabaseExecutionTime);
                }


                IGetSellOffersToStockExecutionRequest getSellOffersToStockExecutionRequest = new GetSellOffersToStockExecutionRequest(quantityFromConfiguration.Configuration.Value, companyId);
                IGetSellOffersToStockExecutionResponse sellOffersToStockExecutionResponse = _sellOfferProvider.GetSellOfferToStockExecute(getSellOffersToStockExecutionRequest);
                databaseTime += sellOffersToStockExecutionResponse.DatabaseExecutionTime;
                if (sellOffersToStockExecutionResponse.ProvideResult != ProvideEnumResult.Success)
                {
                    return new StockExchangeResponse(StockExchangeResultEnum.GetSellOffersFail,databaseTime);
                }

                IGetBuyOffersToStockExecutionRequest buyOffersToStockExecutionRequest = new GetBuyOffersToStockExecutionRequest(quantityFromConfiguration.Configuration.Value, companyId);
                IGetBuyOffersToStockExecutionResponse buyOffersToStockExecutionResponse = _buyOffersProvider.GetBuyOfferToStockExecution(buyOffersToStockExecutionRequest);
                databaseTime += buyOffersToStockExecutionResponse.DatabaseExecutionTime;
                if (buyOffersToStockExecutionResponse.ProvideResult != ProvideEnumResult.Success)
                {
                    return new StockExchangeResponse(StockExchangeResultEnum.GetBuyOffersFail,databaseTime);
                }

                TransactionWindow transactionWindow = new TransactionWindow(buyOffersToStockExecutionResponse.BuyOffers,
                    sellOffersToStockExecutionResponse.SellOffers, quantityFromConfiguration.Configuration.Value);

                if (!transactionWindow.IsValid)
                {
                    return new StockExchangeResponse(StockExchangeResultEnum.ProcessingWindowIsNotValid,databaseTime);
                }

                IProcessingTransactionWindowResult processingTransactionWindowResult = transactionWindow.Process(_logger);
                
                long databaseProcessingExecutionTime = _transactionRepository.SaveTransactionsAfterProcessing(
                    processingTransactionWindowResult.SellOffersToSave,
                    processingTransactionWindowResult.BuyOffersToSave,
                    processingTransactionWindowResult.TransactionsToSave);
                databaseTime += databaseProcessingExecutionTime;

                
                return new StockExchangeResponse(StockExchangeResultEnum.Success,databaseTime);

            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                return new StockExchangeResponse(StockExchangeResultEnum.Exception,databaseTime);
            }
            

        }
    }
}
