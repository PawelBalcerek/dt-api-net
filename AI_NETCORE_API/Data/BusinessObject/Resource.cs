﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.BusinessObject
{
    public class Resource
    {
        public Resource(int id, int userId, int companyId, int amount)
        {
            Id = id;
            UserId = userId;
            CompanyId = companyId;
            Amount = amount;
        }

        public int Id { get; }
        public int UserId { get; }
        public int CompanyId { get; }
        public int Amount { get; }
    }
}