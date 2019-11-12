using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.BusinessObject
{
    public class BuyOffer : BaseOffer
    {
        public BuyOffer(int id, int resourceId, int amount, DateTime date, bool isValid, decimal maxPrice, int startAmount, Company company) : base(id,resourceId, amount, date, isValid)
        {
            MaxPrice = maxPrice;
            Company = company;
            StartAmount = startAmount;
        }

        public decimal MaxPrice { get; }
        public int StartAmount { get; }
        public Company Company { get; }
    }
}
