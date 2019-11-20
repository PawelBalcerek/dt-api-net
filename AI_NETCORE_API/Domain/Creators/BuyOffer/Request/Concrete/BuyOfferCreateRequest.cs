using Domain.Creators.BuyOffer.Request.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Creators.BuyOffer.Request.Concrete
{
    public class BuyOfferCreateRequest : IBuyOfferCreateRequest
    {
        public BuyOfferCreateRequest(int companyId, int amount, double price, int userId)
        {
            CompanyId = companyId;
            Amount = amount;
            Price = price;
            UserId = userId;
        }

        public int CompanyId { get; }
        public int Amount { get; }
        public double Price { get; }
        public int UserId { get; }
    }
}
