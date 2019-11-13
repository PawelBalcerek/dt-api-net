using System;
using System.Collections.Generic;
using System.Text;
using Domain.BusinessObject;
using Domain.Creators.SellOffer.Request.Abstract;
using Domain.Infrastructure.Logging.Abstract;
using Domain.Infrastructure.OffersToTransactionsCalculating.Abstract;
using Domain.Infrastructure.OffersToTransactionsCalculating.Response.Abstract;
using Domain.Infrastructure.TransactionProcessing;
using Domain.Providers.BuyOffers.Abstract;
using Domain.Providers.BuyOffers.Response.Abstract;
using Domain.Providers.Common.Enum;
using Domain.Providers.SellOffers.Abstract;
using Domain.Providers.SellOffers.Response.Abstract;

namespace Domain.Infrastructure.OffersToTransactionsCalculating.Concrete
{
    public class StockExchanger : IStockExchanger
    {
        private readonly ISellOfferProvider _sellOfferProvider;
        private readonly IBuyOffersProvider _buyOffersProvider;
        private readonly ILogger _logger;
        

        public StockExchanger(ISellOfferProvider sellOfferProvider, IBuyOffersProvider buyOffersProvider, ILogger logger)
        {
            _sellOfferProvider = sellOfferProvider;
            _buyOffersProvider = buyOffersProvider;
            _logger = logger;
        }

        public IStockExchangeResponse StockExchange()
        {
            try
            {
                const int quantityFromConfiguration = 5;
                IGetSellOffersToStockExecutionResponse sellOffersToStockExecutionResponse =
                    _sellOfferProvider.GetSellOfferToStockExecute(quantityFromConfiguration);
                if (sellOffersToStockExecutionResponse.ProvideResult != ProvideEnumResult.Success)
                {
                    //TODO inform
                }

                IGetBuyOffersToStockExecutionResponse buyOffersToStockExecutionResponse =
                    _buyOffersProvider.GetBuyOfferToStockExecution(quantityFromConfiguration);
                if (buyOffersToStockExecutionResponse.ProvideResult != ProvideEnumResult.Success)
                {
                    //TODO inform
                }

                TransactionWindow transactionWindow = new TransactionWindow(buyOffersToStockExecutionResponse.BuyOffers,
                    sellOffersToStockExecutionResponse.SellOffers, quantityFromConfiguration);

            }
            catch (Exception ex)
            {
                _logger.Log(ex);
            }


        }
    }
}
