using System;
using System.Collections.Generic;
using System.Text;

namespace Data.BuisnessObject
{
    public abstract class BaseOffer
    {
        protected BaseOffer(int id, int resourceId, int amount, DateTime date, bool isValid)
        {
            Id = id;
            ResourceId = resourceId;
            Amount = amount;
            Date = date;
            IsValid = isValid;
        }

        public int Id { get; }
        public int ResourceId { get; }
        public int Amount { get; }
        public DateTime Date { get; }
        public bool IsValid { get; }
    }
}
