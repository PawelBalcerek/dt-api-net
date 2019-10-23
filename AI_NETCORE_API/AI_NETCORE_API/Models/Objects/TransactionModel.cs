﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AI_NETCORE_API.Models.Objects
{
    public class TransactionModel
    {
        public int Id { get; set; }
        public int SellOfferId { get; set; }
        public int BuyOfferId { get; set; }
        public DateTime Date { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
    }
}