using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.BuisnessObject
{
    public class SellOffer : BaseOffer
    {
        public SellOffer(int id, int resourceId, int amount, DateTime date, bool isValid,decimal price) : base(id,resourceId, amount, date, isValid)
        {
            Price = price;
        }
        public decimal Price { get; }
    }
}
