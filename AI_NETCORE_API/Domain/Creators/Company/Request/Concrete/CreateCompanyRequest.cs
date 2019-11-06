using System;
using System.Collections.Generic;
using System.Text;
using Domain.Creators.Company.Request.Abstract;

namespace Domain.Creators.Company.Request.Concrete
{
    public class CreateCompanyRequest : ICreateCompanyRequest
    {
        public CreateCompanyRequest(int userId, string name, int resourceAmount)
        {
            UserId = userId;
            Name = name;
            ResourceAmount = resourceAmount;
        }

        public int UserId { get; }
        public string Name { get; }
        public int ResourceAmount { get; }
    }
}
