using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.BusinessObject
{
    public class SellOffer : BaseOffer
    {
        public SellOffer(int id, int resourceId, int amount, DateTime date, bool isValid, decimal price, int startAmount) : base(id, resourceId, amount, date, isValid)
        {
            Price = price;
            StartAmount = startAmount;
        }
        public SellOffer(int id, int resourceId, int amount, DateTime date, bool isValid, decimal price, int startAmount, Data.Models.Company company) : base(id,resourceId, amount, date, isValid)
        {
            Price = price;
            StartAmount = startAmount;
            Company = new Company(company.Id, company.Name);
        }
        public decimal Price { get; }
        public int StartAmount { get; }
        public Company Company { get; }
    }
}
