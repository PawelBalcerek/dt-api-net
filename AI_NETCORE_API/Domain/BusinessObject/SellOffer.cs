using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.BusinessObject
{
    public class SellOffer : BaseOffer
    {
        public SellOffer(int id, int resourceId, int amount, DateTime date, bool isValid, double price, int startAmount, Company company) : base(id,resourceId, amount, date, isValid)
        {
            Price = price;
            StartAmount = startAmount;
            Company = company;
        }
        public double Price { get; }
        public int StartAmount { get; }
        public Company Company { get; }
    }
}
