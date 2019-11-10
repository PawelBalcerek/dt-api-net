using AI_NETCORE_API.Models.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AI_NETCORE_API.Models.Response.ExecutingTimes;

namespace AI_NETCORE_API.Models.Response.SellOffers
{
    public class GetSellOffersByUserIdResponseModel
    {
        public IList<SellOfferModel> SellOffers { get; set; }
        public ExecutionDetails ExecDetails { get; set; }
    }
}
