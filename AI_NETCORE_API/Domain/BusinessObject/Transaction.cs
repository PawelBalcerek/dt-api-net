using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.BusinessObject
{
    public enum TransactionType { BUY_OFFER, SELL_OFFER };

    public class Transaction
    {
        public Transaction(int id, int sellOfferId, int buyOfferId, DateTime date, double price, int amount, Company company)
        {
            Id = id;
            SellOfferId = sellOfferId;
            BuyOfferId = buyOfferId;
            Date = date;
            Price = price;
            Amount = amount;
            Company = company;
        }

        public int Id { get; }
        public int SellOfferId { get; }
        public int BuyOfferId { get; }
        public DateTime Date { get; }
        public double Price { get; }
        public int Amount { get; }
        public Company Company { get; }
        public TransactionType Type { get; set; }
    }
}
