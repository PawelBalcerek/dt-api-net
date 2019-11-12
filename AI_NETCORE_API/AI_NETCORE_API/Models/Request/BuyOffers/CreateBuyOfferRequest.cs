using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AI_NETCORE_API.Models.Request.BuyOffers
{
    public class CreateBuyOfferRequest
    {
        public int CompanyId { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
    }
}
