using Domain.Creators.SellOffer.Request.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Creators.SellOffer.Request.Concrete
{
    public class SellOfferCreateRequest : ISellOfferCreateRequest
    {
        public SellOfferCreateRequest(int resourceId, int amount, double price, int userId)
        {
            ResourceId = resourceId;
            Amount = amount;
            Price = price;
            UserId = userId;
        }

        public int ResourceId { get; }
        public int Amount { get; }
        public double Price { get; }
        public int UserId { get; }
    }
 
}
