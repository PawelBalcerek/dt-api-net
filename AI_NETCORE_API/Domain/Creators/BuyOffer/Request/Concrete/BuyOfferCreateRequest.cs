using Domain.Creators.BuyOffer.Request.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Creators.BuyOffer.Request.Concrete
{
    public class BuyOfferCreateRequest : IBuyOfferCreateRequest
    {
        public BuyOfferCreateRequest(int companyId, int amount, decimal price)
        {
            CompanyId = companyId;
            Amount = amount;
            Price = price;
        }

        public int CompanyId { get; }
        public int Amount { get; }
        public decimal Price { get; }
    }
}
