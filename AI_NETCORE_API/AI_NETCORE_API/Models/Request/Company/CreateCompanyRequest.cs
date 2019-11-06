using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AI_NETCORE_API.Models.Request.Company
{
    public class CreateCompanyRequest
    {
        public string Name { get; set; }
        public int ResourceAmount { get; set; }


        public bool IsValid => !string.IsNullOrWhiteSpace(Name) && ResourceAmount > 0;
    }
}
