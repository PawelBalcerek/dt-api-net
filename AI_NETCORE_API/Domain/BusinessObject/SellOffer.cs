﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.BusinessObject
{
    public class SellOffer : BaseOffer
    {
        public SellOffer(int id, int resourceId, int amount, DateTime date, bool isValid, decimal price, int startamount) : base(id,resourceId, amount, date, isValid)
        {
            Price = price;
            StartAmount = startamount;
        }
        public decimal Price { get; }
        public int StartAmount { get; }
    }
}
