using System;
using System.Collections.Generic;
using System.Text;
using Domain.Creators.Transaction.Request.Abstract;

namespace Domain.Creators.Transaction.Request.Concrete
{
    public class CreateTransactionRequest : ICreateTransactionRequest
    {
        public CreateTransactionRequest(int sellOfferId, int buyOfferId, decimal price, int amount)
        {
            SellOfferId = sellOfferId;
            BuyOfferId = buyOfferId;
            Price = price;
            Amount = amount;
            CreationDate = DateTime.Now;
        }

        public int SellOfferId { get; }
        public int BuyOfferId { get; }
        public DateTime CreationDate { get; }
        public decimal Price { get; }
        public int Amount { get; }
    }
}
