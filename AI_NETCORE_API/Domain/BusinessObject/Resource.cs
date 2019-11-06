using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.BusinessObject
{
    public class Resource
    {
        public Resource(int id, int userId, Company company, int amount)
        {
            Id = id;
            UserId = userId;
            Company = company;
            Amount = amount;
        }

        public int Id { get; }
        public int UserId { get; }
        public Company Company { get; }
        public int Amount { get; }
    }
}
