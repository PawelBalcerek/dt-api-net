using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.BuisnessObject
{
    public class Transaction
    {
        public Transaction(int id, int sellOfferId, int buyOfferId, DateTime date, decimal price, int amount)
        {
            Id = id;
            SellOfferId = sellOfferId;
            BuyOfferId = buyOfferId;
            Date = date;
            Price = price;
            Amount = amount;
        }

        public int Id { get; }
        public int SellOfferId { get; }
        public int BuyOfferId { get; }
        public DateTime Date { get; }
        public decimal Price { get; }
        public int Amount { get; }
    }
}
