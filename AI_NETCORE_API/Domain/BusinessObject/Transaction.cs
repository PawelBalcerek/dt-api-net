using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.BusinessObject
{
    public class Transaction
    {
        public Transaction(int id, int sellOfferId, int buyOfferId, DateTime date, double price, int amount)
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
        public double Price { get; }
        public int Amount { get; }
    }
}
