﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AI_NETCORE_API.Models.Objects
{
    public class ResourceModel
    {
        public int Id { get; set; }
        public CompanyModel Company { get; set; }
        public int Amount { get; set; }
    }
}
