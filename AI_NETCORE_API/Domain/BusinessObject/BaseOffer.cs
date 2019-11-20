using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.BusinessObject
{
    public abstract class BaseOffer
    {
        protected BaseOffer(int id,int resourceId,  int amount, DateTime date, bool isValid)
        {
            Id = id;
            Amount = amount;
            Date = date;
            IsValid = isValid;
            ResourceId = resourceId;
        }

        public int Id { get; }
        public int Amount { get; private set; }
        public int ResourceId { get; }
        public DateTime Date { get; }
        public bool IsValid { get; }

        public void UpdateActualAmount(int newAmount)
        {
            Amount = newAmount;
        }
    }
}
