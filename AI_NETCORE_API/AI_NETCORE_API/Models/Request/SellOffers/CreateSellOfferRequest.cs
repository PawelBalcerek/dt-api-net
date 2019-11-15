using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AI_NETCORE_API.Models.Request.SellOffers
{
    public class CreateSellOfferRequest
    {
        public int ResourceId { get; set; }
        public int Amount { get; set; }
        public double Price { get; set; }
    }
}
