using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Infrastructure.OffersToTransactionsCalculating.Abstract;
using Domain.Infrastructure.OffersToTransactionsCalculating.Request.Concrete;
using Domain.Infrastructure.OffersToTransactionsCalculating.Response.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace AI_NETCORE_API.Controllers
{
    [Route("api/Debug")]
    [ApiController]
    public class DebugController : ControllerBase
    {
        private readonly IStockExchanger _stockExchanger;

        public DebugController(IStockExchanger stockExchanger)
        {
            _stockExchanger = stockExchanger;
        }


        [HttpGet("")]
        [ProducesResponseType(200, Type = typeof(object))]
        [ProducesResponseType(500)]
        public ActionResult<object> DoExchange()
        {
            IStockExchangeResponse stockExchangeResponse = _stockExchanger.StockExchange(new StockExchangeRequest(9));
            return null;
        }
    }
}